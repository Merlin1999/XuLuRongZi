using FoxBaseUi.Common;
using FoxBaseUi.Common.Models;
using OYHttpClient;
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
        /// <summary>
        /// 添加按钮
        /// </summary>
        public CommandModel AddUserCmd { get; private set; }

        /// <summary>
        /// 删除按钮
        /// </summary>
        public CommandModel DeleteUserCmd { get; private set; }

        /// <summary>
        /// 编辑按钮
        /// </summary>
        public CommandModel EditUserCmd { get; private set; }

        /// <summary>
        /// 用户添加/编辑对话框数据模型
        /// </summary>
        private AddNewUserViewModel addNewUserVm;

        /// <summary>
        ///  用户添加/编辑对话框
        /// </summary>
        private AddNewUserView addUserView;


        /// <summary>
        /// 用户信息列表
        /// </summary>
        public ObservableCollection<UserInfoModel> UserInfos { get; private set; }


        private UserInfoModel selectedUserInfo;
        /// <summary>
        /// 选中的用户
        /// </summary>
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

            //HttpJsonUserInfo

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

        /// <summary>
        /// 点击添加按钮
        /// </summary>
        /// <param name="sender"></param>
        private void OnAddBtnClicked(object sender)
        {
            this.addNewUserVm.DialogName = "添加用户";
            this.addNewUserVm.IsAddMode = true;
            this.addNewUserVm.Show(ShowDailogCallBack);
        }

        /// <summary>
        /// 点击删除
        /// </summary>
        /// <param name="sender"></param>
        private void OnDeleteClicked(object sender)
        {
            
        }


        /// <summary>
        /// 点击编辑
        /// </summary>
        /// <param name="sender"></param>
        private void OnAddEditClicked(object sender)
        {
            this.addNewUserVm.DialogName = "修改用户信息";
            this.addNewUserVm.IsAddMode = false;
            this.addNewUserVm.UserInfo = new UserInfoModel(selectedUserInfo);
            this.addNewUserVm.Show(ShowDailogCallBack);
        }

        /// <summary>
        ///  用户添加/编辑对话框关闭的回调
        /// </summary>
        /// <param name="result">是否保存操作</param>
        private void ShowDailogCallBack(bool result)
        {

        }
    }
}
