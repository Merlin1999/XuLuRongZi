using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using FoxBaseSocket.Infrastructure;
using FoxBaseSocket.Interface;

namespace FoxBaseSocket.TCP
{
    /// <summary>
    /// Implements the connection logic for the socket client.
    /// </summary>
    public sealed class SocketClient : IDisposable
    {
        /// <summary>
        /// Constants for socket operations.
        /// </summary>
        //private const Int32 ReceiveOperation = 1, SendOperation = 0;

        /// <summary>
        /// Signals the send/receive operation.
        /// </summary>
        //private AutoResetEvent[] autoSendReceiveEvents = new AutoResetEvent[]
        //{
        //    new AutoResetEvent(false),
        //    new AutoResetEvent(false)
        //};
        /// <summary>
        /// The socket used to send/receive messages.
        /// </summary>
        private readonly Socket _clientSocket;

        /// <summary>
        /// Flag for connected socket.
        /// </summary>
        private Boolean _connected = false;

        /// <summary>
        /// Listener endpoint.
        /// </summary>
        private readonly IPEndPoint _hostEndPoint;

        //private SocketAsyncEventArgs _sendCompleteArgs;
        private SocketAsyncEventArgs _receiveCompleteArgs;
        private byte[] _sendBuffer; 
        private byte[] _receiveBuffer;
        private bool _disposed = false;
        private readonly int _maxBufferSize;
        private readonly int _poolNum;
        private SocketAsyncEventArgsPool _sendpool;

        public ISocketRev SocketRev { private get; set; }

        /// <summary>
        /// Flag for connected socket.
        /// </summary>
        public bool Connected
        {
            get { return _connected; }
        }

        /// <summary>
        /// Create an uninitialized client instance.  
        /// To start the send/receive processing
        /// call the Connect method followed by SendReceive method.
        /// </summary>
        /// <param name="hostName">Name of the host where the listener is running.</param>
        /// <param name="port">Number of the TCP port from the listener.</param>
        public SocketClient(string hostName, Int32 port)
        {
            // Get host related information.
            IPHostEntry host = Dns.GetHostEntry(hostName);

            // Addres of the host.
            IPAddress[] addressList = host.AddressList;

            // Instantiates the endpoint and socket.
            this._hostEndPoint = new IPEndPoint(addressList[addressList.Length - 1], port);
            this._clientSocket = new Socket(this._hostEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _maxBufferSize = 1024;
            _poolNum = 10;
            InitSendCompleteArgs();
            InitReceiveCompleteArgs();
        }



        private void InitSendCompleteArgs()
        {
            _sendBuffer = new byte[_maxBufferSize];
            _sendpool = new SocketAsyncEventArgsPool(_poolNum);
            for (int i = 0; i < _poolNum; i++)
            {
                 var sendCompleteArgs = new SocketAsyncEventArgs();
                sendCompleteArgs.SetBuffer(_sendBuffer, 0, _sendBuffer.Length);
                sendCompleteArgs.UserToken = this._clientSocket;
                sendCompleteArgs.RemoteEndPoint = this._hostEndPoint;
                sendCompleteArgs.Completed += OnSend;
                _sendpool.Push(sendCompleteArgs);
            }
        }

        private void InitReceiveCompleteArgs()
        {
            _receiveBuffer = new byte[_maxBufferSize];
            _receiveCompleteArgs = new SocketAsyncEventArgs();
            _receiveCompleteArgs.SetBuffer(_receiveBuffer, 0, _receiveBuffer.Length);
            _receiveCompleteArgs.UserToken = this._clientSocket;
            _receiveCompleteArgs.RemoteEndPoint = this._hostEndPoint;
            _receiveCompleteArgs.Completed += OnReceive;
        }
        /// <summary>
        /// Connect to the host.
        /// </summary>
        /// <returns>True if connection has succeded, else false.</returns>
        public void Connect()
        {
            var connectArgs = new SocketAsyncEventArgs();

            connectArgs.UserToken = this._clientSocket;
            connectArgs.RemoteEndPoint = this._hostEndPoint;
            connectArgs.Completed += OnConnect;

            if (!_clientSocket.ConnectAsync(connectArgs))
            {
                OnConnect(this, connectArgs);
            }
            //autoConnectEvent.WaitOne();

        }

        /// <summary>
        /// Disconnect from the host.
        /// </summary>
        public void Disconnect()
        {
            _clientSocket.Disconnect(false);
        }

        public int Send(string message)
        {
            if (string.IsNullOrEmpty(message)) return 0;
            var sendTmpBuffer = Encoding.UTF8.GetBytes(message);
            return Send(sendTmpBuffer);
        }

        public int Send(byte[] message)
        {
            if (null == message) return 0;
            if (this.Connected)
            {
                return _clientSocket.Send(message);
            }
            else
            {
                throw new SocketException((Int32)SocketError.NotConnected);
            }
        }
        /// <summary>
        /// Exchange a message with the host.
        /// </summary>
        /// <param name="message">Message to send.</param>
        /// <returns>Message sent by the host.</returns>
        public void SendAsync(string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            var sendTmpBuffer = Encoding.UTF8.GetBytes(message);
            SendAsync(sendTmpBuffer);
        }

        public void SendAsync(byte[] message)
        {
            if (null == message) return;
            if (this.Connected)
            {
                //InitSendCompleteArgs();
                Array.Copy(message, _sendBuffer, message.Length);
                var args = _sendpool.Pop();
                if (null == args)
                {
                    Console.WriteLine("掉数据");
                    return;
                }
                args.SetBuffer(_sendBuffer, 0, message.Length);
                // Start sending asyncronally.
                if (!_clientSocket.SendAsync(args))
                {
                    OnSend(this, args);
                }
            }
            else
            {
                throw new SocketException((Int32)SocketError.NotConnected);
            }
        }


        private void OnSend(object sender, SocketAsyncEventArgs e)
        {
            // Signals the end of send.
            //autoSendReceiveEvents[ReceiveOperation].Set();

            if (e.SocketError == SocketError.Success)
            {
                if (e.LastOperation == SocketAsyncOperation.Send)
                {
                    
                    // Prepare receiving.
                    //Socket s = e.UserToken as Socket;

                    //byte[] receiveBuffer = new byte[255];
                    //e.SetBuffer(receiveBuffer, 0, receiveBuffer.Length);
                    //e.Completed += new EventHandler<SocketAsyncEventArgs>(OnReceive);
                    //s.ReceiveAsync(e);
                }
            }
            else
            {
                this.ProcessError(e);
            }
            _sendpool.Push(e);
        }

        private void OnConnect(object sender, SocketAsyncEventArgs e)
        {
            // Set the flag for socket connected.
            this._connected = (e.SocketError == SocketError.Success);
            if (e.SocketError != SocketError.Success)
            {
                throw new SocketException((Int32)e.SocketError);
            }
            else
            {
                var s = e.UserToken as Socket;
                //InitReceiveCompleteArgs();
                if (!s.ReceiveAsync(_receiveCompleteArgs))
                {
                    OnReceive(this, _receiveCompleteArgs);
                }
            }
        }

        private void OnReceive(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                var lst = new byte[e.BytesTransferred];
                Array.Copy(e.Buffer, e.Offset, lst, 0, e.BytesTransferred);
                if (null != SocketRev)
                {
                    SocketRev.ReceiveData(e.RemoteEndPoint.ToString(),lst);
                }
                var s = e.UserToken as Socket;
                //InitReceiveCompleteArgs();
                if (!s.ReceiveAsync(_receiveCompleteArgs))
                {
                    OnReceive(this, _receiveCompleteArgs);
                }
            }
            else
            {
                this.ProcessError(e);
            }
        }


        /// <summary>
        /// Close socket in case of failure and throws a SockeException according to the SocketError.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the failed operation.</param>
        private void ProcessError(SocketAsyncEventArgs e)
        {
            Socket s = e.UserToken as Socket;
            if (s.Connected)
            {
                // close the socket associated with the client
                try
                {
                    s.Shutdown(SocketShutdown.Both);
                }
                catch (Exception)
                {
                    // throws if client process has already closed
                }
                finally
                {
                    this._sendpool.Push(e);
                    if (s.Connected)
                    {
                        s.Close();
                    }
                }
            }

            // Throw the SocketException
            throw new SocketException((Int32)e.SocketError);
        }

        /// <summary>
        /// 必须，以备程序员忘记了显式调用Dispose方法
        /// </summary>
        ~SocketClient()
        {
            //必须为false
            Dispose(false);
        }

        #region IDisposable Members

        /// <summary>
        /// Disposes the instance of SocketClient.
        /// </summary>
        public void Dispose()
        {
            //必须为true
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// 非密封类修饰用protected virtual
        /// 密封类修饰用private
        /// </summary>
        /// <param name="disposing"></param>
        protected  void Dispose(bool disposing)
        {

            if (_disposed)
            {
                return;
            }
            if (disposing)
            {

            }
            //autoSendReceiveEvents[SendOperation].Close();
            //autoSendReceiveEvents[ReceiveOperation].Close();
            if (this._clientSocket.Connected)
            {
                this._clientSocket.Close();
            }
            //让类型知道自己已经被释放
            _disposed = true;
        }

    }
}
