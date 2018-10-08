using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinLuSystemManager.Models
{
    public class NewUserModel: FoxBaseUi.Common.NotifyPropertyChanged
    {
        private string userName;
        private string realName;
        private string passWord;
        private bool canCreate;
        private bool canEdit;
        private bool canDelete;
        private bool canQuery;

        public string UserName { get => userName; set => userName = value; }
        public string RealName { get => realName; set => realName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public bool CanCreate { get => canCreate; set => canCreate = value; }
        public bool CanEdit { get => canEdit; set => canEdit = value; }
        public bool CanDelete { get => canDelete; set => canDelete = value; }
        public bool CanQuery { get => canQuery; set => canQuery = value; }
    }
}
