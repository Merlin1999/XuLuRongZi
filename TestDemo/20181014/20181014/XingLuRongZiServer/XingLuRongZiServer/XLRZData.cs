using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XingLuRongZiServer
{
    public class XLRZData
    {
        public struct UserInfoList
        {
            public List<user_info> userinfolist ;
            public string success;
            public string errmsg;
        }
        public struct CanShuInfoList
        {
            public List<menueditor_info> canshuinfolist;
            public string success;
            public string errmsg;
        }
        public struct UserLogInfoList
        {
            public List<userlog_info> userloginfolist;
            public string success;
            public string errmsg;
        }
        public struct ProjectInfoList
        {
            public List<project_info> projectinfolist;
            public string success;
            public string errmsg;
        }
        public struct RongZiInfoList
        {
            public List<rongzi_info> rongziinfolist;
            public string success;
            public string errmsg;
        }
    }
}