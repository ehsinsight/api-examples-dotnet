namespace DotNetSamples.Models
{
    public class HierarchyDefFetchResponse
    {
        public string ResultCode { get; set; }

        public HierarchyDef Hierarchy { get; set; }

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
