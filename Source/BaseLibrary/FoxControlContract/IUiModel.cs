using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XinLuControlContract.Entity;

namespace XinLuControlContract
{
    /// <summary>
    /// 动态模块接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUiModel<T>:IDisposable where T : class
    {
        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <param name="arg">初始化参数</param>
        void InitModel(object args);
        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <returns></returns>
        ModelInfo GetModelInfo();
        /// <summary>
        /// 通过子模块名获取子模块的实例
        /// </summary>
        /// <param name="name">子模块名</param>
        /// <returns></returns>
        T GetSunModelByName(string name);
    }
}
