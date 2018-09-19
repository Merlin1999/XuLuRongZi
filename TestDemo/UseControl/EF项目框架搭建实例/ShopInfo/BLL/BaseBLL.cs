using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using IBLL;
using Comm;
using Entity;

namespace BLL
{
   public class BaseBLL:IBaseBLL
    {
       public ShopDBEntities entitys = EntityesContext.Instance<ShopDBEntities>();
       public bool AddObject<T>(T t)
       {
           return EntityesContext.Insert(this.entitys, t);
       }

       public bool UpdateObject<T>(T t)
       {
           return EntityesContext.Update(this.entitys, t);
       }

       public bool DeleteObject<T>(T t)
       {
           return EntityesContext.Delete(this.entitys, t);
       }

       public T GetSingle<T>(Expression<Func<T, bool>> model) where T : class,new()
       {
           return this.entitys.CreateObjectSet<T>().FirstOrDefault(model);
       }

       public IList<T> GetList<T>(Expression<Func<T, bool>> model) where T : class,new()
       {
          
           
           return this.entitys.CreateObjectSet<T>().Where<T>(model).ToList();
       }

       public IList<T> GetList<T, Tkey>(Expression<Func<T, bool>> model, Expression<Func<T, Tkey>> order, int rows, int page, out int recordcount) where T : class,new()
       {
           recordcount = GetCount(model);
           return this.entitys.CreateObjectSet<T>().Where<T>(model).OrderByDescending(order).Skip((page - 1) * rows).Take(rows).ToList();
       }
       public IList<T> GetListAsc<T, Tkey>(Expression<Func<T, bool>> model, Expression<Func<T, Tkey>> order, int rows, int page, out int recordcount) where T : class,new()
       {
           recordcount = GetCount(model);
           return this.entitys.CreateObjectSet<T>().Where<T>(model).OrderBy(order).Skip((page - 1) * rows).Take(rows).ToList();
       }
       public int GetCount<T>(Expression<Func<T, bool>> model) where T : class,new()
       {
           return this.entitys.CreateObjectSet<T>().Where<T>(model).Count();
       }

       public IList<T> GetAll<T>() where T : class
       {
           return this.entitys.CreateObjectSet<T>().ToList();
       }
       public IList<T> SqlQuery<T>(string sql, params object[] parametters) where T : class
       {
           return this.entitys.ExecuteStoreQuery<T>(sql, parametters).ToList();
       }
    }
}
