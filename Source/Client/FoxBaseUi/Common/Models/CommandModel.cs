using FoxBaseUi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoxBaseUi.Common.Models
{
    /// <summary>
    /// 含图标的按钮数据模型
    /// </summary>
    public class CommandModel : NotifyPropertyChanged
    {

        private string content;
        private bool isSelected = false;
        private bool isEnable = true;

        /// <summary>
        /// 
        /// </summary>
        public ICommand Command { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (value == isSelected) return;
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }
        public string Content
        {
            get => content;
            set
            {
                if (value == content) return;
                content = value;
                RaisePropertyChanged("Content");
            }
        }

        /// <summary>
        /// 按钮是否可用
        /// </summary>
        public bool IsEnable
        {
            get => isEnable;
            set
            {
                if (value == isEnable) return;
                isEnable = value;
                RaisePropertyChanged("IsEnable");
            }
        }
    }
}
