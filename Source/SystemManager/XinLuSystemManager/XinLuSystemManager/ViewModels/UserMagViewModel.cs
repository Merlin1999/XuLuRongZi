using FoxBaseUi.Common;
using FoxBaseUi.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XinLuSystemManager.Views;

namespace XinLuSystemManager.ViewModels
{
    public class UserMagViewModel: NotifyPropertyChanged
    {

        public CommandModel AddUserCmd { get; private set; }

        private AddNewUserViewModel addNewUserVm;

        private AddNewUserView addUserView;

        public  UserMagViewModel()
        {
            AddUserCmd = new CommandModel() {
                Command = new DelegateCommand(OnAddBtnClicked),
                Content ="+ 添加",
                IsEnable=true,
                IsSelected=false,
                
            };
            this.addUserView = new AddNewUserView();
            this.addNewUserVm = new AddNewUserViewModel(this.addUserView);
            this.addUserView.DataContext = addNewUserVm;


        }

        private void OnAddBtnClicked(object sender)
        {
            this.addNewUserVm.Show(ShowDailogCallBack);
        }


        private void ShowDailogCallBack(bool result)
        {

        }
    }
}
