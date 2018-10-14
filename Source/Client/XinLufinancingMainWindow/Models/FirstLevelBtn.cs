using FoxBaseUi.Common;
using FoxBaseUi.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using XinLuControlContract;

namespace XinLufinancingMainWindow.Models
{
    /// <summary>
    /// 一级菜单按钮数据模型
    /// </summary>
    public class FirstLevelBtn: NotifyPropertyChanged
    {
        private CommandModel btnModel;
        private MenuIntroductionModel intro;
        private ObservableCollection<SecondLevelBtn> secondLevelBtns;
        private ImageSource icon;

        /// <summary>
        /// 按钮所对应分功能模块
        /// </summary>
        public IUiModel<Control> UiModel { get; set; }

        /// <summary>
        /// 按钮控件
        /// </summary>
        public CommandModel BtnModel
        {
            get => btnModel;
            set
            {
                if (value.Equals(btnModel)) return;
                btnModel = value;
                RaisePropertyChanged("BtnModel");
            }
        }

        /// <summary>
        /// 二级菜单按钮集合
        /// </summary>
        public ObservableCollection<SecondLevelBtn> SecondLevelBtns
        {
            get => secondLevelBtns;
            set
            {
                if (value.Equals(secondLevelBtns)) return;
                secondLevelBtns = value;
                RaisePropertyChanged("SecondLevelBtns");
            }
        }

        /// <summary>
        /// 模块简介
        /// </summary>
        public MenuIntroductionModel Intro
        {
            get => intro;
            set
            {
                if (value.Equals(intro)) return;
                intro = value;
                RaisePropertyChanged("Intro");
            }
        }

        /// <summary>
        /// 按钮图标
        /// </summary>
        public ImageSource Icon
        {
            get => icon;
            set
            {
                if (value.Equals(icon)) return;
                icon = value;
                RaisePropertyChanged("Inoc");
            }
        }
    }
}
