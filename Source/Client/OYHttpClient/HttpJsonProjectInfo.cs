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
    public class HttpJsonProjectInfo
    {
        static public bool AddProjectInfo(string szhaiwuname, string szhaiwutype,
            string szhaiwunrename,
            string szhaiquanrenname,string szhaiquanrentype,
            string srongzifangshi,string szijinyongtu,
            string shetongjine,string sdaoweijineshidian,
            string srongziyueshidian,string sdanbaofangshi,
            string slilvtype,string slilvmiaoshu,
            string slilvbaifenbi,string sfeilv,
            string sqixian,string sqixiri,
            string sdaoqiri,string sremark,
            string shetongno)
        {
            string url = "AddProjectInfo";
            string data = BaseHttpJson.HttpPost(url, "szhaiwuname=" + szhaiwuname + "&szhaiwutype=" + szhaiwutype
            + "&szhaiwunrename=" + szhaiwunrename
            + "&szhaiquanrenname=" + szhaiquanrenname + "&szhaiquanrentype=" + szhaiquanrentype
            + "&srongzifangshi=" + srongzifangshi + "&szijinyongtu=" + szijinyongtu
            + "&shetongjine=" + shetongjine + "&sdaoweijineshidian=" + sdaoweijineshidian
            + "&srongziyueshidian=" + srongziyueshidian + "&sdanbaofangshi=" + sdanbaofangshi
            + "&slilvtype=" + slilvtype + "&slilvmiaoshu=" + slilvmiaoshu
            + "&slilvbaifenbi=" + slilvbaifenbi + "&sfeilv=" + sfeilv
            + "&sqixian=" + sqixian + "&sqixiri=" + sqixiri
            + "&sdaoqiri=" + sdaoqiri + "&sremark=" + sremark
            + "&shetongno=" + shetongno);
            if (data != "ERROR")
                return true;
            return false;
        }
        static public List<project_info> GetProjectInfos()
        {
            string url = "GetProjectInfos";
            string data = BaseHttpJson.HttpPost(url, "");
            if (data != "ERROR")
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                ProjectInfoList prolst = new ProjectInfoList();
                prolst = (ProjectInfoList)js.Deserialize(data, prolst.GetType());
                return prolst.projectinfolist;
            }
            return new List<project_info>();
        }
        static public bool EditProjectInfo(string szhaiwuname, string szhaiwutype,
            string szhaiwunrename,
            string szhaiquanrenname,string szhaiquanrentype,
            string srongzifangshi,string szijinyongtu,
            string shetongjine,string sdaoweijineshidian,
            string srongziyueshidian,string sdanbaofangshi,
            string slilvtype,string slilvmiaoshu,
            string slilvbaifenbi,string sfeilv,
            string sqixian,string sqixiri,
            string sdaoqiri,string sremark,
            string shetongno)
        {
            string url = "EditCanShuInfo";
            string data = BaseHttpJson.HttpPost(url, "szhaiwuname=" + szhaiwuname + "&szhaiwutype=" + szhaiwutype
            + "&szhaiwunrename=" + szhaiwunrename
            + "&szhaiquanrenname=" + szhaiquanrenname + "&szhaiquanrentype=" + szhaiquanrentype
            + "&srongzifangshi=" + srongzifangshi + "&szijinyongtu=" + szijinyongtu
            + "&shetongjine=" + shetongjine + "&sdaoweijineshidian=" + sdaoweijineshidian
            + "&srongziyueshidian=" + srongziyueshidian + "&sdanbaofangshi=" + sdanbaofangshi
            + "&slilvtype=" + slilvtype + "&slilvmiaoshu=" + slilvmiaoshu
            + "&slilvbaifenbi=" + slilvbaifenbi + "&sfeilv=" + sfeilv
            + "&sqixian=" + sqixian + "&sqixiri=" + sqixiri
            + "&sdaoqiri=" + sdaoqiri + "&sremark=" + sremark
            + "&shetongno=" + shetongno);
            if (data != "ERROR")
                return true;
            return false;
        }
        static public bool DelProjectInfo(string szhaiwuname)
        {
            string url = "DelProjectInfo";
            string data = BaseHttpJson.HttpPost(url, "szhaiwuname=" + szhaiwuname);
            if (data != "ERROR")
                return true;
            return false;
        }
    }
}
