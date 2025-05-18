namespace DotNetSamples.Models
{ 
    public class CAPA
    {              
        public Guid RowUID { get; set; }     

        public DateTime? CreatedDtm { get; set; }      

        public DateTime? UpdatedDtm { get; set; }      

        public string ChangeToken { get; set; }       

        public string FormNumber { get; set; }     

        public double? IsComplete { get; set; }      

        public double? IsGeneratedClosed { get; set; }      

        public DateTime? ClosedDate { get; set; }      

        public Guid? Originator { get; set; }       

        public Guid? ParentForm { get; set; }       

        public Guid? GenerationBatch { get; set; }      

        public Guid? QuestionSet { get; set; }

        public string IdentificationSource { get; set; }

        public Guid? IdentifyingPerson { get; set; }

        public DateTime? IdentificationDate { get; set; }

        public Guid? BusinessEntity { get; set; }

        public double? IsSingleAssignment { get; set; }

        public Guid? AssignedTo { get; set; }

        public Guid? AssignedRole { get; set; }

        public Guid? ActionType { get; set; }

        public DateTime? DueDate { get; set; }

        public string Findings { get; set; }

        public string ActionDescription { get; set; }

        public Guid? Priority { get; set; }

        public double? IsScheduleEscalation { get; set; }

        public double? IsSingleEscalation { get; set; }

        public Guid? EscalateTo { get; set; }

        public Guid? EscalateRole { get; set; }

        public DateTime? EscalationDate { get; set; }

        public double? IsScheduleReview { get; set; }

        public double? IsSingleReview { get; set; }

        public string ActionTaken { get; set; }

        public DateTime? CompletedDate { get; set; }

        public string ProgressComments { get; set; }
    }
}
