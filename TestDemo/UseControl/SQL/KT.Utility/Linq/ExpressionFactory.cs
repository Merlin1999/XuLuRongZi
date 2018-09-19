using System.Linq.Expressions;

namespace KT.Utility.Linq
{
    /// <summary>
    ///     用于生成表达式
    /// </summary>
    /// <typeparam name="T">表达式的输入类型</typeparam>
    public class ExpressionFactory<T>
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        public ExpressionFactory()
        {
            //相当于生成"p=>"
            ParamExp = Expression.Parameter(typeof (T), "m");
        }

        public ParameterExpression ParamExp { get; private set; }

        /// <summary>
        ///     生成“==”表达式
        /// </summary>
        /// <param name="property">参与比较的属性</param>
        /// <param name="param">参与比较的常量</param>
        /// <returns></returns>
        public Expression EqualExps(string property, object param)
        {
            if (string.IsNullOrEmpty(property)) return null;
            //这里构造左值(属性)表达式.
            var left = Expression.Property(ParamExp, typeof (T).GetProperty(property));
            //构造右值（常量）
            Expression right;
            if (null == param)
            {
                right = Expression.Constant(param);
            }
            else
            {
                right = Expression.Constant(param, param.GetType());
            }
            //返回生成的表达式
            return Expression.Equal(left, right);
        }

        /// <summary>
        ///     生成“!=”表达式
        /// </summary>
        /// <param name="property">参与比较的属性</param>
        /// <param name="param">参与比较的常量</param>
        /// <returns></returns>
        public Expression NotEqualExps(string property, object param)
        {
            if (string.IsNullOrEmpty(property)) return null;
            //这里构造左值(属性)表达式.
            var left = Expression.Property(ParamExp, typeof(T).GetProperty(property));
            //构造右值（常量）
            Expression right;
            if (null == param)
            {
                right = Expression.Constant(param);
            }
            else
            {
                right = Expression.Constant(param, param.GetType());
            }
            //返回生成的表达式
            return Expression.NotEqual(left, right);
        }

        /// <summary>
        ///     生成Contains表达式
        /// </summary>
        /// <param name="property">参与比较的属性</param>
        /// <param name="param">参与比较的常量</param>
        /// <returns></returns>
        public Expression ContainsExps(string property, string param)
        {
            //这里构造左值(属性)表达式.
            var left = Expression.Property(ParamExp, typeof (T).GetProperty(property));
            //构造右值（常量）
            var right = Expression.Constant(param, typeof (string)); //这里构造sname这个常量表达式
            //这里我们用Call这个方法完成"Contains"这个lambda这个表达式的实现.
            return Expression.Call(left, typeof (string).GetMethod("Contains"), right);
        }

        /// <summary>
        ///     生成">"表达式
        /// </summary>
        /// <param name="property">参与比较的属性</param>
        /// <param name="param">参与比较的常量</param>
        /// <returns></returns>
        public Expression MoreExps(string property, object param)
        {
            if (param == null) return null;
            //这里构造左值(属性)表达式.
            var left = Expression.Property(ParamExp, typeof (T).GetProperty(property));
            //构造右值（常量）
            var right = Expression.Constant(param, param.GetType());
            //返回生成的表达式,因为要表示大于,所以属性表达式放在右边
            return Expression.LessThan(right, left);
        }

        /// <summary>
        ///     生成“>=”表达式
        /// </summary>
        /// <param name="property">参与比较的属性</param>
        /// <param name="param">参与比较的常量</param>
        /// <returns></returns>
        public Expression MoreAndEqualExps(string property, object param)
        {
            if (param == null) return null;
            //这里构造左值(属性)表达式.
            var left = Expression.Property(ParamExp, typeof (T).GetProperty(property));
            //构造右值（常量）
            var right = Expression.Constant(param, param.GetType());
            //返回生成的表达式,因为要表示大于,所以属性表达式放在右边
            return Expression.LessThanOrEqual(right, left);
        }

        /// <summary>
        ///     生成“<”表达式
        /// </summary>
        /// <param name="property">参与比较的属性</param>
        /// <param name="param">参与比较的常量</param>
        /// <returns></returns>
        public Expression LessExps(string property, object param)
        {
            if (param == null) return null;
            //这里构造左值(属性)表达式.
            var left = Expression.Property(ParamExp, typeof (T).GetProperty(property));
            //构造右值（常量）
            var right = Expression.Constant(param, param.GetType());
            //返回生成的表达式
            return Expression.LessThan(left, right);
        }

        /// <summary>
        ///     生成“<=”表达式
        /// </summary>
        /// <param name="property">参与比较的属性</param>
        /// <param name="param">参与比较的常量</param>
        /// <returns></returns>
        public Expression LessAndEqualExps(string property, object param)
        {
            if (param == null) return null;
            var pf = typeof (T).GetProperty("Lineinfo");
            var pf1 = pf.PropertyType.GetProperty("LineName");
            // 这里构造左值(属性)表达式.
            var left = Expression.Property(ParamExp, typeof (T).GetProperty(property));
            //MemberExpression left = Expression
            // 构造右值（常量）
            var right = Expression.Constant(param, param.GetType());
            // 返回生成的表达式
            return Expression.LessThanOrEqual(left, right);
        }

        /// <summary>
        ///     生成常量表达式
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Expression ConstantExps(object param)
        {
            if (param == null) return null;
            // 返回生成的表达式
            return Expression.Constant(param, param.GetType());
        }

        /// <summary>
        ///     生成一元属性表达式
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public Expression UnaryExps(string property)
        {
            // 返回生成的表达式
            return Expression.Property(ParamExp, typeof (T).GetProperty(property));
        }

        //public Expression ReplaceParams(LambdaExpression leftexpr, LambdaExpression sourceExp)
        //{
        //    return leftexpr.ReplaceParams(sourceExp);
        //}

    }
}