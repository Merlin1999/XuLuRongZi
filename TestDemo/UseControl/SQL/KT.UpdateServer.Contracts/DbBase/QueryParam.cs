/********************
 * 查询参数基类类及快速查询参数类
 *	Author：胡明灿
 * LastEdit:胡明灿
 * EditTime:2015/9/14
 ********************/

namespace KT.UpdateServer.Contracts.DbBase
{
    /// <summary>
    /// 查询参数的基类
    /// </summary>
    public abstract class QueryParam
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected QueryParam()
        {
            Rows = 100;
            Page = 1;
            Sord = "asc";
            //Sord = SordWay.Asc;
        }

        /// <summary>
        /// 获取起始记录
        /// <para>从0开始</para>
        /// </summary>
        public int StartIndex
        {
            get
            {
                return (Page - 1) * Rows;
            }
        }

        /// <summary>
        /// 起始页码
        /// <para>从1开始</para>
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页记录数，默认100
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 排序方式，默认升序（asc）
        /// </summary>
        public string Sord { get; set; }

        /// <summary>
        /// 用来排序的字段
        /// </summary>
        public string Sidx { get; set; }
    }
}