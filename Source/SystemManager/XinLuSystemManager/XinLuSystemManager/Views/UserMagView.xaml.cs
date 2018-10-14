﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XinLuSystemManager.ViewModels;

namespace XinLuSystemManager.Views
{
    /// <summary>
    /// UserMag.xaml 的交互逻辑
    /// </summary>
    public partial class UserMagView : UserControl
    {

        private UserMagViewModel userMagVm;

        public UserMagView()
        {
            InitializeComponent();
            userMagVm = new UserMagViewModel();
            this.DataContext = userMagVm;
        }
    }
}
