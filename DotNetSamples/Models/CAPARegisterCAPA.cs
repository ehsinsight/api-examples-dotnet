namespace DotNetSamples.Models
{
    public class CAPARegisterCAPA
    {
        public string FormNumber { get; set; }

        public string BusinessEntity { get; set; }

        public DateTime? IdentificationDate { get; set; }

        public string ActionType { get; set; }

        public string ActionDescription { get; set; }

        public string AssignedTo { get; set; }

        public string AssignedRole { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public int? IsComplete { get; set; }
    }
}
