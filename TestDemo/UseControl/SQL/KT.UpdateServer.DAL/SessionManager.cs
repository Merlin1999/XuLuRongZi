using System;
using NHibernate;
using NHibernate.Cfg;

namespace KT.UpdateServer.DAL
{


    public class SessionManager
    {
        //private const string CurrentSessionKey = "nhibernate.current_session";
        private static readonly ISessionFactory SessionFactory;

        private static  ISession _currentSession;

        private static readonly object SessionLock = new object();
        static SessionManager()
        {
            var dllPath = AppDomain.CurrentDomain.BaseDirectory;
            dllPath = dllPath.Substring(0, dllPath.LastIndexOf("\\", StringComparison.Ordinal));
            SessionFactory = new Configuration().Configure(dllPath + "\\Config\\SQLite.cfg.xml").BuildSessionFactory();
        }

       
        public static ISession GetCurrentSession()
        {
            if (_currentSession == null)
            {
                _currentSession = SessionFactory.OpenSession();
            }
            else if(!_currentSession.IsConnected)
            {
                _currentSession.Reconnect();
            }
            return _currentSession;
        }


        public static object GetSessionLock()
        {
            return SessionLock;
        }
 
        public static void CloseSession()
        {
            //HttpContext context = HttpContext.Current;
            //ISession currentSession = context.Items[CurrentSessionKey] as ISession;

            if (_currentSession == null)
            {
                // No current session
                return;
            }
            _currentSession.Close();
        }

        public static void CloseSessionFactory()
        {
            if (SessionFactory != null)
            {
                SessionFactory.Close();
            }
        }
    }

}