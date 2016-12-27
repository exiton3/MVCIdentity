using System;

namespace BookExcursion.Data.Tests
{
    class FilterConfig<T>
    {
        public FilterConfig(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; }

        public Func<T,object> Getter { get; set; }

        public FilterType Type { get; set; }

        public IFilter Filter { get; set; }
    }
}