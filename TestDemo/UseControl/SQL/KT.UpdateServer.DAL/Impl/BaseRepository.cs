using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using KT.UpdateServer.Contracts.BDInterface;
using KT.UpdateServer.Contracts.DbBase;
using NHibernate;
using NHibernate.Linq;

namespace KT.UpdateServer.DAL.Impl
{
    public class BaseRepository<T> : IRepository<T>
    {
        protected int BatchSize = 100;

        private object _sqliteWLock=new object();


        #region 新建对象逻辑
        public object Create(T o)
        {
            //using (SessionManager)
            lock (SessionManager.GetSessionLock())
            {
                ISession session = SessionManager.GetCurrentSession();
                using (ITransaction tx = session.BeginTransaction())
                {
                    try
                    {
                        var newId = session.Save(o);
                        session.Flush();
                        tx.Commit();
                        return newId;
                    }
                    catch (HibernateException)
                    {
                        tx.Rollback();
                    }
                }              
            }
            return null;
        }

        public void Create(IList<T> objs)
        {
            //using (SessionManager)
            {
                ISession session = SessionManager.GetCurrentSession();
                using (ITransaction tx = session.BeginTransaction())
                {
                    try
                    {
                        int cnt = 1;
                        foreach (var o in objs)
                        {
                            session.Save(o);
                            //与批量设置相同
                            if (cnt % BatchSize == 0)
                            {
                                //flush a batch of inserts and release memory:
                                //将本批插入的对象立即写入数据库并释放内存
                                session.Flush();
                                session.Clear();
                            }
                            cnt++;
                        }

                        tx.Commit();
                        //return newId;
                    }
                    catch (HibernateException)
                    {
                        tx.Rollback();
                    }
                }                
            }

        }
        #endregion

        #region 删除对象

        public void Delete(T o, out OperateStatus status)
        {
            status = new OperateStatus();
            lock (SessionManager.GetSessionLock())
            {
                ISession session = SessionManager.GetCurrentSession();
                using (ITransaction tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(o);
                        session.Flush();
                        tx.Commit();
                    }
                    catch (NHibernate.Exceptions.GenericADOException e)
                    {
                        status.ResultSign = ResultSign.Error;
                        status.Message = "删除失败，可能是该操作存在其他约束信息，请检查!";
                        if (tx != null)
                            tx.Rollback();
                    }
                    catch (HibernateException e)
                    {
                        status.ResultSign = ResultSign.Error;
                        status.Message = e.Message;
                        if (tx != null)
                            tx.Rollback();
                    }
                    finally
                    {
                        session.Disconnect();
                        Thread.Sleep(50);
                    }
                }
            }
        }

        public void Deletes(IList<T> objs, out OperateStatus status)
        {
            status = new OperateStatus();
            //using (SessionManager)
            lock (_sqliteWLock)
            {
                ISession session = SessionManager.GetCurrentSession();
                if (!session.IsConnected) session.Reconnect();
                using (ITransaction tx = session.BeginTransaction())
                {
                    try
                    {
                        int cnt = 1;
                        foreach (var o in objs)
                        {
                            session.Delete(o);
                            //与批量设置相同
                            if (cnt % BatchSize == 0)
                            {
                                //flush a batch of inserts and release memory:
                                //将本批插入的对象立即写入数据库并释放内存
                                session.Flush();
                                session.Clear();
                            }
                            cnt++;
                        }
                        tx.Commit();
                    }
                    catch (HibernateException e)
                    {
                        status.ResultSign = ResultSign.Error;
                        status.Message = e.Message;
                        tx.Rollback(); 
                    }
                    finally
                    {
                        session.Disconnect();
                    }
                }
            }

        }

        public int Deletes(string className, IList<string> objs, out OperateStatus status)
        {
            status = new OperateStatus();
            int dels = 0;
            //using (SessionManager)
            lock (_sqliteWLock)
            {
                ISession session = SessionManager.GetCurrentSession();
                if (!session.IsConnected) session.Reconnect();
                using (ITransaction tx = session.BeginTransaction())
                {
                    try
                    {
                        var sql = new StringBuilder();
                        sql.Append("delete ").Append(className).Append("  where Id  in ( :brandId) ");
                        var query = session.CreateQuery(sql.ToString());
                        query.SetParameterList("brandId", objs);
                        dels = query.ExecuteUpdate();
                        tx.Commit();
                    }
                    catch (HibernateException e)
                    {
                        status.ResultSign = ResultSign.Error;
                        status.Message = e.Message;
                        tx.Rollback(); 
                    }
                    finally
                    {
                        session.Disconnect();
                    }
                    return dels;
                }
            }

        }
        #endregion

        #region 更新对象

        public void Update(T o)
        {
            lock (SessionManager.GetSessionLock())
            {
                ISession session = SessionManager.GetCurrentSession();
                using (ITransaction tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(o);
                        session.Flush();
                        tx.Commit();
                    }
                    catch (HibernateException)
                    {
                        if (tx != null)
                            tx.Rollback();
                    }
                    finally
                    {
                        session.Disconnect();
                        Thread.Sleep(50);
                    }
                }
            }


        }

        public void Update<TObj>(IList<TObj> objs)
        {
            //using (SessionManager)
            lock (SessionManager.GetSessionLock())
            {
                ISession session = SessionManager.GetCurrentSession();
                using (ITransaction tx = session.BeginTransaction())
                {
                    try
                    {
                        int cnt = 1;
                        foreach (var o in objs)
                        {
                            session.Update(o);
                            //与批量设置相同
                            if (cnt % BatchSize == 0)
                            {
                                //flush a batch of inserts and release memory:
                                //将本批插入的对象立即写入数据库并释放内存
                                session.Flush();
                                session.Clear();
                            }
                            cnt++;
                        }
                        tx.Commit();
                    }
                    catch (HibernateException)
                    {
                        tx.Rollback(); 
                    }
                }              
            }

        }
        #endregion

        #region 保存更新对象

        public void SaveOrUpdate(T o, out OperateStatus status)
        {
            status = new OperateStatus();

            ISession session = SessionManager.GetCurrentSession();
            if (!session.IsConnected) session.Reconnect();
            lock (SessionManager.GetSessionLock())
            {
                using (ITransaction tx = session.BeginTransaction())
                {

                    try
                    {
                        session.SaveOrUpdate(o);
                        session.Flush();
                        tx.Commit();
                    }
                    catch (HibernateException e)
                    {
                        status.ResultSign = ResultSign.Error;
                        status.Message = e.Message;
                        if (tx != null)
                            tx.Rollback();
                    }
                    finally
                    {
                        session.Disconnect();
                        Thread.Sleep(50);
                    }
                }
            }


        }

        public void SaveOrUpdates(IList<T> objs, out OperateStatus status)
        {
            status = new OperateStatus();
            //using (SessionManager)
            lock (SessionManager.GetSessionLock())
            {
                ISession session = SessionManager.GetCurrentSession();
                if (!session.IsConnected) session.Reconnect();
                using (ITransaction tx = session.BeginTransaction())
                {
                    try
                    {
                        int cnt = 1;
                        foreach (var o in objs)
                        {
                            session.SaveOrUpdate(o);
                            //与批量设置相同
                            if (cnt % BatchSize == 0)
                            {
                                //flush a batch of inserts and release memory:
                                //将本批插入的对象立即写入数据库并释放内存
                                session.Flush();
                                session.Clear();
                            }
                            cnt++;
                        }
                        tx.Commit();
                    }
                    catch (HibernateException e)
                    {
                        status.ResultSign = ResultSign.Error;
                        status.Message = e.Message;
                        tx.Rollback(); 
                    }
                    finally
                    {
                        session.Disconnect();
                    }
                }              
            }

        }


        #endregion


        public T GetObj(string id, out OperateStatus status)
        {
            status = new OperateStatus();
            
            lock (SessionManager.GetSessionLock())
            {
                ISession session = SessionManager.GetCurrentSession();
                var obj = session.Get<T>(id);
                return obj;
            }
        }


        public T LoadObj(string id, out OperateStatus status)
        {
            status = new OperateStatus();
            lock (SessionManager.GetSessionLock())
            {
                ISession session = SessionManager.GetCurrentSession();
                var obj = session.Load<T>(id);
                return obj;
            }
        }



        public IQueryable<T> Collection()
        {
            try
            {
                ISession session = SessionManager.GetCurrentSession();
                var obj = session.Query<T>();
                return obj;
            }
            catch (Exception e)
            {
                SessionManager.CloseSession();
                ISession session = SessionManager.GetCurrentSession();
                var obj = session.Query<T>();
                return obj;
            }

        }

        protected static string GetPathofDll(string subDirectory)
        {
            string dllPath = null;
            if (AppDomain.CurrentDomain.BaseDirectory.StartsWith(System.Environment.CurrentDirectory))//Windows应用程序则相等 
            {
                dllPath = System.Environment.CurrentDirectory;
                //dllPath = dllPath.Substring(0, dllPath.LastIndexOf("\\"));
            }
            else
            {
                dllPath = AppDomain.CurrentDomain.BaseDirectory;
                dllPath = dllPath.Substring(0, dllPath.LastIndexOf("\\", StringComparison.Ordinal));
                //dllPath = dllPath.Substring(0, dllPath.LastIndexOf("\\"));
                //dllPath = AppDomain.CurrentDomain.BaseDirectory + "Bin";
            }
            dllPath += subDirectory;
            return dllPath;
        }

        public void Close()
        {
            SessionManager.CloseSession();
        }


        public object GetLock()
        {
            return SessionManager.GetSessionLock();
        }
    }
}
