using FoxBaseUi.Common;
using FoxBaseUi.Common.Models;
using FoxBaseUi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace XinLuSystemManager.ViewModels
{
    public class AddNewUserViewModel: NotifyPropertyChanged, IDialogControl
    {


        /// <summary>
        /// 对话框关闭时发生
        /// </summary>
        private Action<bool> closeDialogCallBack;

        private Control addUserView;

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
            this.addUserView = View;
        }


        private void OnResetBtnClicked(object sender)
        {

        }

        private void OnConfirmBtnClicked(object sender)
        {
            if (RaiseClosed != null)
            {
                RaiseClosed.Invoke();
            }
            this.closeDialogCallBack.Invoke(true);
        }


        public void CloseDialog()
        {
            this.closeDialogCallBack.Invoke(false);
        }

        public void Show(Action<bool> CloseCallBack)
        {
            this.closeDialogCallBack = CloseCallBack;
            StaticGlobal.ExecuteDllEvent.Invoke(this, new XinLuControlContract.Entity.XinLuEventArgs()
            {
                EventType = XinLuControlContract.Entity.DllEventType.ShowDialog
            });
        }

        public Control GetView()
        {
            return this.addUserView;
        }
    }
}
