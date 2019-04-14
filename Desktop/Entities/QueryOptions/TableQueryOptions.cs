namespace Entities.QueryOptions
{
    public class TableQueryOptions
    {
        public int Offset { get; set; }

        public int Limit { get; set; }

        public string SearchTerm { get; set; }

        public string SortBy { get; set; }

        public string SortType { get; set; }
    }
}
