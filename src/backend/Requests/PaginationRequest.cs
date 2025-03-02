namespace CaoHub.Api.Requests
{
    public abstract record PaginationRequest
    {
        public int? Offset { get; set; }

        public int? Limit { get; set; }
    }
}
