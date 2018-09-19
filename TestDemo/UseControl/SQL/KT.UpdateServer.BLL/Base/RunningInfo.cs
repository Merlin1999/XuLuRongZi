using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;
using KT.Utility.Files;

namespace KT.UpdateServer.BLL.Base
{
    /// <summary>
    /// 全局运行参数
    /// </summary>
    public class RunningInfo
    {
        private static IPEndPoint _endPoint = null;
        private static string _runningDir=string.Empty;
        private static int? _maxConnection = null;
        private static int _maxFtpDownLoadNum;

        /// <summary>
        /// 获取服务ip
        /// </summary>
        /// <returns></returns>
        public static IPEndPoint GetLocalEndPoint()
        {
            if (_endPoint != null)
                return _endPoint;
            var node= XmlHelper.GetXmlNodeByXpath(RunningDir() + "\\Config\\App.xml", "//AppConfig//EndPoint");
            try
            {
                LoadConfigInfo(node);
            }
            catch (Exception e)
            {

                return null;
            }
            return _endPoint;
        }

        /// <summary>
        /// 获取最大连接数
        /// </summary>
        /// <returns></returns>
        public static int GetMaxConnection()
        {
            if (_maxConnection != null)
                return _maxConnection.Value;
            var node = XmlHelper.GetXmlNodeByXpath(RunningDir() + "\\Config\\App.xml", "//AppConfig//EndPoint");
            try
            {
                LoadConfigInfo(node);
            }
            catch (Exception e)
            {

                return -1;
            }
            return _maxConnection.Value;
        }


        /// <summary>
        /// 获取FTP最大下载数
        /// </summary>
        /// <returns></returns>
        public static int GetMaxFtpDownLoadNum()
        {
            if (_maxFtpDownLoadNum <= 0)
                return _maxFtpDownLoadNum;
            var node = XmlHelper.GetXmlNodeByXpath(RunningDir() + "\\Config\\App.xml", "//AppConfig//EndPoint");
            try
            {
                LoadConfigInfo(node);
            }
            catch (Exception e)
            {

                return 10;
            }
            return _maxFtpDownLoadNum;
        }

        private static void LoadConfigInfo(XmlNode node)
        {
            if (node.Attributes != null)
            {
                int port = int.Parse(node.Attributes["Port"].Value);
                var ip = IPAddress.Any;
                _maxConnection = int.Parse(node.Attributes["MaxConnection"].Value);
                _maxFtpDownLoadNum = int.Parse(node.Attributes["MaxFtpDownLoadNum"].Value);
                _endPoint = new IPEndPoint(ip, port);
            }
        }

        


        public static string RunningDir()
        {
            if (!string.IsNullOrWhiteSpace(_runningDir))
                return _runningDir;
            _runningDir=Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return _runningDir;
        }
    }
}
