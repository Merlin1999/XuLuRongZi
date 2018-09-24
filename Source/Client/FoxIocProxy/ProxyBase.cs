using System;
using System.Reflection;

namespace FoxIocProxy
{
    /// <summary>
    /// 动态生成实体的代理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProxyBase<T> where T:class
    {

        T tagetObj = null;

        /// <summary>
        /// 初始化代理
        /// </summary>
        /// <param name="fileName">程序集全名</param>
        /// <param name="typyName">类型全名（包括命名空间）</param>
        public void InitProxy(string fileName,string typyName)
        {
            //根据程序集全名加载程序集
            Assembly assembly = AppDomain.CurrentDomain.Load(fileName);
            //根据类型全名创建新实例
            object temp = assembly.CreateInstance(typyName);
            if (temp is T)
                tagetObj = temp as T;
        }

        /// <summary>
        /// 代理创建的实体
        /// </summary>
        public T Instance
        { get { return tagetObj; } }
    }
}
