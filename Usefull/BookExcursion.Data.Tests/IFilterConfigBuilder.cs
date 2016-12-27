namespace BookExcursion.Data.Tests
{
    internal interface IFilterConfigBuilder
    {
        IFilterConfigBuilder SetFilter<TFilter>() where TFilter : IFilter,new();
    }
}