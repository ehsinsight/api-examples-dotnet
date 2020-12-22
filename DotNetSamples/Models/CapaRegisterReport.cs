namespace DotNetSamples.Models
{
    public class CAPARegisterReportResponse
    {
        public string ResultCode { get; set; }
        public List<CAPARegisterReport> Rows { get; set; }
    }

    public class CAPARegisterReport
    {
        public string? Form { get; set; }
        public string? BusinessEntity { get; set; }
        public DateTime? IdentificationDate { get; set; }
        public string? ActionType { get; set; }
        public string? Source { get; set; }
        public string? RecommendedActionDescription { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime? CurrentDueDate { get; set; }
        public int? DaysOverdue { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? CompletionDays { get; set; }
        public string? Workflow { get; set; }
        public string? Status { get; set; }
    }
}
