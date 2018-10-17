using OYHttpClient.HttpBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static OYHttpClient.Entity.XLRZData;

namespace OYHttpClient
{

    public class HttpJsonUserLogInfo
    {
        static public bool AddUserLogInfo(string sloginname, string slogtype, string slogcontent)
        {
            string url = "AddUserLogInfo";
            string data = BaseHttpJson.HttpPost(url, "sloginname=" + sloginname
             + "&slogtype=" + slogtype
             + "&slogcontent=" + slogcontent);
            if (data != "ERROR")
                return true;
            return false;
        }
        static public List<userlog_info> GetUserLogInfos()
        {
            string url = "GetUserLogInfos";
            string data = BaseHttpJson.HttpPost(url, "");
            if (data != "ERROR")
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                UserLogInfoList userloglst = new UserLogInfoList();
                userloglst = (UserLogInfoList)js.Deserialize(data, userloglst.GetType());
                return userloglst.userloginfolist;
             }
            return new List<userlog_info>();
        }
    }
}
