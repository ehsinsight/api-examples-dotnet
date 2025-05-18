namespace DotNetSamples.Models
{
    public class PositionRow
    {
        public Guid? RowUID { get; set; }

        public string ChangeToken { get; set; }

        public string Title { get; set; }

        public Guid? PositionFamily { get; set; }
    }
}
