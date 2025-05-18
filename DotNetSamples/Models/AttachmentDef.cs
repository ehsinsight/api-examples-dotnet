namespace DotNetSamples.Models
{
    public class AttachmentDef
    {
        public Guid? RowUID { get; set; }

        public string ChangeToken { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public string FileBytesBase64 { get; set; }
    }
}
