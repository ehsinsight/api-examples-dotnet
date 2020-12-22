namespace DotNetSamples.Models
{
    public class PositionListResponse
    {
        public string ResultCode { get; set; }
        public List<Position> List { get; set; }
    }

    public class PositionPostRepsonse
    {
        public string ResultCode { get; set; }
        public Guid RowUID { get; set; }
        public string? Description { get; set; }
        public List<ErrorMessage>? Messages { get; set; }

        public class ErrorMessage
        {
            public string? ValidationKey { get; set; }
            public string? Message { get; set; }
        }
    }

    public class Position
    {
        public Guid? RowUID { get; set; }
        public string ChangeToken { get; set; }
        public string Title { get; set; }
        public Guid PostionFamily { get; set; }
    }
}
