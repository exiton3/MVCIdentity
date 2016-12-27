using System.Collections.Generic;

namespace BookExcursion.Data.Tests
{
    public interface IFilterData
    {
        IEnumerable<IFilter> Filters { get; set; }
    }

    class FilterData : IFilterData
    {
        public FilterData()
        {
            Filters = new List<IFilter>();
        }
        public IEnumerable<IFilter> Filters { get; set; }
    }
}