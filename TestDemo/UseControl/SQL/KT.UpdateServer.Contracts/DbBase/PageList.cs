using System.Collections.Generic;
using System.Linq;

namespace KT.UpdateServer.Contracts.DbBase
{
    public class PageList<T>
    {
        public PageList()
        {
        }

        public PageList(IEnumerable<T> source, QueryParam param)
        {
            PageInfo = new PagerInfo(param);
            Data = new List<T>();
            PageInfo.TotalRowCount = source.Count();
            Data.AddRange(source.Skip((PageInfo.PageIndex - 1)*PageInfo.PageSize).Take(PageInfo.PageSize));
        }

        public PagerInfo PageInfo { set; get; }
        public List<T> Data { set; get; }
    }
}