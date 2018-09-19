using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
   public  interface IBaseBLL
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool AddObject<T>(T t);
        /// <summary>
        /// 修改对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool UpdateObject<T>(T t);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool DeleteObject<T>(T t);
        T GetSingle<T>(Expression<Func<T, bool>> model) where T : class,new();
        IList<T> GetList<T>(Expression<Func<T, bool>> model) where T : class,new();
        IList<T> GetList<T, Tkey>(Expression<Func<T, bool>> model, Expression<Func<T, Tkey>> order, int rows, int page, out int recordcount) where T : class,new();
        IList<T> GetListAsc<T, Tkey>(Expression<Func<T, bool>> model, Expression<Func<T, Tkey>> order, int rows, int page, out int recordcount) where T : class,new();
        int GetCount<T>(Expression<Func<T, bool>> model) where T : class,new();
        IList<T> GetAll<T>() where T : class;
        IList<T> SqlQuery<T>(string sql, params object[] parametters) where T : class;
    }
}
