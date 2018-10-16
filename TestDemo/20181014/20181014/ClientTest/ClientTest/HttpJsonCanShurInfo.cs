using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static ClientTest.XLRZData;

namespace ClientTest
{
    class HttpJsonCanShuInfo
    {
        static public bool AddCanShuInfo(string scanshutype)
        {
            string url = "AddCanShuInfo";
            string data = BaseHttpJson.HttpPost(url, "scanshutype=" + scanshutype);
            if (data != "ERROR")
                return true;
            return false;
        }
        static public List<menueditor_info> GetCanShuInfos()
        {
            string url = "GetCanShuInfos";
            string data = BaseHttpJson.HttpPost(url, "");
            if (data != "ERROR")
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                CanShuInfoList cslst = new CanShuInfoList();
                cslst = (CanShuInfoList)js.Deserialize(data, cslst.GetType());
                return cslst.canshuinfolist;
            }
            return new List<menueditor_info>();
        }
        static public bool EditCanShuInfo(string scanshutype, string scanshulist)
        {
            string url = "EditCanShuInfo";
            string data = BaseHttpJson.HttpPost(url, "scanshutype=" + scanshutype
            + "&scanshulist=" + scanshulist);
            if (data != "ERROR")
                return true;
            return false;
        }
        static public bool DelCanShuInfo(string scanshutype)
        {
            string url = "DelCanShuInfo";
            string data = BaseHttpJson.HttpPost(url, "scanshutype=" + scanshutype);
            if (data != "ERROR")
                return true;
            return false;
        }
    }
}
