namespace BookExcursion.Data.Tests
{
    class FilterConfigrBuilder<T> : IFilterConfigBuilder
    {
        readonly FilterConfig<T> _filterConfig; 
       
        public static IFilterConfigBuilder Create( FilterConfig<T> filterConfig)
        {
            return new FilterConfigrBuilder<T>(filterConfig);
        }


        private FilterConfigrBuilder(FilterConfig<T> config)
        {
            _filterConfig = config;
        }

        public IFilterConfigBuilder SetFilter<TFilter>() where TFilter : IFilter,new ()
        {
            _filterConfig.Filter = new TFilter();
            return this;
        }

    }
}