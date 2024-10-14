namespace VezeetaApi.Dto
{
    public class RequestCountDto
    {
        public int TotalRequests { get; set; }
        public int PendingRequests { get; set; }
        public int CompletedRequests { get; set; }
        public int CancelledRequests { get; set; }
    }
}
