using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace XinLuControlContract.Entity
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public class ModelInfo
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModelName;
        /// <summary>
        /// 模块功能描述
        /// </summary>
        public string ModelIntroduction;
        /// <summary>
        /// 模块图标
        /// </summary>
        public ImageSource Icon;
        /// <summary>
        /// 子模块列表
        /// </summary>
        public List<string> SubModelNames;
    }

}
