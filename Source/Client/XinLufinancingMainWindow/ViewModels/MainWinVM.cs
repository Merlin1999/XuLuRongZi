using FoxBaseUi.Common;
using FoxBaseUi.Common.Models;
using FoxBaseUi.ControlEx;
using FoxBaseUi.Interface;
using FoxCoreUtility.Files;
using FoxIocProxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using XinLuControlContract;
using XinLuControlContract.Entity;
using XinLufinancingMainWindow.Models;

namespace XinLufinancingMainWindow.ViewModels
{
    /// <summary>
    /// 主界面ViewModel
    /// </summary>
    public class MainWinVM : NotifyPropertyChanged
    {

        #region 界面容器 

        /// <summary>
        /// 主界面上的控件容器
        /// </summary>
        private readonly Panel ctrContent;

        /// <summary>
        /// 对话框容器控件
        /// </summary>
        private readonly Border dailogBorder;

        private IDialogControl dialog;

        #endregion

        #region 界面上绑定的属性

        /// <summary>
        /// 退出弹出框事件
        /// </summary>
        public ICommand CloseMsgBoxCmd { get; set; }

        ObservableCollection<FirstLevelBtn> firstMenuBtns;
        /// <summary>
        /// 一级菜单按钮集合
        /// </summary>
        public ObservableCollection<FirstLevelBtn> FirstMenuBtns
        {
            get => firstMenuBtns;
            set => firstMenuBtns = value;
        }


        FirstLevelBtn selectedFirstBtn;
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

        SecondLevelBtn selectedSecondBtn;
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



        Visibility msgBoxVisibility = Visibility.Collapsed;
        /// <summary>
        /// 弹出框可见性
        /// </summary>
        public Visibility MsgBoxVisibility
        {
            get => msgBoxVisibility;
            set
            {
                if (value.Equals(msgBoxVisibility)) return;
                msgBoxVisibility = value;
                RaisePropertyChanged("MsgBoxVisibility");
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrContent">控件的容器</param>
        public MainWinVM(Panel panel,Border border)
        {
            ctrContent = panel;
            this.dailogBorder = border;
            firstMenuBtns = new ObservableCollection<FirstLevelBtn>();
            MsgBoxVisibility = Visibility.Collapsed;
            CloseMsgBoxCmd = new DelegateCommand(OnCloseMsgBox);

            LoadBLLModels();

        }

        /// <summary>
        /// 加载业务模块
        /// </summary>
        private void LoadBLLModels()
        {
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
                    firstBtn.UiModel.RegisterEventHandler(DealDllEventHandler);
                    firstBtn.BtnModel = new CommandModel()
                    {
                        Content = firstBtn.UiModel.GetModelInfo().ModelName,
                        IsSelected = false,
                        Command = new DelegateCommand(OnFirstLevelBtnClicked)
                    };
                    firstBtn.Intro = new MenuIntroductionModel()
                    { MenuName = firstBtn.BtnModel.Content, MenuIntro = firstBtn.UiModel.GetModelInfo().ModelIntroduction };
                    firstBtn.Icon = firstBtn.UiModel.GetModelInfo().Icon;
                    firstBtn.SecondLevelBtns = new ObservableCollection<SecondLevelBtn>();
                    foreach (var sub in firstBtn.UiModel.GetModelInfo().SubModelNames)
                    {
                        firstBtn.SecondLevelBtns.Add(new SecondLevelBtn()
                        {
                            BtnModel = new CommandModel() { Content = sub, IsSelected = false, Command = new DelegateCommand(OnSecondLevelBtnClicked) }
                        });
                    }
                    firstMenuBtns.Add(firstBtn);
                }
            }
        }


        #region 事件处理

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


        /// <summary>
        /// 退出弹出框事件
        /// </summary>
        /// <param name="sender">按钮的model</param>
        private void OnCloseMsgBox(object sender)
        {
            if (this.dialog != null)
            {
                this.dialog.CloseDialog();
            }
            DealCloseDialog();
        }

        private void DealCloseDialog()
        {
            MsgBoxVisibility = Visibility.Collapsed;
            this.dailogBorder.Child = null;
            this.dialog = null;
        }

        /// <summary>
        /// 处理业务模块的提交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DealDllEventHandler(object sender, XinLuEventArgs e)
        {
            if (e.EventType == DllEventType.ShowDialog)
            {
                if (sender is IDialogControl)
                {
                    DispatcherHelper.Initialize();
                    DispatcherHelper.CheckBeginInvokeonUi(new Action(() =>
                    {
                        this.dialog = sender as IDialogControl;
                        this.dialog.RaiseClosed += DealCloseDialog;
                        this.dailogBorder.Child = this.dialog.GetView();
                        MsgBoxVisibility = Visibility.Visible;
                    }));

                }
            }
        }

        #endregion





    }
}
