using FoxCoreUtility.Files;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OYHttpClient.HttpBase
{
    class BaseHttpJson
    {
        static string StrHttpJsonUrl = "";


        public static void InitHttp(String serverName)
        {
            if(string.IsNullOrWhiteSpace(StrHttpJsonUrl))
            {
                //获取程序所在路径
                var path = AppDomain.CurrentDomain.BaseDirectory;
                //获取配置文件中的所有类节点
                var nodes = XmlHelper.GetXmlNodeListByXpath(path + @"\Config\HttpServer.cfg", "//Servers//Server");
                if (nodes != null)
                {
                    //遍历所有节点，找到输入的类别名对应的节点
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        var node = nodes.Item(i);
                        if (node.Attributes["name"].InnerText != serverName)
                        {
                            continue;
                        }
                        if (!string.IsNullOrWhiteSpace(node.Attributes["url"].InnerText))
                        {
                            StrHttpJsonUrl = node.Attributes["url"].InnerText;
                        }
                    }
                }
            }
        }



        static public string HttpPost(string Url, string postDataStr)
        {
            InitHttp("XinLuRongZi");
            string retString="";
            if (string.IsNullOrWhiteSpace(StrHttpJsonUrl))
                return "NULL URL";

            Url = StrHttpJsonUrl + Url;
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
