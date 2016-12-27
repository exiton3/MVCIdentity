namespace BookExcursion.Data.Tests
{
    internal class SimpleFilter:IFilter
    {
        public string Value { get; set; }

        public FilterType FilterType { get { return FilterType.Simple; } }
    }
}