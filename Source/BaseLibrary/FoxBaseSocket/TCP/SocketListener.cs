using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using FoxBaseSocket.Infrastructure;
using FoxBaseSocket.Interface;

namespace FoxBaseSocket.TCP
{
    /// <summary>
    /// Implements the connection logic for the socket server.  
    /// After accepting a connection, all data read from the client is sent back. 
    /// The read and echo back to the client pattern is continued until the client disconnects.
    /// </summary>
    public sealed class SocketListener
    {
        /// <summary>
        /// The socket used to listen for incoming connection requests.
        /// </summary>
        private Socket _listenSocket;

        /// <summary>
        /// Buffer size to use for each socket I/O operation.
        /// </summary>
        private readonly Int32 _bufferSize;

        /// <summary>
        /// The total number of clients connected to the server.
        /// </summary>
        private Int32 _numConnectedSockets;

        /// <summary>
        /// the maximum number of connections the sample is designed to handle simultaneously.
        /// </summary>
        private readonly Int32 _numConnections;

        /// <summary>
        /// Pool of reusable SocketAsyncEventArgs objects for write, read and accept socket operations.
        /// </summary>
        private SocketAsyncEventArgsPool _readPool;
        private SocketAsyncEventArgsPool _writePool;
        private readonly TokenDictionary _tokens;

        /// <summary>
        /// Controls the total number of clients connected to the server.
        /// </summary>
        private readonly Semaphore _semaphoreAcceptedClients;

        private bool _isOnLine;

        public ISocketRev SocketRev { private get; set; }
        /// <summary>
        /// Create an uninitialized server instance.  
        /// To start the server listening for connection requests
        /// call the Init method followed by Start method.
        /// </summary>
        /// <param name="numConnections">Maximum number of connections to be handled simultaneously.</param>
        /// <param name="bufferSize">Buffer size to use for each socket I/O operation.</param>
        public SocketListener(Int32 numConnections, Int32 bufferSize)
        {
            this._numConnectedSockets = 0;
            this._numConnections = numConnections;
            this._bufferSize = bufferSize;
            _isOnLine = false;
            _tokens = new TokenDictionary(numConnections);
            InitReadWritePool(numConnections);
            this._semaphoreAcceptedClients = new Semaphore(numConnections, numConnections);
        }

        private void InitReadWritePool(Int32 capacity)
        {
            this._readPool = new SocketAsyncEventArgsPool(capacity);
            // Preallocate pool of SocketAsyncEventArgs objects.
            for (Int32 i = 0; i < capacity; i++)
            {
                var readEventArg = new SocketAsyncEventArgs();
                readEventArg.Completed += OnIoCompleted;
                readEventArg.SetBuffer(new Byte[this._bufferSize], 0, this._bufferSize);
                // Add SocketAsyncEventArg to the pool.
                this._readPool.Push(readEventArg);
            }

            this._writePool = new SocketAsyncEventArgsPool(capacity);
            for (Int32 i = 0; i < capacity; i++)
            {
                var writeEventArg = new SocketAsyncEventArgs();
                writeEventArg.Completed += OnIoCompleted;
                writeEventArg.SetBuffer(new Byte[this._bufferSize], 0, this._bufferSize);
                this._writePool.Push(writeEventArg);
            }

        }

        public int Send(string ipAndport, string message)
        {
            if (string.IsNullOrEmpty(ipAndport)) return 0;
            if (string.IsNullOrEmpty(message)) return 0;
            var sendTmpBuffer = Encoding.UTF8.GetBytes(message);
            return Send(ipAndport, sendTmpBuffer);
        }

        public int Send(string ipAndport, byte[] message)
        {
            if (string.IsNullOrEmpty(ipAndport)) return 0;
            if (null == message) return 0;
            var token = _tokens.GetToken(ipAndport);
            if (token == null) return 0;
            if (token.Connection.Connected)
            {
                lock (token.Connection)
                {
                    return token.Connection.Send(message);
                }
            }
            else
            {
                CloseClient(token);
                return -1;
            }
        }

        public int SendToAll(byte[] message)
        {
            int result = 0;
            try
            {
                foreach (var token in _tokens.GetEnumerable())
                {
                    if (token.Value.Connection.Connected)
                    {
                        lock (token.Value.Connection)
                        {
                            token.Value.Connection.Send(message);
                        }                     
                        Thread.Sleep(200);
                        result++;
                    }
                    else
                    {
                        CloseClient(token.Value);
                    }

                }
            }
            catch { };
            return result;
        }

        public void SendAsync(string ipAndport, string message)
        {
            if (string.IsNullOrEmpty(ipAndport)) return;
            if (string.IsNullOrEmpty(message)) return;
            var sendTmpBuffer = Encoding.UTF8.GetBytes(message);
            SendAsync(ipAndport, sendTmpBuffer);
        }

        public void SendAsync(string ipAndport, byte[] message)
        {
            if (string.IsNullOrEmpty(ipAndport)) return;
            if (null == message) return;
            var toaken = _tokens.GetToken(ipAndport);
            if (toaken == null) throw new ArgumentNullException("toaken");
            if (toaken.Connection.Connected)
            {
                //Array.Copy(message, _sendBuffer, message.Length);
                var args = _writePool.Pop();
                args.UserToken = toaken;
                args.RemoteEndPoint = toaken.Connection.RemoteEndPoint;
                args.SetBuffer(message, 0, message.Length);
                // Start sending asyncronally.
                if (!toaken.Connection.SendAsync(args))
                {
                    ProcessSend(args);
                }
            }
            else
            {
                throw new SocketException((Int32)SocketError.NotConnected);
            }
        }

        public void CloseClient(string ipAndPort)
        {
            var token = _tokens.GetToken(ipAndPort);
            CloseClient(token);
        }

        private void CloseClient(Token token)
        {
            _tokens.Remove(token.Connection.RemoteEndPoint.ToString());
            token.Dispose();

            var readEventArg = new SocketAsyncEventArgs();
            readEventArg.Completed += OnIoCompleted;
            readEventArg.SetBuffer(new Byte[this._bufferSize], 0, this._bufferSize);
            // Add SocketAsyncEventArg to the pool.
            this._readPool.Push(readEventArg);

            // Decrement the counter keeping track of the total number of clients connected to the server.
            this._semaphoreAcceptedClients.Release();

            Interlocked.Decrement(ref this._numConnectedSockets);
            Console.WriteLine(
                "A client has been disconnected from the server. There are {0} clients connected to the server",
                this._numConnectedSockets);

            // Free the SocketAsyncEventArg so they can be reused by another client.
        }

        /// <summary>
        /// Close the socket associated with the client.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the completed send/receive operation.</param>
        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            Token token = e.UserToken as Token;
            this.CloseClientSocket(token, e);
        }

        private void CloseClientSocket(Token token, SocketAsyncEventArgs e)
        {
            try
            {
                _tokens.Remove(token.Connection.RemoteEndPoint.ToString());
                token.Dispose();
            }
            catch (Exception)
            {
                
            }


            // Decrement the counter keeping track of the total number of clients connected to the server.
            this._semaphoreAcceptedClients.Release();
            Interlocked.Decrement(ref this._numConnectedSockets);
            Console.WriteLine("A client has been disconnected from the server. There are {0} clients connected to the server", this._numConnectedSockets);

            // Free the SocketAsyncEventArg so they can be reused by another client.
            this._readPool.Push(e);
        }

        /// <summary>
        /// Callback method associated with Socket.AcceptAsync 
        /// operations and is invoked when an accept operation is complete.
        /// </summary>
        /// <param name="sender">Object who raised the event.</param>
        /// <param name="e">SocketAsyncEventArg associated with the completed accept operation.</param>
        private void OnAcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            this.ProcessAccept(e);
        }

        /// <summary>
        /// Callback called whenever a receive or send operation is completed on a socket.
        /// </summary>
        /// <param name="sender">Object who raised the event.</param>
        /// <param name="e">SocketAsyncEventArg associated with the completed send/receive operation.</param>
        private void OnIoCompleted(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                switch (e.LastOperation)
                {
                    case SocketAsyncOperation.Receive:
                        this.ProcessReceive(e);
                        break;
                    case SocketAsyncOperation.Send:
                        this.ProcessSend(e);
                        break;
                    default:
                        throw new ArgumentException("The last operation completed on the socket was not a receive or send");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error when processing data received from {0}:\r\n{1}", "OnIoCompleted", ex.ToString());
            }
            // Determine which type of operation just completed and call the associated handler.

        }

        /// <summary>
        /// Process the accept for the socket listener.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the completed accept operation.</param>
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            Socket s = e.AcceptSocket;
            if (_isOnLine)
            {
                try
                {
                    SocketAsyncEventArgs readEventArgs = this._readPool.Pop();
                    if (readEventArgs != null)
                    {
                        // Get the socket for the accepted client connection and put it into the 
                        // ReadEventArg object user token.
                        var token = new Token(s, this._bufferSize);
                        readEventArgs.UserToken = token;
                        readEventArgs.RemoteEndPoint = s.RemoteEndPoint;
                        _tokens.Add(s.RemoteEndPoint.ToString(), token);
                        Interlocked.Increment(ref this._numConnectedSockets);
                        System.Diagnostics.Debug.WriteLine("Client connection accepted. There are {0} clients connected to the server",
                            this._numConnectedSockets);
                        SocketRev.AcceptClient(s.RemoteEndPoint.ToString());
                        if (!s.ReceiveAsync(readEventArgs))
                        {
                            this.ProcessReceive(readEventArgs);
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("There are no more available sockets to allocate.");
                    }
                }
                catch (SocketException ex)
                {
                    Token token = e.UserToken as Token;
                    System.Diagnostics.Debug.WriteLine("Error when processing data received from {0}:\r\n{1}", token.Connection.RemoteEndPoint, ex.ToString());
                    this.ProcessError(e);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    this.ProcessError(e);
                }

                // Accept the next connection request.
                this.StartAccept(e);
            }

        }

        private void ProcessError(SocketAsyncEventArgs e)
        {
            Token token = e.UserToken as Token;
            IPEndPoint localEp = token.Connection.LocalEndPoint as IPEndPoint;

            this.CloseClientSocket(token, e);

            System.Diagnostics.Debug.WriteLine("Socket error {0} on endpoint {1} during {2}.", (Int32)e.SocketError, localEp, e.LastOperation);
        }

        /// <summary>
        /// This method is invoked when an asynchronous receive operation completes. 
        /// If the remote host closed the connection, then the socket is closed.  
        /// If data was received then the data is echoed back to the client.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the completed receive operation.</param>
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            // Check if the remote host closed the connection.
            if (e.BytesTransferred > 0)
            {
                if (e.SocketError == SocketError.Success)
                {
                    Token token = e.UserToken as Token;
                    token.SetData(e);

                    if (null != SocketRev)
                    {
                        var revTmpBuffer = token.GetRecevieData();
                        try
                        {
                            SocketRev.ReceiveData(e.RemoteEndPoint.ToString(), revTmpBuffer.ToArray());
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Error when processing data received from {0}:\r\n{1}", token.Connection.RemoteEndPoint, ex.ToString());
                        }

                    }
                    if (!token.Connection.ReceiveAsync(e))
                    {
                        // Read the next block of data sent by client.
                        this.ProcessReceive(e);
                    }
                }
                else
                {
                    this.ProcessError(e);
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// This method is invoked when an asynchronous send operation completes.  
        /// The method issues another receive on the socket to read any additional 
        /// data sent from the client.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the completed send operation.</param>
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                //// Done echoing data back to the client.
                //Token token = e.UserToken as Token;

                //if (!token.Connection.ReceiveAsync(e))
                //{
                //    // Read the next block of data send from the client.
                //    this.ProcessReceive(e);
                //}
            }
            else
            {
                this.ProcessError(e);
            }
            this._writePool.Push(e);
        }

        /// <summary>
        /// Starts the server listening for incoming connection requests.
        /// </summary>
        /// <param name="localEndPoint"></param>
        public void Start(IPEndPoint localEndPoint)
        {

            // Create the socket which listens for incoming connections.
            this._listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this._listenSocket.ReceiveBufferSize = this._bufferSize;
            this._listenSocket.SendBufferSize = this._bufferSize;

            if (localEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
            {
                // Set dual-mode (IPv4 & IPv6) for the socket listener.
                // 27 is equivalent to IPV6_V6ONLY socket option in the winsock snippet below,
                this._listenSocket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName) 27, false);
                this._listenSocket.Bind(new IPEndPoint(IPAddress.IPv6Any, localEndPoint.Port));
            }
            else
            {
                // Associate the socket with the local endpoint.
                this._listenSocket.Bind(new IPEndPoint(IPAddress.Any, localEndPoint.Port));
            }
            _isOnLine = true;
            // Start the server.
            this._listenSocket.Listen(this._numConnections);

            // Post accepts on the listening socket.
            this.StartAccept(null);

            // Blocks the current thread to receive incoming messages.

        }

        /// <summary>
        /// Begins an operation to accept a connection request from the client.
        /// </summary>
        /// <param name="acceptEventArg">The context object to use when issuing 
        /// the accept operation on the server's listening socket.</param>
        private void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += OnAcceptCompleted;
            }
            else
            {
                // Socket must be cleared since the context object is being reused.
                acceptEventArg.AcceptSocket = null;
            }
            //限制最大连接数
            this._semaphoreAcceptedClients.WaitOne();
            if (!this._listenSocket.AcceptAsync(acceptEventArg))
            {
                this.ProcessAccept(acceptEventArg);
            }
        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        public void Stop()
        {
            _tokens.CloseAll();
            _isOnLine = false;
            this._listenSocket.Close();
            this._listenSocket.Dispose();
            this._listenSocket = null;
        }
    }
}
