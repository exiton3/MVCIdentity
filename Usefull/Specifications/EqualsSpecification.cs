using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookExcursion.Data.Repositories
{
    class EqualsSpecification<T> : ISpecification<T> where T: class
    {
        string _value;

        public EqualsSpecification(string value)
        {
            _value = value;
        }

        public string Property { get; set; }
        public Func<T,string> Property2 { get; set; }


        public Expression<Func<T, bool>> GetExpression<TValue>(Expression<Func<T,TValue>> propertyExpr)
        {
            ParameterExpression paramExp = propertyExpr.Parameters.Single();
            var expression = propertyExpr.Body as MemberExpression;

            BinaryExpression binaryExpression = Expression.Equal(Expression.Constant(_value,typeof(TValue)), expression);

            return Expression.Lambda<Func<T, bool>>(binaryExpression, paramExp);
        }


        public Expression<Func<T, bool>> GetExpression()
        {
            ParameterExpression paramExp = Expression.Parameter(typeof(T), "x");
            MemberExpression memberExp = Expression.Property(paramExp, Property);
            BinaryExpression binaryExpression =  Expression.Equal(Expression.Constant(_value), memberExp);
            
            return Expression.Lambda<Func<T, bool>>(binaryExpression, paramExp);
        }
    }
}