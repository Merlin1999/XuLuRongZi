/********************
 * 为动态生成表达式而创建的扩展方法及支持这些方法的类
 * 测试通过
 *	Author：胡明灿
 * LastEdit:胡明灿
 * EditTime:2015/9/17
 ********************/

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace KT.Utility.Linq
{
    /// <summary>
    /// 用于存放扩展方法的类
    /// </summary>
    public static class ExpressionExt
    {
        /// <summary>
        /// 将sourceExp表达式的参数表达式换成自己的参数表达式
        /// </summary>
        /// <param name="leftexpr">自己</param>
        /// <param name="sourceExp">需要替换参数表达式的表达式</param>
        /// <returns>Expression类型的表达式</returns>
        public static Expression ReplaceParams(this LambdaExpression leftexpr, LambdaExpression sourceExp)
        {
            //生成Dictionary类型的参数表达式表，并传传入替换参数表达式的方法
            return ParameterRebinder.ReplaceParameters(sourceExp.Parameters.Select(
                (f, i) => new { f, s = leftexpr.Parameters[i] }).ToDictionary(p => p.f, p => p.s)
                , sourceExp.Body);

            
        }
     
    }

    /// <summary>
    /// 替换参数表达式的类
    /// </summary>
    public class ParameterRebinder : ExpressionVisitor
    {
        /// <summary>
        /// 存放参数表达式的表
        /// </summary>
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="map">参数表达式的表</param>
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            //如果
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}
