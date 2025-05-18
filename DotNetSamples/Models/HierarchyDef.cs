namespace DotNetSamples.Models
{
    public class HierarchyDef
    {
        public Guid RowUID { get; set; }
        
        public string ChangeToken { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Comments { get; set; }

        public string LatLong { get; set; }

        public Guid? ParentHierarchyUID { get; set; }

        public Guid? Type { get; set; }

        public Guid? SubType { get; set; }
    }
}
