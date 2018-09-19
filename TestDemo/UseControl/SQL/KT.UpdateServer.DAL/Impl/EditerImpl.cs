using System;
using System.Collections.Generic;
using KT.UpdateServer.Contracts.BDInterface;
using KT.UpdateServer.Contracts.DbBase;

namespace KT.UpdateServer.DAL.Impl
{
    //限定此类型的操作必须是Entity类型
    public class EditerImpl<T> : IEditer<T>
    {
        private readonly IRepository<T> _repository;

        public EditerImpl():this(null){}

        public EditerImpl(IRepository<T> repository)
        {
            if (repository != null)
                _repository = repository;
            else
                _repository = new BaseRepository<T>();
        }

        public void Save(T t, out OperateStatus status)
        {
            _repository.SaveOrUpdate(t, out status);
        }

        public void Update(T t, out OperateStatus status)
        {

            _repository.SaveOrUpdate(t, out status);

        }

        public void Delete(string id, out OperateStatus status)
        {
            var entity = _repository.GetObj(id, out status);
            if (null == entity)//对象不存在，不进行操作
            {
                SetNoObjOperateStatus(out status);
                return;
            }
            _repository.Delete(entity, out status);
        }

        public void Saves(List<T> t, out OperateStatus status)
        {
            _repository.SaveOrUpdates(t, out status);

        }

        public void Updates(List<T> t, out OperateStatus status)
        {
            _repository.SaveOrUpdates(t, out status);

        }

        public void Deletes(List<string> ids, out OperateStatus status)
        {
            _repository.Deletes(typeof(T).Name, ids, out status);
        }

        /// <summary>
        /// 设置对象不存在OperateStatus信息
        /// </summary>
        /// <param name="status">程序操作的信息类</param>
        private void SetNoObjOperateStatus(out OperateStatus status)
        {
            status = new OperateStatus();
            status.ResultSign = ResultSign.Warning;
            status.Message = "当前信息已失效，可能是已被删除或更改，请更新信息后，重新操作。";
        }
    }
}
