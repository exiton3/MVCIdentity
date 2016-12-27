using System.Collections.Generic;

namespace BookExcursion.Data.Tests
{
    class MultiSelectFilter : IFilter
    {
        public IEnumerable<string> SelectedItems { get; set; }
        public FilterType FilterType { get {return FilterType.MultiSelect;} }
    }
}