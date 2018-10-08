using FoxBaseUi.Common;
using FoxBaseUi.ControlEx.Models;
using FoxCoreUtility.Files;
using FoxIocProxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using XinLuControlContract;
using XinLufinancingMainWindow.Models;

namespace XinLufinancingMainWindow.ViewModels
{
    /// <summary>
    /// 主界面ViewModel
    /// </summary>
    public class MainWinVM : NotifyPropertyChanged
    {

        ObservableCollection<FirstLevelBtn> firstMenuBtns;

        FirstLevelBtn selectedFirstBtn;

        SecondLevelBtn selectedSecondBtn;

        /// <summary>
        /// 主界面上的控件容器
        /// </summary>
        private Panel ctrContent;

        /// <summary>
        /// 一级菜单按钮集合
        /// </summary>
        public ObservableCollection<FirstLevelBtn> FirstMenuBtns
        {
            get => firstMenuBtns;
            set => firstMenuBtns = value;
        }

        /// <summary>
        /// 被选中的一级菜单按钮
        /// </summary>
        public FirstLevelBtn SelectedFirstBtn
        {
            get => selectedFirstBtn;
            set
            {
                if (value.Equals(selectedFirstBtn)) return;
                selectedFirstBtn = value;
                RaisePropertyChanged("SelectedFirstBtn");
            }
        }

        /// <summary>
        /// 被选中的二级菜案按钮
        /// </summary>
        public SecondLevelBtn SelectedSecondBtn
        {
            get => selectedSecondBtn;
            set
            {
                if (value.Equals(selectedSecondBtn)) return;
                selectedSecondBtn = value;
                RaisePropertyChanged("SelectedSecondBtn");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrContent">控件的容器</param>
        public MainWinVM(Panel panel)
        {
            ctrContent = panel;

            firstMenuBtns = new ObservableCollection<FirstLevelBtn>();


            //获取程序所在路径
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //获取配置文件中的所有类节点
            var nodes = XmlHelper.GetXmlNodeListByXpath(path + @"\Config\XinluApp.cfg.xml", "//Peojects//Peoject");
            if (nodes != null)
            {
                //遍历所有节点，找到输入的类别名对应的节点
                for (int i = 0; i < nodes.Count; i++)
                {
                    var node = nodes.Item(i);                  
                    var firstBtn = new FirstLevelBtn();
                    firstBtn.UiModel = DynamicProxy<IUiModel<Control>>.GetInstance(node.Attributes["name"].InnerText);
                    if (firstBtn.UiModel == null)
                        continue;
                    firstBtn.UiModel.InitModel(null);
                    firstBtn.BtnModel = new ImageBtnModel()
                    {
                        Content = firstBtn.UiModel.GetModelInfo().ModelName, IsSelected = false,
                        Command = new DelegateCommand(OnFirstLevelBtnClicked)
                    };
                    firstBtn.Intro = new MenuIntroductionModel()
                    { MenuName = firstBtn.BtnModel.Content, MenuIntro = firstBtn.UiModel.GetModelInfo().ModelIntroduction };
                    firstBtn.Icon = firstBtn.UiModel.GetModelInfo().Icon;
                    firstBtn.SecondLevelBtns = new ObservableCollection<SecondLevelBtn>() ;
                    foreach(var sub in firstBtn.UiModel.GetModelInfo().SubModelNames)
                    {
                        firstBtn.SecondLevelBtns.Add(new SecondLevelBtn()
                        {
                            BtnModel = new ImageBtnModel() { Content = sub, IsSelected = false, Command = new DelegateCommand(OnSecondLevelBtnClicked) }
                        });
                    }
                    firstMenuBtns.Add(firstBtn);
                }
            }
            ////DynamicProxy<IUiModel<Control>>.GetInstance("");
            //firstMenuBtns.Add(new FirstLevelBtn()
            //{
            //    BtnModel = new ImageBtnModel() { Content = "TEST1", IsSelected = false, Command = new DelegateCommand(OnFirstLevelBtnClicked) },
            //    Intro = new MenuIntroductionModel() { MenuName = "TEST1", MenuIntro = "this is test1 demo" },
            //    SecondLevelBtns = new ObservableCollection<SecondLevelBtn>(),
            //    Icon = new BitmapImage(new Uri(
            //@"C:\Users\Administrator\Desktop\兴泸项目\XuLuRongZi\Source\Client\XinLufinancingMainWindow\Resources\xitong.png",
            //UriKind.RelativeOrAbsolute)),
            //});
            //firstMenuBtns.Add(new FirstLevelBtn()
            //{
            //    BtnModel = new ImageBtnModel() { Content = "TEST2", IsSelected = false, Command = new DelegateCommand(OnFirstLevelBtnClicked) },
            //    Intro = new MenuIntroductionModel() { MenuName = "TEST2", MenuIntro = "this is test2 demo" },
            //    SecondLevelBtns = new ObservableCollection<SecondLevelBtn>(),
            //    Icon = new BitmapImage(new Uri(
            //    @"C:\Users\Administrator\Desktop\兴泸项目\XuLuRongZi\Source\Client\XinLufinancingMainWindow\Resources\danbao.png",
            //    UriKind.RelativeOrAbsolute)),
            //});
            //firstMenuBtns[0].SecondLevelBtns.Add(new SecondLevelBtn()
            //{
            //    BtnModel = new ImageBtnModel() { Content = "TEST1SUB1", IsSelected = false, Command = new DelegateCommand(OnSecondLevelBtnClicked) }
            //});
            //firstMenuBtns[0].SecondLevelBtns.Add(new SecondLevelBtn()
            //{
            //    BtnModel = new ImageBtnModel() { Content = "TEST1SUB2", IsSelected = false, Command = new DelegateCommand(OnSecondLevelBtnClicked) }
            //});
            //firstMenuBtns[1].SecondLevelBtns.Add(new SecondLevelBtn()
            //{
            //    BtnModel = new ImageBtnModel() { Content = "TEST2SUB1", IsSelected = false, Command = new DelegateCommand(OnSecondLevelBtnClicked) }
            //});
            //firstMenuBtns[1].SecondLevelBtns.Add(new SecondLevelBtn()
            //{
            //    BtnModel = new ImageBtnModel() { Content = "TEST2SUB2", IsSelected = false, Command = new DelegateCommand(OnSecondLevelBtnClicked) }
            //});
        }

        /// <summary>
        /// 一级菜单点击事件
        /// </summary>
        /// <param name="sender">按钮的model</param>
        private void OnFirstLevelBtnClicked(object sender)
        {
            //遍历按钮集合，找到点击的按钮
            foreach (var btn in FirstMenuBtns)
            {
                if (btn.Equals(sender))
                {
                    btn.BtnModel.IsSelected = true;
                    SelectedFirstBtn = btn;
                }
                else
                {
                    btn.BtnModel.IsSelected = false; ;
                }
            }
        }

        /// <summary>
        /// 二级菜单点击事件
        /// </summary>
        /// <param name="sender">按钮的model</param>
        private void OnSecondLevelBtnClicked(object sender)
        {
            //在被选中的一级菜案按钮中的二级菜单按钮集合中找到点击的按钮
            foreach (var btn in selectedFirstBtn.SecondLevelBtns)
            {
                if (btn.Equals(sender))
                {
                    btn.BtnModel.IsSelected = true;
                    SelectedSecondBtn = btn;
                    ctrContent.Children.Clear();

                    if(SelectedSecondBtn.SubModel==null)
                        SelectedSecondBtn.SubModel = SelectedFirstBtn.UiModel.GetSubModelByName(SelectedSecondBtn.BtnModel.Content);

                    if (SelectedSecondBtn.SubModel != null)
                        ctrContent.Children.Add(SelectedSecondBtn.SubModel);
                }
                else
                {
                    btn.BtnModel.IsSelected = false; ;
                }
            }
        }

    }
}
