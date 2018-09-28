using FoxIocProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XinLufinancingMainWindow.ViewModels;

namespace XinLufinancingMainWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {


        private MainWinVM mainWinVM;


        public MainWindow()
        {
            
            InitializeComponent();
            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            mainWinVM = new MainWinVM(ctlPanel);
            this.DataContext = mainWinVM;
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
