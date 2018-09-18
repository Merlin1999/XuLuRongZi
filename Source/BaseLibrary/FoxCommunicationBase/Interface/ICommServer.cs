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

        int SendMsg(List<IJsonObj> msg, string ipAndport);

        void CloseClient(string ipAndPort);
    }
}
