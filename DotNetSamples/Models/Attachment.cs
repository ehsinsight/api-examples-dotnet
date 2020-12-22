namespace DotNetSamples.Models
{
    public class AttachmentPostResponse
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

    public class Attachment
    {
        public Guid? RowUID { get; set; }
        public string ChangeToken { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string FileBytesBase64 { get; set; }
    }
}
