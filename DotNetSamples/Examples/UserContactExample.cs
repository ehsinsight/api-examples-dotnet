using DotNetSamples.Models;
using DotNetSamples.Services;

namespace DotNetSamples.Examples
{
    public class UserContactExample
    {
        /// <summary>
        /// Add a new user example.
        /// </summary>
        /// <returns>Guid of new user.</returns>
        public static async Task<Guid> AddUserAsync()
        {
            // Fetch some lists that we will need for foreign key value lookups.
            var roleList = await UserRoleService.FetchUserRoleListAsync();
            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();
            var positionList = await PositionService.FetchPositionListAsync();
            var employerList = await EmployerService.FetchEmployerListAsync();

            // Grab the RowUID Guids that we will use.
            var hierarchyRowUID = hierarchyList.First(x => x.Title == "Scranton").RowUID;
            var positionRowUID = positionList.First(x => x.Title == "Bookkeeper").RowUID;
            var employerRowUID = employerList.First(x => x.Title == "Contoso").RowUID;

            var rnd = new Random();

            // Create a new object to represent the JSON that we will eventually post to the API.
            var newUser = new UserContact
            {
                UserContactType = "User", // Valid values here include User, Guest, and Device.
                FullName = "API Example",
                IsEnabled = 1,
                AuthProvider = "Internal", // This could also be "SAML" if SSO is enabled.
                EmailAddress = "api.example@sts.local",

                // Username has to be unique
                Username = $"api.example_{rnd.Next(99)}", // This must be unique; our example is using rnd to accomplish this

                // Set properties needed because we are adding a "User". If we were adding a "Guest" or "Device", these would not be required.
                FirstName = "API",
                LastName = "Example",
                EmployeeID = rnd.Next(999).ToString(), // Unique for each user contact record, do not set this to be random!
                BusinessEntity = hierarchyRowUID,
                Position = positionRowUID,
                Employer = employerRowUID,
                RoleAssignmentType = "DirectAssignment",
            };

            // Add roles, which drive permissions in the site. In this example, we are adding "WfIncidentRead" for the entire hierarchy.
            var siteUserRole = new UserRole
            {
                RoleUID = roleList.First(x => x.RoleCode.ToString() == "WfIncidentRead").RowUID,

                // Find the "root" of the hierarchy tree.
                BusinessEntity = hierarchyList.First(x => x.ParentHierarchyUID == null).RowUID
            };

            newUser.UserRoles = new List<UserRole> { siteUserRole };

            // Call to the API to add the user and receive the new user's RowUID.
            return await UserContactService.AddUserContactAsync(newUser);
        }

        /// <summary>
        /// Add a new contact example.
        /// </summary>
        /// <returns>Guid of new contact.</returns>
        public static async Task<Guid> AddContactAsync()
        {
            // Fetch some lists that we will need for foreign key value lookups.
            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();
            var positionList = await PositionService.FetchPositionListAsync();
            var employerList = await EmployerService.FetchEmployerListAsync();
            var rnd = new Random();

            // Grab the RowUID Guids that we will use.
            var hierarchyRowUID = hierarchyList.First(x => x.Title == "Scranton").RowUID;
            var positionRowUID = positionList.First(x => x.Title == "Bookkeeper").RowUID;
            var employerRowUID = employerList.First(x => x.Title == "Contoso").RowUID;

            // Create a new object to represent the JSON that we will eventually post to the API.
            var newContact = new UserContact
            {
                FullName = "API Example",
                UserContactType = "Contact", // Valid values here include Contact and Unmanaged.
                FirstName = "API",
                LastName = "Example",
                IsEnabled = 1,

                // These next 3 fields would not be required for an Unmanaged Contact.
                BusinessEntity = hierarchyRowUID,
                Position = positionRowUID,
                Employer = employerRowUID,

                // Additional optional field.
                EmployeeID = "234567"
            };

            // Call to the API to add the record and receive the new contact's RowUID.
            return await UserContactService.AddUserContactAsync(newContact);
        }

        /// <summary>
        /// Update an existing user example.
        /// </summary>
        /// <returns></returns>
        public static async Task UpdateUserAsync()
        {
            // Fetch some lists that we will need for foreign key value lookups.
            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();
            var positionList = await PositionService.FetchPositionListAsync();
            var employerList = await EmployerService.FetchEmployerListAsync();

            // Grab the RowUID Guids that we will use for update.
            var hierarchyRowUID = hierarchyList.First(x => x.Title == "Houston").RowUID;
            var positionRowUID = positionList.First(x => x.Title == "Accountant").RowUID;
            var employerRowUID = employerList.First(x => x.Title == "Letterman Staffing").RowUID;

            var existingContacts = await UserContactService.FetchUserContactListAsync();

            // For this example, we will just edit the first item we find that matches a user we created by the AddContactExample method above, but you will normally want to find the contact based on their EmployeeID or some other key property.
            var existingContactRow = existingContacts.First(x => (x.FirstName ?? "").ToString() == "API");
            var existingRowUID = existingContactRow.RowUID;

            // Fetch the full record (because the "List" method used above only pulls back summary data about the items).
            var existingContact = await UserContactService.FetchUserContactAsync(existingRowUID.GetValueOrDefault());

            // Update some of the properties
            existingContact.BusinessEntity = hierarchyRowUID;
            existingContact.Position = positionRowUID;
            existingContact.Employer = employerRowUID;

            // Call to the API to update the record.
            await UserContactService.UpdateUserContactAsync(existingContact);
        }
    }
}
