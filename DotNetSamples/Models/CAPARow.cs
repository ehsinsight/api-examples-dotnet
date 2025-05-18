namespace DotNetSamples.Models
{
    public class CAPARow
    {
        public Guid RowUID { get; set; }

        public DateTime? CreatedDtm { get; set; }
    
        public DateTime? UpdatedDtm { get; set; }
     
        public string ChangeToken { get; set; }
 
        public string FormNumber { get; set; }

        public DateTime? IdentificationDate { get; set; }

        public Guid? BusinessEntity { get; set; }

        public string ActionDescription { get; set; }
    }
}
