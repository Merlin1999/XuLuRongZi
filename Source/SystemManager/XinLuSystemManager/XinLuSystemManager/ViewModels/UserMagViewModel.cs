using FoxBaseUi.Common;
using FoxBaseUi.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XinLuSystemManager.Models;
using XinLuSystemManager.Views;

namespace XinLuSystemManager.ViewModels
{
    public class UserMagViewModel: NotifyPropertyChanged
    {

        public CommandModel AddUserCmd { get; private set; }
        public CommandModel DeleteUserCmd { get; private set; }
        public CommandModel EditUserCmd { get; private set; }

        private AddNewUserViewModel addNewUserVm;

        private AddNewUserView addUserView;

        public ObservableCollection<UserInfoModel> UserInfos { get; private set; }


        private UserInfoModel selectedUserInfo;
        public UserInfoModel SelectedUserInfo
        {
            get => selectedUserInfo;
            set
            {
                if (value == selectedUserInfo) return;
                selectedUserInfo = value;
                RaisePropertyChanged("SelectedUserInfo");

            }
        }

        public  UserMagViewModel()
        {
            AddUserCmd = new CommandModel() {
                Command = new DelegateCommand(OnAddBtnClicked),
                Content ="+ 添加",
                IsEnable=true,
                IsSelected=false,
                
            };
            DeleteUserCmd = new CommandModel()
            {
                Command = new DelegateCommand(OnDeleteClicked),
                Content = "删除用户",
                IsEnable = true,
                IsSelected = false,

            };
            EditUserCmd = new CommandModel()
            {
                Command = new DelegateCommand(OnAddEditClicked),
                Content = "修改用户",
                IsEnable = true,
                IsSelected = false,

            };
            this.addUserView = new AddNewUserView();
            this.addNewUserVm = new AddNewUserViewModel(this.addUserView);
            this.addUserView.DataContext = addNewUserVm;

            UserInfos = new ObservableCollection<UserInfoModel>();
            UserInfos.Add(new UserInfoModel()
            {
                UserName="OY",
                RealName="欧阳",
            });
            UserInfos.Add(new UserInfoModel()
            {
                UserName = "H",
                RealName = "胡",
                CanCreate = true,
            });
        }

        private void OnAddBtnClicked(object sender)
        {
            this.addNewUserVm.DialogName = "添加用户";
            this.addNewUserVm.IsAddMode = true;
            this.addNewUserVm.Show(ShowDailogCallBack);
        }

        private void OnDeleteClicked(object sender)
        {
            
        }

        private void OnAddEditClicked(object sender)
        {
            this.addNewUserVm.DialogName = "修改用户信息";
            this.addNewUserVm.IsAddMode = false;
            this.addNewUserVm.UserInfo = new UserInfoModel(selectedUserInfo);
            this.addNewUserVm.Show(ShowDailogCallBack);
        }


        private void ShowDailogCallBack(bool result)
        {

        }
    }
}
