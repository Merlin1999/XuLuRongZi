using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using KT.UpdateServer.Contracts.BDInterface;
using KT.UpdateServer.Contracts.DbBase;
using KT.UpdateServer.DAL.Impl;
using KT.UpdateServer.DB.Dto.Base;
using KT.UpdateServer.DB.Entities.Base;

namespace KT.UpdateServer.BLL.Base
{
    /// <summary>
    /// 封装编辑的一般方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParam"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public abstract class AccessDtoImpl<TEntity, TDto, TParam> : QueryDtoImpl<TEntity, TParam>
        where TEntity : Entity
        where TDto : Dto
        where TParam : QueryParam
    {
        private IEditer<TEntity> _diter;


        public IEditer<TEntity> Editer
        {
            get { return _diter ?? (_diter = new EditerImpl<TEntity>()); }
        }
        #region 保存前预处理方法

        /// <summary>
        /// 保存前执行方法
        /// </summary>
        /// <param name="dto">数据传输模型</param>
        /// <param name="status">程序操作的信息类</param>
        /// <returns>出现问题返回false</returns>
        protected virtual bool OnPreSave(TDto dto, out OperateStatus status)
        {
            status = new OperateStatus();
            return true;
        }


        /// <summary>
        /// 保存前执行方法
        /// </summary>
        /// <param name="dtos">数据传输模型集合</param>
        /// <param name="status">程序操作的信息类</param>
        /// <returns>出现问题返回false</returns>
        protected virtual bool OnPreSaves(List<TDto> dtos, out OperateStatus status)
        {
            status = new OperateStatus();
            return true;
        }

        /// <summary>
        /// 保存前执行方法
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="status">程序操作的信息类</param>
        /// <returns>出现问题返回false</returns>
        protected virtual bool OnPreSave(TEntity entity, out OperateStatus status)
        {
            status = new OperateStatus();
            return true;
        }


        /// <summary>
        /// 保存前执行方法
        /// </summary>
        /// <param name="entities">实体模型集合</param>
        /// <param name="status">程序操作的信息类</param>
        /// <returns>出现问题返回false</returns>
        protected virtual bool OnPreSaves(List<TEntity> entities, out OperateStatus status)
        {
            status = new OperateStatus();
            return true;
        }

        /// <summary>
        /// 保存前执行方法
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="status">程序操作的信息类</param>
        /// <param name="dto">数据传输模型</param>
        /// <returns>出现问题返回false</returns>
        protected virtual bool OnPreCreate(TDto dto, TEntity entity, out OperateStatus status)
        {
            status = new OperateStatus();
            return true;
        }

        #endregion


        #region 更新前预处理方法

        /// <summary>
        /// 更新前执行方法
        /// </summary>
        /// <param name="dto">数据传输模型</param>
        /// <param name="status">程序操作的信息类</param>
        /// <returns>出现问题返回false</returns>
        protected virtual bool OnPreUpdate(TDto dto, out OperateStatus status)
        {
            status = new OperateStatus();
            return true;
        }


        /// <summary>
        /// 更新前执行方法
        /// </summary>
        /// <param name="dtos">数据传输模型集合</param>
        /// <param name="status">程序操作的信息类</param>
        /// <returns>出现问题返回false</returns>
        protected virtual bool OnPreUpdates(List<TDto> dtos, out OperateStatus status)
        {
            status = new OperateStatus();
            return true;
        }

        /// <summary>
        /// 更新前执行方法
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="status">程序操作的信息类</param>
        /// <returns>出现问题返回false</returns>
        protected virtual bool OnPreUpdate(TEntity entity, out OperateStatus status)
        {
            status = new OperateStatus();
            return true;
        }


        /// <summary>
        /// 更新前执行方法
        /// </summary>
        /// <param name="entities">实体模型集合</param>
        /// <param name="status">程序操作的信息类</param>
        /// <returns>出现问题返回false</returns>
        protected virtual bool OnPreUpdates(List<TEntity> entities, out OperateStatus status)
        {
            status = new OperateStatus();
            return true;
        }

        #endregion

        #region 删除前预处理方法

        /// <summary>
        /// 删除前执行方法
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="status">程序操作的信息类</param>
        /// <returns>出现问题返回false</returns>
        protected virtual bool OnPreDelete(TEntity entity, out OperateStatus status)
        {
            status = new OperateStatus();
            return true;
        }

        /// <summary>
        /// 删除前执行方法
        /// </summary>
        /// <param name="ids">删除实体的主键集合</param>
        /// <param name="status">程序操作的信息类</param>
        /// <returns>出现问题返回false</returns>
        protected virtual bool OnPreDeletes(List<string> ids, out OperateStatus status)
        {
            status = new OperateStatus();
            return true;
        }

        #endregion
       
    }
}
