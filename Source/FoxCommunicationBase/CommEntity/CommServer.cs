using FoxBaseSocket.TCP;
using FoxCommunicationBase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace FoxCommunicationBase.CommEntity
{
    public class CommServer:ICommServer
    {

        private SocketListener _listener;
        private Action<byte[], string> _onRevMsg;
        private Action<string> _onAccept;


        private bool _disposed = false;

        public void InitComm(System.Net.IPEndPoint localEndPoint, int maxConnect, Action<byte[], string> onRevMsg, Action<string> onAccept)
        {
            _listener = new SocketListener(maxConnect, 8096) { SocketRev = this };
            _listener.Start(localEndPoint);
            _onRevMsg = onRevMsg;
            _onAccept = onAccept;
        }

        public int SendServerPushMsg(List<Protocol.IJsonObj> msg)
        {
            throw new NotImplementedException();
        }

        public int SendMsg(List<Protocol.IJsonObj> msg, string ipAndport)
        {
            string data = "";
            JavaScriptSerializer js = new JavaScriptSerializer();
            StringBuilder jsonStrBuilder=new StringBuilder();
            js.Serialize(msg, jsonStrBuilder);
            byte[] msgData = Encoding.UTF32.GetBytes(jsonStrBuilder.ToString());

            return _listener.Send(ipAndport, msgData);
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
            if (_onAccept != null)
            {
                _onAccept.BeginInvoke(ipAndPort, null, null);
            }
        }


        public void CloseClient(string ipAndPort)
        {
            _listener.CloseClient(ipAndPort);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        ~CommServer()
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

            _listener.Stop();
            //让类型知道自己已经被释放
            _disposed = true;
        }
    }
}
