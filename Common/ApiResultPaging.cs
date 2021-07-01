namespace Common
{
    public class ApiResultPaging
    {
        public int? total { get; set; }
        public int? perPage { get; set; }
        public int? currentPage { get; set; }
        public int? lastPage { get; set; }
        public object apiResult { get; set; }
    }
}
