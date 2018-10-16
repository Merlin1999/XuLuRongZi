using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinLuSystemManager.Models
{
    public class UserInfoModel: FoxBaseUi.Common.NotifyPropertyChanged
    {

        private string userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get => userName;
            set
            {
                if (value == userName) return;
                userName = value;
                RaisePropertyChanged("UserName");
            }
        }

        private string realName;
        /// <summary>
        /// 真实名
        /// </summary>
        public string RealName
        {
            get => realName;
            set
            {
                if (value == realName) return;
                realName = value;
                RaisePropertyChanged("RealName");
            }
        }


        private string role;
        /// <summary>
        /// 角色
        /// </summary>
        public string Role
        {
            get => realName;
            set
            {
                if (value == role) return;
                role = value;
                RaisePropertyChanged("Role");
            }
        }


        private string passWord;
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            get => passWord;
            set
            {
                if (value == passWord) return;
                passWord = value;
                RaisePropertyChanged("PassWord");
            }
        }


        private bool canCreate;
        /// <summary>
        /// 添加权限
        /// </summary>
        public bool CanCreate
        {
            get => canCreate;
            set
            {
                if (value == canCreate) return;
                canCreate = value;
                RaisePropertyChanged("CanCreate");
            }
        }


        private bool canEdit;
        /// <summary>
        /// 编辑权限
        /// </summary>
        public bool CanEdit
        {
            get => canEdit;
            set
            {
                if (value == canEdit) return;
                canEdit = value;
                RaisePropertyChanged("CanCreate");
            }
        }


        private bool canDelete;
        /// <summary>
        /// 删除权限
        /// </summary>
        public bool CanDelete
        {
            get => canDelete;
            set
            {
                if (value == canDelete) return;
                canDelete = value;
                RaisePropertyChanged("CanDelete");
            }
        }


        private bool canQuery;
        /// <summary>
        /// 查询权限
        /// </summary>
        public bool CanQuery
        {
            get => canQuery;
            set
            {
                if (value == canQuery) return;
                canQuery = value;
                RaisePropertyChanged("CanQuery");
            }
        }


        private string pId;
        /// <summary>
        /// 编码
        /// </summary>
        public string PId
        {
            get => pId;
            set
            {
                if (value == pId) return;
                pId = value;
                RaisePropertyChanged("PId");
            }
        }


        private int number;
        /// <summary>
        /// 序号
        /// </summary>
        public int Number
        {
            get => number;
            set
            {
                if (value == number) return;
                number = value;
                RaisePropertyChanged("Number");
            }
        }



        public UserInfoModel()
        {
        }

        public UserInfoModel(UserInfoModel userInfo)
        {
            this.userName = userInfo.UserName;
            this.role = userInfo.Role;
            this.realName = userInfo.RealName;
            this.pId = userInfo.PId;
            this.passWord = userInfo.PassWord;
            this.canQuery = userInfo.CanQuery;
            this.canEdit = userInfo.CanEdit;
            this.canDelete = userInfo.CanDelete;
            this.canCreate = userInfo.CanCreate;
        }
    }
}
