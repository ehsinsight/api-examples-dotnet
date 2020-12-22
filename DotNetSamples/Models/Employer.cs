namespace DotNetSamples.Models
{
    public class EmployerListResponse
    {
        public string ResultCode { get; set; }
        public List<Employer> List { get; set; }
    }

    public class EmployerPostResponse
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

    public class Employer
    {
        public Guid? RowUID { get; set; }
        public string ChangeToken { get; set; }
        public string Title { get; set; }
    }
}
