namespace DotNetSamples.Models
{
    public class CAPAListResponse
    {
        public string ResultCode { get; set; }
        public List<CAPA> List { get; set; }
    }

    public class CAPAFetchResponse
    {
        public string ResultCode { get; set; }
        public CAPA Entity { get; set; }
    }

    public class CAPAPostResponse
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

    public class CAPA
    {
        public Guid? RowUID { get; set; }
        public string ChangeToken { get; set; }
        public DateTime? CreatedDtm { get; set; }
        public DateTime? UpdatedDtm { get; set; }
        public string FormNumber { get; set; }
        public int? IsComplete { get; set; }
        public int? IsGeneratedClosed { get; set; }
        public DateTime? ClosedDate { get; set; }
        public Guid? Originator { get; set; }
        public Guid? IdentifyingPerson { get; set; }
        public Guid? ParentForm { get; set; }
        public Guid? QuestionSet { get; set; }
        public string? IdentificationSource { get; set; }
        public DateTime? IdentificationDate { get; set; }
        public Guid? BusinessEntity { get; set; }
        public string? ActionType { get; set; }
        public string? ActionType_Other { get; set; }
        public Guid? AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }
        public string Findings { get; set; }
        public string ActionDescription { get; set; }
    }
}
