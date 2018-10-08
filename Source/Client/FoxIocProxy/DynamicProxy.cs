using FoxCoreUtility.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoxIocProxy
{
    /// <summary>
    /// 动态类型代理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class DynamicProxy<T> where T:class
    {
        /// <summary>
        /// 用于保存类型别名对应的类代理
        /// </summary>
        private static Dictionary<string, ProxyBase<T>> proxyDic = new Dictionary<string, ProxyBase<T>>();

        /// <summary>
        /// 获取类的实例
        /// </summary>
        /// <param name="name">类别名</param>
        /// <returns></returns>
        public static T GetInstance(string name)
        {
            //获取程序所在路径
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //获取配置文件中的所有类节点
            var nodes = XmlHelper.GetXmlNodeListByXpath(path+@"\Config\FoxIocProxy.cfg.xml", "//assembly//Object");

            if (nodes != null)
            {
                //遍历所有节点，找到输入的类别名对应的节点
                for (int i = 0; i < nodes.Count; i++)
                {
                    var node = nodes.Item(i);
                    if (node.Attributes["name"].InnerText != name)
                    {
                        continue;
                    }
                    if(string.IsNullOrWhiteSpace(node.Attributes["path"].InnerText)
                        || string.IsNullOrWhiteSpace(node.Attributes["typeName"].InnerText))
                    {
                        return null;
                    }


                    //如果该类是单例生成
                    if (node.Attributes["isSingle"].InnerText.Trim().ToUpper() == "TRUE")
                    {
                        //在代理字典里寻找相应代理，没有找到就新实例化一个代理
                        if (!proxyDic.ContainsKey(name))
                        {
                            ProxyBase<T> proxyBase = new ProxyBase<T>();
                            proxyBase.InitProxy(node.Attributes["path"].InnerText, node.Attributes["typeName"].InnerText);
                            proxyDic.Add(name, proxyBase);
                        }
                        return proxyDic[name].Instance;
                    }
                    //不是单例，直接实例化一个代理
                    else
                    {
                        ProxyBase<T> proxyBase = new ProxyBase<T>();
                        proxyBase.InitProxy(node.Attributes["path"].InnerText, node.Attributes["typeName"].InnerText);
                        return proxyBase.Instance;
                    }


                }
            }
            return null;

           
        }
    }
}
