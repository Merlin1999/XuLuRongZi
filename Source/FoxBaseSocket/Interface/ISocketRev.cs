
using System.Collections.Generic;

namespace FoxBaseSocket.Interface
{
    /// <summary>
    /// 从异步socket中接收数据的接口
    /// </summary>
    public interface ISocketRev
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAndPort">格式如"127.0.0.1：8000"</param>
        /// <param name="datas"></param>
        void ReceiveData(string ipAndPort, byte[] datas);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAndPort">格式如"127.0.0.1：8000"</param>
        void AcceptClient(string ipAndPort);
    }
}
