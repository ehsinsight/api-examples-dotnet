namespace DotNetSamples.Models
{
    public class UserRoleListResponse
    {
        public string ResultCode { get; set; }
        public List<UserRole> List { get; set; }
    }

    public class UserRole
    {
        public Guid? RowUID { get; set; }
        public string ChangeToken { get; set; }
        public Guid? UserUID { get; set; }
        public Guid? RoleUID { get; set; }
        public Guid? BusinessEntity { get; set; }
        public string RoleCode { get; set; }
    }
}
