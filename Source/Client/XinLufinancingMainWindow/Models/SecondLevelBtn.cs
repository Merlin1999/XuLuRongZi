using FoxBaseUi.Common;
using FoxBaseUi.ControlEx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace XinLufinancingMainWindow.Models
{
    /// <summary>
    /// 二级菜案按钮数据模型
    /// </summary>
    public class SecondLevelBtn: NotifyPropertyChanged
    {
        /// <summary>
        /// 子模块控件
        /// </summary>
        public Control SubModel;
        /// <summary>
        /// 按钮控件
        /// </summary>
        private ImageBtnModel btnModel;



        public ImageBtnModel BtnModel
        {
            get => btnModel;
            set
            {
                if (value.Equals(btnModel)) return;
                btnModel = value;
                RaisePropertyChanged("BtnModel");
            }
        }
    }
}
