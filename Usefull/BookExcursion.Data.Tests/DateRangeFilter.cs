using System;

namespace BookExcursion.Data.Tests
{
    internal class DateRangeFilter:IFilter
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public FilterType FilterType { get { return  FilterType.DateRange;} }
    }
}