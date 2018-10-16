using FoxBaseUi.Common;
using FoxBaseUi.Common.Models;
using FoxBaseUi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using XinLuSystemManager.Models;

namespace XinLuSystemManager.ViewModels
{
    public class AddNewUserViewModel: NotifyPropertyChanged, IDialogControl
    {


        /// <summary>
        /// 对话框关闭时发生
        /// </summary>
        private Action<bool> closeDialogCallBack;

        /// <summary>
        /// 对话框的内容控件
        /// </summary>
        private Control addUserView;

        private string dialogName;
        /// <summary>
        /// 对话框名称
        /// </summary>
        public string DialogName
        {
            get => dialogName;
            set
            {
                if (value == dialogName) return;
                dialogName = value;
                RaisePropertyChanged("DialogName");

            }
        }

        private bool isAddMode;
        /// <summary>
        /// 是否是添加模式（false时时修改模式）
        /// </summary>
        public bool IsAddMode
        {
            get => isAddMode;
            set
            {
                if (value == isAddMode) return;
                isAddMode = value;
                RaisePropertyChanged("IsAddMode");

            }
        }

        private UserInfoModel userInfo;
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoModel UserInfo
        {
            get => userInfo;
            set
            {
                if (value == userInfo) return;
                userInfo = value;
                RaisePropertyChanged("UserInfo");

            }
        }

        /// <summary>
        /// 重置按钮
        /// </summary>
        public CommandModel ResetCommand { get;  set; }

        /// <summary>
        /// 确认按钮
        /// </summary>
        public CommandModel ConfirmCommand { get; set; }
        


        /// <summary>
        /// 向对话框容器提交关闭请求
        /// </summary>
        public event Action RaiseClosed;



        public AddNewUserViewModel(Control View)
        {
            ResetCommand = new CommandModel() {
                Command=new DelegateCommand(OnResetBtnClicked),
                Content="重置",
                IsEnable=true,
            };
            ConfirmCommand = new CommandModel()
            {
                Command = new DelegateCommand(OnConfirmBtnClicked),
                Content = "确认",
                IsEnable = true,
            };
            UserInfo = new UserInfoModel();
            this.addUserView = View;
        }

        /// <summary>
        /// 重置操作
        /// </summary>
        /// <param name="sender"></param>
        private void OnResetBtnClicked(object sender)
        {

        }

        /// <summary>
        /// 关闭并保存
        /// </summary>
        /// <param name="sender"></param>
        private void OnConfirmBtnClicked(object sender)
        {
            if (RaiseClosed != null)
            {
                RaiseClosed.Invoke();
            }
            this.closeDialogCallBack.Invoke(true);
        }

        /// <summary>
        /// 关闭对话框（放弃）
        /// </summary>
        public void CloseDialog()
        {
            this.closeDialogCallBack.Invoke(false);
        }

        public void Show(Action<bool> CloseCallBack)
        {
            this.closeDialogCallBack = CloseCallBack;
            StaticGlobal.RunExecuteDllEvent(this, new XinLuControlContract.Entity.XinLuEventArgs()
            {
                EventType = XinLuControlContract.Entity.DllEventType.ShowDialog
            });
        }

        /// <summary>
        /// 获取对话框的内容控件
        /// </summary>
        /// <returns></returns>
        public Control GetView()
        {
            return this.addUserView;
        }

    }
}
