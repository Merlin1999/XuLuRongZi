using System.Collections.Generic;
using System.Web.Script.Serialization;
using static ClientTest.XLRZData;

namespace ClientTest
{
    class HttpJsonUserInfo
    {
        static public List<user_info> GetUserInfos()
        {
            string url = "GetUserInfos";
            string data = BaseHttpJson.HttpPost( url, "");
            if (data != "ERROR")
            { 
                JavaScriptSerializer js = new JavaScriptSerializer();
                UserInfoList userlst = new UserInfoList();
                userlst = (UserInfoList)js.Deserialize(data, userlst.GetType());
                return userlst.userinfolist;
           }
            return new List<user_info>();
        }
        static public bool AddUserInfo(string iroletype, string susername, string sloginname, string sloginpass, string srolequanxian)
        {
            string url = "AddUserInfo";
            string data = BaseHttpJson.HttpPost(url, "iroletype=" + iroletype
            + "&susername=" + susername
            + "&sloginname=" + sloginname
            + "&sloginpass=" + sloginpass
            + "&srolequanxian=" + srolequanxian);
            if (data != "ERROR")
                return true;
            return false;
        }
        static public bool DelUserInfo(string sloginname)
        {
            string url = "DelUserInfo";
            string data = BaseHttpJson.HttpPost(url, "sloginname=" + sloginname);
            if (data != "ERROR")
                return true;
            return false;
        }
        static public bool EditUserInfo(string sloginname, string sloginpass, string srolequanxian)
        {
            string url = "EditUserInfo";
            string data = BaseHttpJson.HttpPost(url, "sloginname=" + sloginname
            + "&sloginpass=" + sloginpass
            + "&srolequanxian=" + srolequanxian);
            if (data != "ERROR")
                return true;
            return false;
        }
    }
}
