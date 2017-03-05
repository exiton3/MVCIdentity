using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BookExcursion.Data.Repositories
{
    public interface ISpecification<T> where T:class 
    {
        Expression<Func<T, bool>> GetExpression();
    }

    class MultiSpecification<T> : ISpecification<T> where T : class
    {
        string value;

        public string Property { get; set; }
        public MultiSpecification(string value)
        {
            this.value = value;
        }

        public Expression<Func<T, bool>> GetExpression()
        {
            return x => Property.Contains(value);
        }
    }
}