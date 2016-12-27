using System;
using System.Linq.Expressions;

namespace BookExcursion.Data.Tests
{
    internal static class PropertyExpressionHelper
    {
        public static Func<TContainer, TProperty> InitializeGetter<TContainer, TProperty>(
            Expression<Func<TContainer, TProperty>> getterExpression)
        {
            return getterExpression.Compile();
        }

        public static string GetPropertyName<T, TProp>(Expression<Func<T, TProp>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
                return member.Member.Name;

            throw new ArgumentException("Expression is not a member access", "expression");
        }
    }
}