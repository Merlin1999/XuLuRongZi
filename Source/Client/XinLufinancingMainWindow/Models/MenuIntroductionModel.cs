using FoxBaseUi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinLufinancingMainWindow.Models
{
    public class MenuIntroductionModel: NotifyPropertyChanged
    {
        private string menuName;
        private string menuIntro;

        /// <summary>
        /// 菜单选项的名称
        /// </summary>
        public string MenuName
        {
            get => menuName;
            set
            {
                if (value == menuName) return;
                menuName = value;
                RaisePropertyChanged("MenuName");
            }
        }
        /// <summary>
        /// 选项的简介
        /// </summary>
        public string MenuIntro
        {
            get => menuIntro;
            set
            {
                if (value == menuIntro) return;
                menuIntro = value;
                RaisePropertyChanged("MenuIntro");
            }

        }
    }
}
