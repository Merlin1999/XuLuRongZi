using System;
using System.Runtime.Serialization;

namespace KT.UpdateServer.Contracts.DbBase
{
    /// <summary>
    /// 分页信息
    /// </summary>
    [DataContract]
    public class PagerInfo
    {
        /// <summary>
        /// 根据查询参数初始化分页信息
        /// </summary>
        /// <param name="queryParam"></param>
        public PagerInfo(QueryParam queryParam)
        {
            PageSize = queryParam.Rows;
            StartIndex = queryParam.StartIndex < 0 ? 0 : queryParam.StartIndex;
            PageIndex = queryParam.Page < 1 ? 1 : queryParam.Page;
        }

        /// <summary>
        /// 获取或设置总记录数
        /// </summary>
        [DataMember]
        public int TotalRowCount { get; set; }

        /// <summary>
        /// 获取或设置每页记录数
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// 获取或设置起始记录（从0开始）
        /// </summary>        
        [DataMember]
        public int StartIndex { get; set; }

        /// <summary>
        /// 获取当前页码（从1开始）
        /// </summary>
        public int PageIndex { get; set; }

        ///// <summary>
        ///// 获取当前页码（从1开始）
        ///// </summary>
        //public int PageIndex
        //{
        //    get
        //    {
        //        return ComputePageIndex(StartIndex + 1, PageSize);
        //    }
        //}


        /// <summary>
        /// 获取记录的总页数
        /// </summary>
        public int TotalPageCount
        {
            get { return ComputePageIndex(TotalRowCount, PageSize); }
        }

        /// <summary>
        /// 计算页码
        /// </summary>
        /// <param name="total"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private static int ComputePageIndex(int total, int pageSize)
        {
            int rem;
            int pageIndex = Math.DivRem(total, pageSize, out rem);
            if (rem > 0)
                pageIndex++;
            return pageIndex;
        }
    }
}