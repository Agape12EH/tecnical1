namespace tecnical1.Generic
{
    public class PaginadorGenerico<T> where T : class
    {
        public int CurrentPage { get; set; }

        public int RowsPerPage { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public string CurrentSearch { get; set; }

        public string CurrentOrder { get; set; }

        public string CurrentOrderType { get; set; }

        public IEnumerable<T> Result { get; set; }
    }
}
