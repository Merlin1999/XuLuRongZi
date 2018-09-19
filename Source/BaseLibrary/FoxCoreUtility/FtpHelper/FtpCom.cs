using System;
using System.Collections;
using System.IO;
using System.Threading;
using FtpLib;

namespace FoxCoreUtility.FtpHelper
{
    public class FtpCom : IDisposable
    {
        private readonly FtpConnection _ftp;
        private bool _disposed = false;
        private bool _isError = false;

        public bool IsError
        {
            get { return _isError; }
        }

        public FtpCom(string hostIP,int hostPort, string userName, string passWord)
        {
            this._ftp = new FtpConnection(hostIP, hostPort, userName, passWord);
            _isError = !ConnectFtpHost(this._ftp);
        }

        private bool ConnectFtpHost(FtpConnection ftpConnection)
        {
            try
            {
                if (ftpConnection != null)
                {
                    ftpConnection.Open(); /* Open the FTP connection */
                    ftpConnection.Login(); /* Login using previously provided credentials */
                    ftpConnection.SetCurrentDirectory(".");
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DownLoadFile(string remoteFile, string localFile)
        {
            if (_isError)
            {
                _isError = !ConnectFtpHost(this._ftp);
            }
            if (_isError)
                return false;

            if (_ftp != null)
            {
                if (_ftp.FileExists(remoteFile))
                {
                    _ftp.GetFile(localFile, remoteFile, false);
                    return true;                   
                }

            }
            return false;
        }



        public bool UploadFiles(string localFile,string remoteFile)
        {
            if (_isError)
            {
                _isError = !ConnectFtpHost(this._ftp);
            }
            if (_isError)
                return false;

            if (_ftp != null)
            {
                try
                {
                    if (!_ftp.DirectoryExists(Path.GetDirectoryName(remoteFile)))
                        _ftp.CreateDirectory(Path.GetDirectoryName(remoteFile));
                    _ftp.PutFile(localFile, remoteFile);
                    return true;
                }
                catch (Exception)
                {

                    return false; 
                }

            }
            return false;
        }

        //public void UploadFilesInFolder(string folderName, string fileToUpExtension)
        //{
        //    try
        //    {
        //        int i = 0;
        //        //string[] strFiles = Directory.GetFiles(@"G:\ftpClientFolder", "*.dat");
        //        string[] strFiles = Directory.GetFiles(folderName, fileToUpExtension);
        //        foreach (string strFile in strFiles)
        //        {
        //            if (_ftp != null)
        //            {
        //                _ftp.PutFile(strFile);
        //                FileInfo fi = new FileInfo(strFile);
        //                if (fi != null)
        //                {
        //                    fi.Delete();
        //                }
        //                i++;
        //                Console.WriteLine("i={0},strFile={1}", i, fi.Attributes.ToString());
        //            }
        //        }
        //    }
        //    catch (FtpException e)
        //    {
        //        //需要上报错误吗？
        //        Console.WriteLine(String.Format("FTP Error: {0} {1}", e.ErrorCode, e.Message));
        //    }
        //}

        public void CloseFtp()
        {
            _ftp.Close();
        }

        public void Dispose()
        {
            //必须为true
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        ///<summary>
        /// 必须，以备程序员忘记了显式调用Dispose方法
        ///</summary>
        ~FtpCom()
        {
            //必须为false
            Dispose(false);
        }

        ///<summary>
        /// 非密封类修饰用protected virtual
        /// 密封类修饰用private
        ///</summary>
        ///<param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                // 清理托管资源

            }
            // 清理非托管资源
            _ftp.Close();

            //让类型知道自己已经被释放
            _disposed = true;
        }
    }
}
