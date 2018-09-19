
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KT.UpdateServer.Contracts.DbBase;
using KT.UpdateServer.Contracts.Models.Query;
using KT.UpdateServer.DB.Dto.Base;
using KT.UpdateServer.DB.Entities.Base;

namespace KT.UpdateServer.BLL.Base
{
    /// <summary>
    ///     数据操作基类（使用DTO）
    /// </summary>
    /// <typeparam name="TEntity">操作的实体类型</typeparam>
    /// <typeparam name="TDto">dto</typeparam>
    /// <typeparam name="TParam">查询条件</typeparam>
    public abstract class DataManagerBaseDto<TEntity, TDto, TParam> : AccessDtoImpl<TEntity, TDto, TParam>
        where TEntity : Entity
        where TDto : Dto
        where TParam : QueryParam
    {

        private object _lockObj = new object();
        /// <summary>
        /// </summary>
        /// <param name="resultDto"></param>
        protected virtual void AfterQueryByIdHandler(TDto resultDto)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="result"></param>
        protected virtual void AfterSaveHandler(TEntity result)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="result"></param>
        protected virtual void AfterQueryHandler(PageList<TDto> dtoList)
        {
        }

        /// <summary>
        ///     从DTO中获得数据实体
        /// </summary>
        /// <param name="dataDto"></param>
        /// <param name="status">是否执行成功（如果名称与数据库已有数据重复，则返回不成功）</param>
        /// <param name="isAdd">是否是新添数据</param>
        /// <returns></returns>
        protected abstract TEntity GetEntity(TDto dataDto, out OperateStatus status, bool isAdd = false);

        #region 编辑操作

        /// <summary>
        ///     添加一条记录
        /// </summary>
        /// <param name="dataDto"></param>
        /// <param name="status">执行结果</param>
        /// <returns>执行状态</returns>
        public virtual void Save(TDto dataDto, out OperateStatus status)
        {
            if (!string.IsNullOrWhiteSpace(dataDto.Id))
            {
                //lock (_lockObj)
                {
                    if (Query.Collection().Any(p => p.Id == dataDto.Id))
                    {
                        status = new OperateStatus
                        {
                            ResultSign = ResultSign.Warning,
                            Message = "数据已存在，请刷新！"
                        };
                        return;
                    }
                }
            }
            TEntity data;
            //lock (_lockObj)
            {
                data = GetEntity(dataDto, out status, true);
            }
            if (status.IsNotSuccessful)
                return;
            //通过反射获取Flag标志
            //var type = data.GetType();
            //try
            //{
            //    var propertyInfo = type.GetProperty("Flag");
            //    if (propertyInfo != null)
            //        propertyInfo.SetValue(data, Convert.ToByte(0), null);
            //}
            //catch
            //{
            //    status = new OperateStatus {ResultSign = ResultSign.Error, Message = "服务执行出错！"};
            //    return;
            //}
            Editer.Save(data, out status);
            if (status.IsSuccessful)
            {
                AfterSaveHandler(data);
                status.Message = "操作成功";
            }

        }

        /// <summary>
        ///     修改一条记录
        /// </summary>
        /// <param name="dataDto"></param>
        /// <param name="status">执行结果</param>
        /// <returns>执行状态</returns>
        public virtual void Update(TDto dataDto, out OperateStatus status)
        {
            //lock (_lockObj)
            {
                if (!Query.Collection().Any(p => p.Id == dataDto.Id))
                {
                    status = new OperateStatus
                    {
                        ResultSign = ResultSign.Warning,
                        Message = "数据不存在，请刷新！"
                    };
                    return;
                }
            }
            TEntity data;
            //lock (_lockObj)
            {
                data = GetEntity(dataDto, out status);
            }

            if (status.IsNotSuccessful)
                return;
            if (!OnPreUpdate(data, out status))
                return;
            Editer.Update(data, out status);
            if (status.IsSuccessful)
            {
                status.Message = "操作成功";
            }

        }

        /// <summary>
        ///     删除数据
        /// </summary>
        /// <param name="id">通过ID</param>
        /// <param name="status"></param>
        /// <returns>执行状态</returns>
        public void Delete(string id, out OperateStatus status)
        {
            //查询需要删除的数据
            var item = Query.GetModel(id);
            //如果没有查出数据返回失败
            if (item == null)
            {
                status = new OperateStatus
                {
                    ResultSign = ResultSign.Warning,
                    Message = "数据不存在，请刷新！"
                };
                return;
            }
            //删除项目关联的信息
            if (!OnPreDelete(item, out status) && (status.ResultSign != ResultSign.Warning ||
                                                   status.Message != "没有符合条件的附件可删除"))
                return;
            ////获取项目删除标记类型
            //var propertyInfo = item.GetType().GetProperty("Flag");
            ////假删除
            //if (propertyInfo != null)
            //{
            //    propertyInfo.SetValue(item, Convert.ToByte(1), null);
            //    Repository.SaveOrUpdate(item, out status);
            //}
            ////如果没有删除标记，真删
            //else
            //{
                //Repository.Delete(item, out status);
            //}
            //直接删除
                Editer.Delete(item.Id, out status);
            if (status.IsSuccessful)
            {
                status.Message = "删除成功";
            }
        }

        #endregion

        #region 查询方法

        /// <summary>
        ///     执行快速查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public PageList<TDto> QuickQuery(AllInOneQueryParam condition)
        {
            //筛选表达式
            var expWhere = CreateWhereExprForQuick(condition);
            //产生linq
            var result = Queryable(condition.Sidx, condition.Sord, expWhere);
            //产生分页表
            return new PageList<TDto>(result, condition);
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public PageList<TDto> SeniorQuery(TParam condition)
        {
            //筛选表达式
            var expWhere = CreateWhereExprFoSenior(condition);
            //产生linq
            var result = Queryable(condition.Sidx, condition.Sord, expWhere);

            //产生分页表
            return new PageList<TDto>(result, condition);
        }


        /// <summary>
        ///     无分页查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<TDto> QueryNoPaging(TParam condition)
        {
            //筛选表达式
            var expWhere = CreateWhereExprFoSenior(condition);
            //产生linq
            var result = Queryable(condition.Sidx, condition.Sord, expWhere);

            return result.ToList();
        }


        /// <summary>
        ///     无分页快速查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<TDto> FQueryNoPaging(AllInOneQueryParam condition)
        {
            //筛选表达式
            var expWhere = CreateWhereExprForQuick(condition);
            //产生linq
            var result = Queryable(condition.Sidx, condition.Sord, expWhere);

            return result.ToList();
        }


        /// <summary>
        ///     通过ID查询
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public TDto QueryById(string ID)
        {
            var temp = new List<TEntity>();
            //从数据库查询
            var tempResult = Query.GetModel(ID);
            if (tempResult == null)
            {
                return null;
            }
            temp.Add(tempResult);
            //映射表达式
            var expSelect = (Expression<Func<TEntity, TDto>>) CreateSelectExpression();
            var resultDto = temp.AsQueryable().Select(expSelect).ToList()[0];
            //如果查询后还有特殊操作
            AfterQueryByIdHandler(resultDto);
            return resultDto;
        }

        /// <summary>
        ///     执行查询
        /// </summary>
        /// <param name="sidx">排序字段</param>
        /// <param name="sord">排序方式</param>
        /// <param name="expWhere">筛选表达式</param>
        /// <returns></returns>
        private IEnumerable<TDto> Queryable(string sidx, string sord, Expression<Func<TEntity, bool>> expWhere)
        {
            //映射表达式
            var expSelect = (Expression<Func<TEntity, TDto>>) CreateSelectExpression();
            //排序表达式
            var expOrder = (Expression<Func<TEntity, object>>) CreateOrderByExpression(sidx);
            //生成linq
            try
            {
                //            var result = sord == "asc"
                //? Query.Collection().Where(expWhere).OrderBy(expOrder).Select(expSelect)
                //: Query.Collection().Where(expWhere).OrderByDescending(expOrder).Select(expSelect);
                List<TEntity> result2;
                lock (Query.GetLock())
                {
                    result2 = sord == "asc"
                        ? Query.Collection().Where(expWhere).OrderBy(expOrder).ToList()
                        : Query.Collection().Where(expWhere).OrderByDescending(expOrder).ToList();
                }
                var result3 = result2.Select(expSelect.Compile());
                return result3;
            }
            catch (Exception e)
            {

                return new List<TDto>();
            }

            //return result;
        }

        #endregion
    }
}