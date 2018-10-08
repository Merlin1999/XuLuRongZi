using FoxBaseSocket.Interface;
using FoxCommunicationBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;



namespace FoxCommunicationBase.Interface
{
    public interface ICommServer : IDisposable, ISocketRev
    {
        void InitComm(IPEndPoint localEndPoint, int maxConnect, Action<byte[], string> onRevMsg, Action<string> onAccept);

        int SendServerPushMsg(List<IJsonObj> msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ipAndport">格式如：“192.168.1.1：8000”</param>
        /// <returns></returns>
        int SendMsg(List<IJsonObj> msg, string ipAndport);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAndPort">格式如：“192.168.1.1：8000”</param>
        void CloseClient(string ipAndPort);
    }
}
