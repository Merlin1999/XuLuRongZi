using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace KT.Utility.Files
{
    public class IniFile
    {
        //写INI文件 
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);

        //读ini文件（字符 
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        //读ini文件（数字 
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileInt(string section, string key, int def, string filePath);

        private readonly string _fileName;


        public IniFile(string filename)
        {
            _fileName = filename;
        }

        public int ReadInt(string section, string key, int def)
        {
            return GetPrivateProfileInt(section, key, def, _fileName);
        }

        public string ReadString(string section, string key, string def)
        {
            byte[] temp = new byte[1024];
            int i = GetPrivateProfileString(section, key, def, temp, temp.GetUpperBound(0), _fileName);
            string s = Encoding.GetEncoding(0).GetString(temp, 0, i);
            return s;
        }

        public void WriteInt(string section, string key, int iVal)
        {
            WritePrivateProfileString(section, key, iVal.ToString(), _fileName);
        }

        public void WriteString(string section, string key, string strVal)
        {
            WritePrivateProfileString(section, key, strVal, _fileName);
        }

        public void DelKey(string section, string key)
        {
            WritePrivateProfileString(section, key, null, _fileName);
        }

        public void DelSection(string section)
        {
            WritePrivateProfileString(section, null, null, _fileName);
        }

        public IList<string> ReadKeysOnSection(string section)
        {
            string buffer = ReadString(section, null, null);
            return GetStringsFromBuffer(buffer);
        }

        public IList<string> ReadSections()
        {
            string buffer = ReadString(null, null, null);
            return GetStringsFromBuffer(buffer);
        }

        private IList<string> GetStringsFromBuffer(string buffer)
        {
            var strings = new List<string>();
            if (buffer.Length != 0)
            {
                string[] split = buffer.Split(new char[] { '\0' });
                strings.AddRange(split);
            }
            return strings;
        }

    }
}
