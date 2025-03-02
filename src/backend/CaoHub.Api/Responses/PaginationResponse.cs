namespace CaoHub.Api.Responses
{
    public abstract record PaginationResponse
    {
        public int Offset { get; set; }

        public int Limit { get; set; }

        public int TotalCount { get; set; }
    }
}
