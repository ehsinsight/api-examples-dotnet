namespace DotNetSamples.Models
{
    public class UserContactRow
    {
        public Guid? RowUID { get; set; }

        public string ChangeToken { get; set; }

        public string UserContactNumber { get; set; }

        public string UserContactType { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public int? IsEnabled { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid? BusinessEntity { get; set; }

        public Guid? Employer { get; set; }

        public Guid? Position { get; set; }

        public string EmployeeID { get; set; }

        public string AuthProvider { get; set; }

        public string Username { get; set; }

        public string RoleAssignmentType { get; set; }
    }
}