using FoxCoreUtility.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoxIocProxy
{
    public static class DynamicProxy<T> where T:class
    {
        private static ProxyBase<T> proxyBase = null;

        public static T GetInstance(string name)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var nodes = XmlHelper.GetXmlNodeListByXpath(path+
                @"\Config\FoxIocProxy.cfg.xml"
, "//assembly//Object");
            for(int i =0;i<nodes.Count;i++)
            {
                var node = nodes.Item(i);
                if( node.Attributes["name"].InnerText == name)
                {
                    proxyBase.InitProxy(node.Attributes["path"].InnerText, node.Attributes["typeName"].InnerText);
                }
            }

            return proxyBase.Instance;
        }
    }
}
