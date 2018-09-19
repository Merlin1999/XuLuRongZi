using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace FoxCoreUtility.Files
{
    public class ZipHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toZip">要压缩的对象</param>
        /// <param name="zipFile">压缩产生的文件</param>
        /// <param name="zipMsg"></param>
        /// <returns></returns>
        public static bool GreatZipFile(string toZip, string zipFile,out string zipMsg)
        {
            if (!System.IO.Directory.Exists(toZip) && !System.IO.File.Exists(toZip))
            {
                zipMsg = "The directory or file does not exist!";
                return false;
            }
            if (System.IO.File.Exists(zipFile))
            {
                zipMsg = "That zipfile already exists!";
                return false;
            }
            if (!zipFile.EndsWith(".zip"))
            {
                zipMsg="The filename must end with .zip!";
                return false;
            }
            string directoryToZip = toZip;
            string zipFileToCreate = zipFile;
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.StatusMessageTextWriter = System.Console.Out;
                    zip.AddDirectory(directoryToZip); // recurses subdirectories
                    zip.Save(zipFileToCreate);
                }
            }
            catch (System.Exception ex1)
            {
                zipMsg = "exception: " + ex1;
                return false;
            }
            zipMsg = "Zip successed!";
            return true;
        }
    }
}
