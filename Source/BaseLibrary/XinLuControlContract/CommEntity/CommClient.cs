using FoxBaseSocket.TCP;
using FoxCommunicationBase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace FoxCommunicationBase.CommEntity
{
    public class CommClient : ICommClient
    {


        private Action<byte[], string> _onRevMsg;

        private SocketClient _client;

        private bool _disposed = false;


        public bool IsConnected()
        {
            return _client.Connected;
        }


        public void InitComm(System.Net.IPEndPoint remoteEndPoint, Action<byte[], string> onRevMsg)
        {
            _client = new SocketClient(remoteEndPoint.Address.ToString(), remoteEndPoint.Port) { SocketRev = this };
             _onRevMsg = onRevMsg;
        }

        public void Connect()
        {
            _client.Connect();
        }

        public int SendMsg(List<Protocol.IJsonObj> msg)
        {
            string data = "";
            JavaScriptSerializer js = new JavaScriptSerializer();
            StringBuilder jsonStrBuilder = null;
            js.Serialize(msg, jsonStrBuilder);
            byte[] msgData = Encoding.UTF32.GetBytes(jsonStrBuilder.ToString());
            return _client.Send(msgData);
        }

        public void Close()
        {
            _client.Disconnect();
        }

        public void ReceiveData(string ipAndPort, byte[] datas)
        {
            if (_onRevMsg != null)
            {
                _onRevMsg.BeginInvoke(datas, ipAndPort, null, null);
            }
        }

        public void AcceptClient(string ipAndPort)
        {
            throw new NotImplementedException();
        }




        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        ~CommClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// 非密封类修饰用protected virtual
        /// 密封类修饰用private
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {

            if (disposing) // 清理托管资源
            {
                //没有，略            
            }

            // 清理非托管资源

            _client.Disconnect();
            //让类型知道自己已经被释放
            _disposed = true;
        }

    }
}
