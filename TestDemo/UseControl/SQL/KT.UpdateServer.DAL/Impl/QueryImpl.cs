/********************
 * 数据库操作类
 *	Author：罗小民
 * LastEdit:胡明灿
 * EditTime:2015/9/14
 ********************/

using System;
using System.Linq;
using System.Linq.Expressions;
using KT.UpdateServer.Contracts.BDInterface;
using KT.UpdateServer.Contracts.DbBase;

namespace KT.UpdateServer.DAL.Impl
{
    public class QueryImpl<T> : IQuery<T>
    {

        private IRepository<T> _repository;

        public IRepository<T> Repository
        {
            get { return _repository ?? (_repository = new BaseRepository<T>()); }
        }

        /// <summary>
        /// 用ID查询单条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetModel(string id)
        {
            OperateStatus stau;
            return Repository.GetObj(id, out stau);
        }

        public T LoadModel(string id)
        {
            OperateStatus stau;

            return Repository.LoadObj(id, out stau);
        }

        /// <summary>
        /// 是否存在满足条件的记录
        /// </summary>
        /// <param name="conditionExp"></param>
        /// <returns></returns>
        public T IsAny(Expression<Func<T, bool>> conditionExp)
        {
            if (null == conditionExp) return default(T);
            T result;
            lock (Repository.GetLock())
            {
                result=Repository.Collection().FirstOrDefault(conditionExp);
            }
            return result;
        }

        /// <summary>
        /// 将数据仓储获取集合接口公布
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Collection()
        {
            return Repository.Collection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object GetLock()
        {
            return Repository.GetLock();
        }


    }
}
