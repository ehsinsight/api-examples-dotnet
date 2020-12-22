namespace DotNetSamples.Models
{
    public class UserContactListResponse
    {
        public string ResultCode { get; set; }
        public List<UserContact> List { get; set; }
    }

    public class UserContactFetchResponse
    {
        public string ResultCode { get; set; }
        public UserContact Entity { get; set; }
    }

    public class UserContactPostResponse
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

    public class UserContact
    {
        public Guid? RowUID { get; set; }
        public string ChangeToken { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string? UserContactType { get; set; }
        public string EmployeeID { get; set; }
        public string? UserContactNumber { get; set; }
        public Guid? BusinessEntity { get; set; }
        public Guid? Position { get; set; }
        public Guid? Employer { get; set; }
        public string? AuthProvider { get; set; }
        public int? IsEnabled { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleAssignmentType { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
