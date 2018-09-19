using System;
using System.IO;

namespace KT.Utility.Files
{
    public class FileConvert
    {
        /// <summary>
        /// Files the content.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public byte[] ConvertToByte(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);
                return buffur;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    //关闭资源
                    fs.Close();
                }
            }
        }


        public void ConvertToFile(string path, byte[] bytes)
        {
            var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
               fs.Write(bytes, 0, bytes.Length);
            fs.Close();         
        }
    }
}
