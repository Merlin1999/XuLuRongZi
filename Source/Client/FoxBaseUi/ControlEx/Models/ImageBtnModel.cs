using FoxBaseUi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoxBaseUi.ControlEx.Models
{
    /// <summary>
    /// 含图标的按钮数据模型
    /// </summary>
    public class ImageBtnModel : NotifyPropertyChanged
    {

        private string content;
        private bool isSelected;
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

    }
}
