namespace CareerTrack.Application.Paging
{
    public class PagingModel
    {
        const int maxPageSize = 20;

        public int PageNumber { get; set; } = 1;

        private int _pageSize { get; set; } = 10;

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string QueryFilter { get; set; } = "";

        public string Field { get; set; }

        public Order Order { get; set; }
    }

    public enum Order
    {
        asc = 1,
        desc
    }
}
