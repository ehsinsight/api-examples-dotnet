namespace DotNetSamples.Models
{
    public class BusinessHierarchyListResponse
    {
        public string ResultCode { get; set; }
        public List<BusinessHierarchy> List { get; set; }
    }

    public class BusinessHierarchyFetchResponse
    {
        public string ResultCode { get; set; }
        public BusinessHierarchy Hierarchy { get; set; }
    }

    public class BusinessHierarchyPostResponse
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

    public class BusinessHierarchy
    {
        public Guid? RowUID { get; set; }
        public string ChangeToken { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Comments { get; set; }
        public string? LatLong { get; set; }
        public Guid? ParentHierarchyUID { get; set; }
        public Guid? Type { get; set; }
        public Guid? SubType { get; set; }
    }
}
