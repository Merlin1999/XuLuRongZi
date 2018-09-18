using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;

namespace FoxBaseSocket.Infrastructure
{
    delegate void ProcessData(SocketAsyncEventArgs args);

    /// <summary>
    /// 可以用于SocketAsyncEventArgs的一个“Token”类
    /// </summary>
    internal sealed class Token : IDisposable
    {
        private readonly Socket _connection;

        private readonly List<byte> _revData;

        private Int32 _currentIndex;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connection">接入（accept）的Socket对象</param>
        /// <param name="bufferSize">此链接的缓存大小</param>
        internal Token(Socket connection, Int32 bufferSize)
        {
            this._connection = connection;
            _revData = new List<byte>(bufferSize);
        }

        /// <summary>
        /// Accept socket.
        /// </summary>
        internal Socket Connection
        {
            get { return this._connection; }
        }


        /// <summary>
        /// 将套接字中传输的数据存入接收缓存
        /// </summary>
        /// <param name="args">SocketAsyncEventArgs used in the operation.</param>
        internal void SetData(SocketAsyncEventArgs args)
        {
            Int32 count = args.BytesTransferred;
            if (count > this._revData.Capacity)
            {
                throw new ArgumentOutOfRangeException("count",
                    String.Format(CultureInfo.CurrentCulture, "Adding {0} bytes on buffer , the listener buffer will overflow.", count));
            }
            if ((this._currentIndex + count) > this._revData.Capacity)
            {
                _revData.RemoveRange(0, count);
            }
            var revBuffer = new byte[count];
            Array.Copy(args.Buffer, args.Offset, revBuffer, 0, count);
            _revData.AddRange(revBuffer);
            this._currentIndex += count;
        }

        /// <summary>
        /// 获取接收缓存中的所有数据
        /// </summary>
        /// <returns></returns>
        internal List<byte> GetRecevieData()
        {
            var lst = new List<byte>(_revData.Count);
            lst.AddRange(_revData);
            _revData.Clear();
            this._currentIndex = 0;
            return lst;
        }


        #region IDisposable Members

        /// <summary>
        /// Release instance.
        /// </summary>
        public void Dispose()
        {
            try
            {
                this._connection.Shutdown(SocketShutdown.Send);
            }
            catch (Exception)
            {
                // Throw if client has closed, so it is not necessary to catch.
            }
            finally
            {
                if (_connection.Connected)
                this._connection.Close();
            }
        }

        #endregion
    }
}
