using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using static XingLuRongZiServer.XLRZData;

namespace XingLuRongZiServer
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string GetVersionNo()
        {
            return "XingLuRongZi_1.0.0";
        }
        #region 系统设置模块
            #region 用户模块  UserInfo
            /// <summary>
            /// 获取用户信息列表
            /// </summary>
            /// <returns></returns>
            [WebMethod]
            public string GetUserInfos()
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                excellentmcoinEntities db = new excellentmcoinEntities();
                string json = "";                 
                UserInfoList list = new UserInfoList();
                list.userinfolist = new List<user_info>();
                var infos = db.Database.SqlQuery<user_info>("select * from user_info");
                foreach (var info in infos)
                {
                    list.userinfolist.Add(info);
                }
                list.success = "OK";
                json= js.Serialize(list);      
                return json;
            }
            /// <summary>
            /// 增加用户信息
            /// </summary>
            /// <returns></returns>
            [WebMethod]
            public bool AddUserInfo(string iroletype, string susername, string sloginname ,string sloginpass, string srolequanxian)
            {
                using (var context = new excellentmcoinEntities())
                {
                    using (var dbcxtransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            user_info user = new user_info
                            {
                                roletype = int.Parse(iroletype),
                                username = susername,
                                loginname = sloginname,
                                loginpass = sloginpass,
                                rolequanxian = srolequanxian,
                                updatetime =DateTime.Now,
                                createtime = DateTime.Now                          
                            };
                            context.t_userinfo.Add(user);
                            context.SaveChanges();
                            dbcxtransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine(e.Message);
                            dbcxtransaction.Rollback();
                            return false;
                        }
                    }
                }
                return true;
            }
            /// <summary>
            /// 删除用户信息
            /// </summary>
            /// <returns></returns>
            [WebMethod]
            public bool DelUserInfo(string sloginname)
            {
                using (var context = new excellentmcoinEntities())
                {
                    using (var dbcxtransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            excellentmcoinEntities db = new excellentmcoinEntities();
                            db.t_userinfo.Where(p => p.loginname == sloginname).ToList<user_info>().ForEach((s) => db.t_userinfo.Remove(s));
                            db.SaveChanges();
                            context.SaveChanges();
                            dbcxtransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine(e.Message);
                            dbcxtransaction.Rollback();
                            return false;
                        }
                    }
                }
                return true;
            }
            /// <summary>
            /// 修改用户信息
            /// </summary>
            /// <returns></returns>
            [WebMethod]
            public bool EditUserInfo(string sloginname,string sloginpass, string srolequanxian)
            {
                using (var context = new excellentmcoinEntities())
                {
                    using (var dbcxtransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            excellentmcoinEntities db = new excellentmcoinEntities();
                            var aa = db.t_userinfo.Where(p => p.loginname == sloginname).ToList<user_info>();
                            foreach (var a in aa)
                            {
                                a.loginpass = sloginpass;
                                a.rolequanxian = srolequanxian;
                                a.updatetime = DateTime.Now;
                            }
                            db.SaveChanges();
                            dbcxtransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine(e.Message);
                            dbcxtransaction.Rollback();
                            return false;
                        }
                    }
                }
                return true;
            }
            #endregion
            #region 菜单模块  MenuEditInfo
            /// <summary>
            /// 增加参数类型
            /// </summary>
            /// <returns></returns>
            [WebMethod]
            public bool AddCanShuInfo(string scanshutype)
            {
                using (var context = new excellentmcoinEntities())
                {
                    using (var dbcxtransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            menueditor_info menu = new menueditor_info
                            {
                                canshutype = scanshutype,
                                updatetime = DateTime.Now,
                                createtime = DateTime.Now
                            };
                            context.t_menueditorinfo.Add(menu);
                            context.SaveChanges();
                            dbcxtransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine(e.Message);
                            dbcxtransaction.Rollback();
                            return false;
                        }
                    }
                }
                return true;
            }
            /// <summary>
            /// 获取参数类型列表
            /// </summary>
            /// <returns></returns>
            [WebMethod]
            public string GetCanShuInfos()
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                excellentmcoinEntities db = new excellentmcoinEntities();
                string json = "";
                CanShuInfoList list = new CanShuInfoList();
                list.canshuinfolist = new List<menueditor_info>();
                var infos = db.Database.SqlQuery<menueditor_info>("select * from menueditor_info");
                foreach (var info in infos)
                {
                    list.canshuinfolist.Add(info);
                }
                list.success = "OK";
                json = js.Serialize(list);
                return json;
            }
            /// <summary>
            /// 删除参数信息
            /// </summary>
            /// <returns></returns>
            [WebMethod]
            public bool DelCanShuInfo(string scanshutype)
            {
                using (var context = new excellentmcoinEntities())
                {
                    using (var dbcxtransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            excellentmcoinEntities db = new excellentmcoinEntities();
                            db.t_menueditorinfo.Where(p => p.canshutype == scanshutype).ToList<menueditor_info>().ForEach((s) => db.t_menueditorinfo.Remove(s));
                            db.SaveChanges();
                            context.SaveChanges();
                            dbcxtransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine(e.Message);
                            dbcxtransaction.Rollback();
                            return false;
                        }
                    }
                }
                return true;
            }
            /// <summary>
            /// 修改参数类型信息
            /// </summary>
            /// <returns></returns>
            [WebMethod]
            public bool EditCanShuInfo(string scanshutype, string scanshulist)
            {
                using (var context = new excellentmcoinEntities())
                {
                    using (var dbcxtransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            excellentmcoinEntities db = new excellentmcoinEntities();
                            var aa = db.t_menueditorinfo.Where(p => p.canshutype == scanshutype).ToList<menueditor_info>();
                            foreach (var a in aa)
                            {
                                a.canshulist = scanshulist;
                                a.updatetime = DateTime.Now;
                            }
                            db.SaveChanges();
                            dbcxtransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine(e.Message);
                            dbcxtransaction.Rollback();
                            return false;
                        }
                    }
                }
                return true;
            }
            #endregion
            #region  用户日志模块  MenuEditInfo
            /// <summary>
            /// 增加参数类型
            /// </summary>
            /// <returns></returns>
            [WebMethod]
            public bool AddUserLogInfo(string sloginname,string slogtype,string slogcontent)
            {
                using (var context = new excellentmcoinEntities())
                {
                    using (var dbcxtransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            userlog_info userlog = new userlog_info
                            {
                                logcontent= slogcontent,
                                loginname= sloginname,
                                logtype = int.Parse(slogtype),
                                createtime = DateTime.Now
                            };
                            context.t_userlog_info.Add(userlog);
                            context.SaveChanges();
                            dbcxtransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine(e.Message);
                            dbcxtransaction.Rollback();
                            return false;
                        }
                    }
                }
                return true;
            }
            /// <summary>
            /// 获取用户日志列表
            /// </summary>
            /// <returns></returns>
            [WebMethod]
            public string GetUserLogInfos()
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                excellentmcoinEntities db = new excellentmcoinEntities();
                string json = "";
                UserLogInfoList list = new UserLogInfoList();
                list.userloginfolist = new List<userlog_info>();
                var infos = db.Database.SqlQuery<userlog_info>("select * from userlog_info");
                foreach (var info in infos)
                {
                    list.userloginfolist.Add(info);
                }
                list.success = "OK";
                json = js.Serialize(list);
                return json;
            }
            /// <summary>
            /// 删除用户日志信息
            /// </summary>
            /// <returns></returns>
            [WebMethod]
            public bool DeUserLogInfo(string scanshutype)
            {
                return true;
            }
            /// <summary>
            /// 修改参数类型信息
            /// </summary>
            /// <returns></returns>
            [WebMethod]
            public bool EditUserLogInfo(string scanshutype, string scanshulist)
            {
                return true;
            }
        #endregion
        #endregion
        #region 项目模块
        #region 项目信息模块
        /// <summary>
        /// 获取项目信息列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetProjectInfos()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            excellentmcoinEntities db = new excellentmcoinEntities();
            string json = "";
            ProjectInfoList list = new ProjectInfoList();
            list.projectinfolist = new List<project_info>();
            var infos = db.Database.SqlQuery<project_info>("select * from project_info");
            foreach (var info in infos)
            {
                list.projectinfolist.Add(info);
            }
            list.success = "OK";
            json = js.Serialize(list);
            return json;
        }
        /// <summary>
        /// 增加项目信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public bool AddProjectInfo(string sprojectname, string sprojectdanwei, 
                                                    decimal dprojectzongtou, DateTime dsprojectstartdate,
                                                    DateTime dprojectcompletedate, string sprpjecttype,
                                                    string sprojectstatus)
        {
            using (var context = new excellentmcoinEntities())
            {
                using (var dbcxtransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        project_info project = new project_info
                        {
                            projectdanwei=sprojectdanwei,
                            projectzongtou= dprojectzongtou,
                            projectstartdate= dsprojectstartdate,
                            projectname = sprojectname,
                            projectcompletedate= dprojectcompletedate,
                            prpjecttype= sprpjecttype,
                            projectstatus= sprojectstatus,
                            updatetime = DateTime.Now,
                            createtime = DateTime.Now
                        };
                        context.t_project_info.Add(project);
                        context.SaveChanges();
                        dbcxtransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e.Message);
                        dbcxtransaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 删除项目信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public bool DeProjectInfo(string sprojectname)
        {
            using (var context = new excellentmcoinEntities())
            {
                using (var dbcxtransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        excellentmcoinEntities db = new excellentmcoinEntities();
                        db.t_project_info.Where(p => p.projectname == sprojectname).ToList<project_info>().ForEach((s) => db.t_project_info.Remove(s));
                        db.SaveChanges();
                        context.SaveChanges();
                        dbcxtransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e.Message);
                        dbcxtransaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 修改项目信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public bool EditProjectInfo(string sprojectname, string sprojectdanwei,
                                                    decimal dprojectzongtou, DateTime dsprojectstartdate,
                                                    DateTime dprojectcompletedate, string sprpjecttype,
                                                    string sprojectstatus)
        {
            using (var context = new excellentmcoinEntities())
            {
                using (var dbcxtransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        excellentmcoinEntities db = new excellentmcoinEntities();
                        var aa = db.t_project_info.Where(p => p.projectname == sprojectname).ToList<project_info>();
                        foreach (var a in aa)
                        {
                            a.projectdanwei = sprojectdanwei;
                            a.projectzongtou = dprojectzongtou;
                            a.projectstartdate = dsprojectstartdate;
                            a.projectcompletedate = dprojectcompletedate;
                            a.prpjecttype = sprpjecttype;
                            a.projectstatus = sprojectstatus;
                            a.updatetime = DateTime.Now;
                        }
                        db.SaveChanges();
                        dbcxtransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        dbcxtransaction.Rollback();
                        Trace.WriteLine(e.Message);
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
        #region  项目使用模块
        #endregion
        #endregion
        #region 融资模块
        #region 融资信息模块
        /// <summary>
        /// 获取融资信息列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetRongZiInfos()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            excellentmcoinEntities db = new excellentmcoinEntities();
            string json = "";
            RongZiInfoList list = new RongZiInfoList();
            list.rongziinfolist = new List<rongzi_info>();
            var infos = db.Database.SqlQuery<rongzi_info>("select * from rongzi_info");
            foreach (var info in infos)
            {
                list.rongziinfolist.Add(info);
            }
            list.success = "OK";
            json = js.Serialize(list);
            return json;
        }
        /// <summary>
        /// 增加融资信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public bool AddRongZiInfo(string szhaiwuname, string szhaiwutype,
            string szhaiwunrename,
            string szhaiquanrenname,string szhaiquanrentype,
            string srongzifangshi,string szijinyongtu,
            string shetongjine,string sdaoweijineshidian,
            string srongziyueshidian,string sdanbaofangshi,
            string slilvtype,string slilvmiaoshu,
            string slilvbaifenbi,string  sfeilv,
            string sqixian,string sqixiri,
            string sdaoqiri,string sremark,
            string shetongno)
        {
            using (var context = new excellentmcoinEntities())
            {
                using (var dbcxtransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        rongzi_info rongzi = new rongzi_info
                        { 
                            zhaiwuname = szhaiwuname,zhaiwutype= szhaiwutype,
                            zhaiwunrename = szhaiwunrename,
                            zhaiquanrenname = szhaiquanrenname,zhaiquanrentype = szhaiquanrentype,
                            rongzifangshi = srongzifangshi,zijinyongtu = szijinyongtu,
                            hetongjine = decimal.Parse(shetongjine),daoweijineshidian = DateTime.Parse(sdaoweijineshidian),
                            rongziyueshidian = DateTime.Parse(srongziyueshidian),danbaofangshi = sdanbaofangshi,
                            lilvtype = slilvtype,lilvmiaoshu = slilvmiaoshu,
                            lilvbaifenbi = double.Parse(slilvbaifenbi),feilv = double.Parse(sfeilv),
                            qixian = int.Parse(sqixian),qixiri = DateTime.Parse(sqixiri),
                            daoqiri = DateTime.Parse(sdaoqiri),remark = sremark,
                            hetongno = shetongno,
                            updatetime = DateTime.Now,
                            createtime = DateTime.Now
                        };
                        context.t_rongzi_info.Add(rongzi);
                        context.SaveChanges();
                        dbcxtransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e.Message);
                        dbcxtransaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 删除融资信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public bool DeRongZiInfo(string szhaiwuname)
        {
            using (var context = new excellentmcoinEntities())
            {
                using (var dbcxtransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        excellentmcoinEntities db = new excellentmcoinEntities();
                        db.t_rongzi_info.Where(p => p.zhaiwuname == szhaiwuname).ToList<rongzi_info>().ForEach((s) => db.t_rongzi_info.Remove(s));
                        db.SaveChanges();
                        context.SaveChanges();
                        dbcxtransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e.Message);
                        dbcxtransaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 修改项目信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public bool EditRongZiInfo(string szhaiwuname, string szhaiwutype,
            string szhaiwunrename,
            string szhaiquanrenname, string szhaiquanrentype,
            string srongzifangshi, string szijinyongtu,
            string shetongjine, string sdaoweijineshidian,
            string srongziyueshidian, string sdanbaofangshi,
            string slilvtype, string slilvmiaoshu,
            string slilvbaifenbi, string sfeilv,
            string sqixian, string sqixiri,
            string sdaoqiri, string sremark,
            string shetongno)
        {
            using (var context = new excellentmcoinEntities())
            {
                using (var dbcxtransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        excellentmcoinEntities db = new excellentmcoinEntities();
                        var aa = db.t_rongzi_info.Where(p => p.zhaiwuname == szhaiwuname).ToList<rongzi_info>();
                        foreach (var a in aa)
                        {
                            a.zhaiwutype = szhaiwutype;a.zhaiwunrename = szhaiwunrename;
                            a.zhaiquanrenname = szhaiquanrenname; a.zhaiquanrentype = szhaiquanrentype;
                            a.rongzifangshi = srongzifangshi;a.zijinyongtu = szijinyongtu;
                            a.hetongjine = decimal.Parse(shetongjine);a.daoweijineshidian = DateTime.Parse(sdaoweijineshidian);
                            a.rongziyueshidian = DateTime.Parse(srongziyueshidian);a.danbaofangshi = sdanbaofangshi;
                            a.lilvtype = slilvtype;a.lilvmiaoshu = slilvmiaoshu;
                            a.lilvbaifenbi = double.Parse(slilvbaifenbi);a.feilv = double.Parse(sfeilv);
                            a.qixian = int.Parse(sqixian); a.qixiri = DateTime.Parse(sqixiri);
                            a.daoqiri = DateTime.Parse(sdaoqiri); a.remark = sremark;
                            a.hetongno = shetongno;
                            a.updatetime = DateTime.Now;
                        }
                        db.SaveChanges();
                        dbcxtransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        dbcxtransaction.Rollback();
                        Trace.WriteLine(e.Message);
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
        #endregion
    }
}
