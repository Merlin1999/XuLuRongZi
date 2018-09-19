//**********************************************
// 文  件  名：PredicateList
// 命名空间：CRD.MIMS.Infrastructure.Linq
// 内       容：
// 功       能：
// 文件关系：
// 作       者：罗小民
// 小       组：研发中心
// 生成日期：2015/10/15 11:55:27
// 版  本  号：V1.0.0.0
// 修改日志：
// 版权说明：
//**********************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KT.Utility.Linq
{
    /// <summary>
    /// 功能：条件过滤类
    /// 作者：张占岭
    /// 日期：2012-6-7
    /// </summary>
    public class PredicateList<TEntity> : IEnumerable<Expression<Func<TEntity, bool>>> where TEntity : class
    {
        List<Expression<Func<TEntity, bool>>> expressionList;
        public PredicateList()
        {
            expressionList = new List<Expression<Func<TEntity, bool>>>();
        }
        /// <summary>
        /// 添加到集合
        /// </summary>
        /// <param name="predicate"></param>
        public void Add(Expression<Func<TEntity, bool>> predicate)
        {
            expressionList.Add(predicate);
        }
        /// <summary>
        /// Or操作添加到集合
        /// </summary>
        /// <param name="exprleft"></param>
        /// <param name="exprright"></param>
        public void AddForOr(Expression<Func<TEntity, bool>> exprleft, Expression<Func<TEntity, bool>> exprright)
        {
            expressionList.Add(exprleft.Or(exprright));
        }
        /// <summary>
        /// And操作添加到集合
        /// </summary>
        /// <param name="exprleft"></param>
        /// <param name="exprright"></param>
        public void AddForAnd(Expression<Func<TEntity, bool>> exprleft, Expression<Func<TEntity, bool>> exprright)
        {
            expressionList.Add(exprleft.And(exprright));
        }

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return expressionList.GetEnumerator();
        }

        #endregion

        #region IEnumerable<Expression<Func<TEntity>>> 成员

        IEnumerator<Expression<Func<TEntity, bool>>> IEnumerable<Expression<Func<TEntity, bool>>>.GetEnumerator()
        {
            return expressionList.GetEnumerator();
        }

        #endregion
    }
}
