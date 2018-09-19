using System.Security.Cryptography;
using System.Text;

namespace FoxCoreUpdateServer.Security
{
    public static class MD5Helper
    {

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串（编码默认UTF8）</param>
        /// <returns></returns>
        public static string Md5Encrypt(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();;
            byte[] t = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            return sb.ToString();
        }
    }
}
