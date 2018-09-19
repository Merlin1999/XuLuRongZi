using System;
using System.Collections.Generic;
using KT.UpdateServer.Contracts.DbBase;

namespace KT.UpdateServer.Contracts.BDInterface
{
    public interface IEditer<T> 
    {
        void Save(T t, out OperateStatus status);
        void Update(T t, out OperateStatus status);
        void Delete(string id, out OperateStatus status);

        void Saves(List<T> t, out OperateStatus status);
        void Updates(List<T> t, out OperateStatus status);
        void Deletes(List<string> ids, out OperateStatus status);
    }
}
