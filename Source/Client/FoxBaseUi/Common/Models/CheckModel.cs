using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxBaseUi.Common.Models
{
    /// <summary>
    /// CheckBoxd 的数据模型
    /// </summary>
    public class CheckModel: NotifyPropertyChanged
    {

        private string content;
        private bool isChecked = false;
        private bool isEnable = true;


        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                if (value == isChecked) return;
                isChecked = value;
                RaisePropertyChanged("IsChecked");
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
