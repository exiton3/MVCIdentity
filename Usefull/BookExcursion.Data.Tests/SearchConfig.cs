namespace BookExcursion.Data.Tests
{
    class SearchConfig : ConfigSearch<SearchDto>
    {
        public SearchConfig()
        {
            For(x => x.Name).SetFilter<SimpleFilter>();
            For(x => x.Date).SetFilter<DateRangeFilter>();
            For(x => x.Number).SetFilter<MultiSelectFilter>();
        } 
    }
}