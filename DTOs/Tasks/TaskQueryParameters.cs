namespace TaskFlow.Api.DTOs.Tasks
{
    public class TaskQueryParameters
    {
        private const int MaxPageSize = 50;
        public int Page { get; set; } = 1;
        public int _PageSize = 10;
        public int PageSize 
        {
            get => _PageSize;
                set=>_PageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
