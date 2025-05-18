namespace DotNetSamples.Models
{
    public class HierarchyDefListResponse
    {
        public string ResultCode { get; set; }

        public List<HierarchyDefRow> List { get; set; }

        public string Description { get; set; }

        public List<ValidationMessage> Messages { get; set; }

        public string CorrelationID { get; set; }

        public class ValidationMessage
        {
            public string ValidationKey { get; set; }

            public string Message { get; set; }
        }
    }
}
