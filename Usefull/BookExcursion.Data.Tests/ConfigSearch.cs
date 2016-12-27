using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BookExcursion.Data.Tests
{
    class ConfigSearch<T> where T:class
    {
        readonly List<FilterConfig<T>> _filterConfigs = new List<FilterConfig<T>>();
        protected IFilterConfigBuilder For<TValue>(Expression<Func<T, TValue>> expression)
        {
            var propertyName = PropertyExpressionHelper.GetPropertyName(expression);
            var getter = PropertyExpressionHelper.InitializeGetter(expression);

            var g = new Func<T, object>(o => getter(o));
            var filterConfig = new FilterConfig<T>(propertyName)
            {
                Getter = g
            };
            _filterConfigs.Add(filterConfig);
            var filterConfigBuilder = FilterConfigrBuilder<T>.Create(filterConfig);
            return filterConfigBuilder;
        }
    }
}