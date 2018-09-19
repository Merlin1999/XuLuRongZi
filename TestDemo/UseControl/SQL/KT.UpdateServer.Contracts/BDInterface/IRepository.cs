using System;
using System.Collections.Generic;
using System.Linq;
using KT.UpdateServer.Contracts.DbBase;

namespace KT.UpdateServer.Contracts.BDInterface
{
	public interface IRepository<T>
	{
		void Delete(T o, out OperateStatus status);
        int Deletes(string className, IList<string> objs, out OperateStatus status);
        void SaveOrUpdate(T o, out OperateStatus status);
        void SaveOrUpdates(IList<T> objs, out OperateStatus status);
        T GetObj(string id, out OperateStatus status);
        T LoadObj(string id, out OperateStatus status);
        IQueryable<T> Collection();
	    Object GetLock();
	}
}

