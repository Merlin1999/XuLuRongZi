/********************
 * 数据库查询接口
 *	Author：罗小民
 * LastEdit:胡明灿
 * EditTime:2015/9/14
 ********************/

using System;
using System.Linq;
using System.Linq.Expressions;

namespace KT.UpdateServer.Contracts.BDInterface
{
    public interface IQuery<T>
    {

        /// <summary>
        /// 用ID查询单条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetModel(string id);
        /// <summary>
        /// 用ID查询单条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T LoadModel(string id);

        /// <summary>
        /// 是否存在满足条件的记录
        /// </summary>
        /// <param name="conditionExp"></param>
        /// <returns></returns>
        T IsAny(Expression<Func<T, bool>> conditionExp);
        /// <summary>
        /// 获得查询对象
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Collection();

        object GetLock();
    }
}
