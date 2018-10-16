using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OYHttpClient.Entity
{
    public class XLRZData
    {
        #region 系统配置模块
        #region 用户模块
        public class user_info
        {
            public int idstr { get; set; }
            public Nullable<int> roletype { get; set; }
            public string username { get; set; }
            public string loginname { get; set; }
            public string loginpass { get; set; }
            public string rolequanxian { get; set; }
            public Nullable<System.DateTime> createtime { get; set; }
            public Nullable<System.DateTime> updatetime { get; set; }
            public string remark { get; set; }
        }

        public struct UserInfoList
        {
            public List<user_info> userinfolist ;
            public string success;
            public string errmsg;
        }
        #endregion
        #region 参数类型模块
        public partial class menueditor_info
        {
            public int idstr { get; set; }
            public string canshutype { get; set; }
            public string canshulist { get; set; }
            public Nullable<System.DateTime> createtime { get; set; }
            public Nullable<System.DateTime> updatetime { get; set; }
            public string remark { get; set; }
        }
        public struct CanShuInfoList
        {
            public List<menueditor_info> canshuinfolist;
            public string success;
            public string errmsg;
        }
        #endregion
        #region 用户日志模块
        public partial class userlog_info
        {
            public int idstr { get; set; }
            public string loginname { get; set; }
            public Nullable<int> logtype { get; set; }
            public string logcontent { get; set; }
            public Nullable<System.DateTime> createtime { get; set; }
        }
        public struct UserLogInfoList
        {
            public List<userlog_info> userloginfolist;
            public string success;
            public string errmsg;
        }
        #endregion
        #endregion
        #region 项目模块
        #region 项目信息模块
        public partial class project_info
        {
            public int idstr { get; set; }
            public string projectname { get; set; }
            public string projectdanwei { get; set; }
            public Nullable<decimal> projectzongtou { get; set; }
            public Nullable<System.DateTime> projectstartdate { get; set; }
            public Nullable<System.DateTime> projectcompletedate { get; set; }
            public string prpjecttype { get; set; }
            public string projectstatus { get; set; }
            public Nullable<System.DateTime> createtime { get; set; }
            public Nullable<System.DateTime> updatetime { get; set; }
            public string remark { get; set; }
        }
        public struct ProjectInfoList
        {
            public List<project_info> projectinfolist;
            public string success;
            public string errmsg;
        }
        #endregion
        #endregion
    }
}