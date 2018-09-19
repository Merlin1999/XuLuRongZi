using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace KT.UpdateServer.Contracts.DbBase
{
    /// <summary>
    /// 分页查询结果
    /// </summary>
    [DataContract(IsReference = true)]
    [KnownType(typeof(PagerInfo))]
    public class PagedResults<T>
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PagedResults()
        { }

        /// <summary>
        /// 构造一个无数据清单的查询结果
        /// </summary>
        public PagedResults(QueryParam param)
        {
            PagerInfo = new PagerInfo(param);
            Data = new List<T>();
        }
        /// <summary>
        /// 分页信息
        /// </summary>
        [DataMember]
        public PagerInfo PagerInfo { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [DataMember]
        public IList<T> Data { get; set; }

        /// <summary>
        /// 根据传入的转换方法将分页结果转换为指定类型的分页结果
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public PagedResults<TResult> ConvertTo<TResult>(Func<T, TResult> converter)
        {
            var results = new PagedResults<TResult>
            {
                PagerInfo = PagerInfo,
                Data = Data.Select(converter).ToArray()
            };
            return results;
        }

        /// <summary>
        /// 将传入的分页结果强制转换为指定类型的分页结果
        /// </summary>
        /// <returns></returns>
        public static PagedResults<T> ConvertFrom(object pagedResults)
        {
            var type = pagedResults.GetType();
            var pagerInfo = type.GetProperty("PagerInfo").GetValue(pagedResults, null);
            var data = type.GetProperty("Data").GetValue(pagedResults, null);
            var methodInfo = typeof(Enumerable).GetMethod("Cast", BindingFlags.Public | BindingFlags.Static);
            var resultData = (IEnumerable<T>)methodInfo.MakeGenericMethod(typeof(T)).Invoke(null, new[] { data });
            var results = new PagedResults<T>
            {
                PagerInfo = (PagerInfo)pagerInfo,
                Data = resultData.ToArray()
            };
            return results;
        }
    }
}
