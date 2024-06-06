namespace Tour_API.Helpers
{
    public class QueryObject
    {
        public string? Title { get; set; }
        public int? DestinationId { get; set; }
        public string? SortBy { get; set; }
        public bool IsDecsending { get; set; } = false;
    }
}
