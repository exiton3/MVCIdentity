using System.Collections;
using System.Collections.Generic;

namespace Data.Tests
{
    public interface IFilter
    {
        string FieldName { get; set; }

        FilterType Type { get; set; }

    }

    class Filter : IFilter
    {
        public string FieldName { get; set; }
        public FilterType Type { get; set; }
    }

    class MultiSelectFilter: Filter
    {
        public IEnumerable<string> Values { get; set; }
    }

    class SimpleFilter : Filter
    {

        public string Value { get; set; }
    }
}