using FoxBaseSocket.Interface;
using FoxCommunicationBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace FoxCommunicationBase.Interface
{
    public interface ICommClient : IDisposable, ISocketRev
    {
        void InitComm(IPEndPoint remoteEndPoint,Action<byte[], string> onRevMsg);

        void Connect();

        int SendMsg(List<IJsonObj> msg);

        void Close();

        bool IsConnected();
    }
}
