using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

// code from https://stackoverflow.com/questions/49074642/combine-multiple-linq-expressions

namespace JARS.Core.Utils
{
    public static class LinqExpressionBuilder
    {
        public static Expression<Func<T, bool>> True<T>()
        {
            return (Expression<Func<T, bool>>)(input => true);
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return (Expression<Func<T, bool>>)(input => false);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            InvocationExpression invocationExpression = Expression<Func<T, bool>>.Invoke(expression2, expression1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expression1.Body, invocationExpression), expression1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            InvocationExpression invocationExpression = Expression<Func<T, bool>>.Invoke(expression2, expression1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expression1.Body, invocationExpression), expression1.Parameters);
        }
    }
}
