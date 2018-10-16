using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClientTest
{
    class BaseHttpJson
    {
        static string strHttpJsonUrl = "http://localhost:58681//WebService1.asmx/";

        static public string HttpPost(string Url, string postDataStr)
        {
            string retString="";
            Url = strHttpJsonUrl + Url;
            try
            {          
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded;charset=gb2312";
                CookieContainer cookie = new CookieContainer();

                request.CookieContainer = cookie;
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream myResponseStream = response.GetResponseStream();

                XmlTextReader Reader = new XmlTextReader(myResponseStream);
                Reader.MoveToContent();
                retString = Reader.ReadInnerXml();//取出Content中的Json数据
                Reader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return "ERROR";
            }
            return retString;
        }
    }
}
