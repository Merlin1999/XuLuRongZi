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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            excellentmcoinEntities db = new excellentmcoinEntities();
            //创建对象实体，注意，这里需要对所有属性进行赋值（除了自动增长主键外），如果不赋值，则会数据库中会被设置为NULL（注意是否可空）
            var user = new user_info
            {
                username = "欧阳",
                loginname = "ouyang",
                loginpass = "123456"
            };
            db.t_userinfo.Add(user);
            db.SaveChanges();
            
        }
    }
}
