using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KT.UpdateServer.Contracts.BDInterface;
using KT.UpdateServer.Contracts.DbBase;
using KT.UpdateServer.Contracts.Models.Query;
using KT.UpdateServer.DAL.Impl;
using KT.UpdateServer.DB.Dto.Base;
using KT.UpdateServer.DB.Entities.Base;

namespace KT.UpdateServer.BLL.Base
{
    public abstract class QueryDtoImpl<TEntity, TParam>
        where TEntity : Entity
        where TParam : QueryParam
    {
        private IQuery<TEntity> _query;

        public IQuery<TEntity> Query
        {
            get { return _query ?? (_query = new QueryImpl<TEntity>()); }
        }

        #region 构造表达式的方法

        /// <summary>
        /// 创建快速查询where表达式
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected abstract Expression<Func<TEntity, bool>> CreateWhereExprForQuick(AllInOneQueryParam param);

        /// <summary>
        /// 创建高级查询where表达式
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected abstract Expression<Func<TEntity, bool>> CreateWhereExprFoSenior(TParam param);


        /// <summary>
        ///     创建 linq 中的排序表达式
        /// </summary>
        /// <param name="order">排序条件</param>
        /// <returns></returns>
        protected abstract Expression CreateOrderByExpression(string order);

        /// <summary>
        ///     创建映射表达式
        /// </summary>
        /// <returns></returns>
        protected abstract Expression CreateSelectExpression();

        #endregion
    }
}
