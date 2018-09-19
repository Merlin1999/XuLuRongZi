using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.UpdateServer.BLL
{
    public class GlobleRunningTime
    {
        private static string _ftpServerId = null;

        public static string FtpServerId
        {
            get { return _ftpServerId; } 
        }

        public static void SetFtpServer(string id)
        {
                _ftpServerId = id;
        }
    }
}
