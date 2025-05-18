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
            var roleList = await RoleService.FetchRoleListAsync();
            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();
            var positionList = await PositionService.FetchPositionListAsync();
            var employerList = await EmployerService.FetchEmployerListAsync();

            // Grab the RowUID Guids that we will use.
            var hierarchyRowUID = hierarchyList.First(x => x.Title == "Scranton").RowUID;
            var positionRowUID = positionList.First(x => x.Title == "Bookkeeper").RowUID;
            var employerRowUID = employerList.First(x => x.Title == "Contoso").RowUID;

            var randomNumber = new Random().Next(9999).ToString();

            var rowUID = Guid.NewGuid();

            // Create a new object to represent the JSON that we will eventually post to the API.
            var newUser = new UserContact
            {
                RowUID = rowUID,

                UserContactType = "User", // Valid values here include User, Guest, and Device.

                AuthProvider = "Internal", // This could also be "SAML" if SSO is enabled.

                IsEnabled = 1,

                FullName = "API Example",

                Username = $"api.example.{randomNumber}", // Username must be unique; our example is using a random number to accomplish this

                EmailAddress = "api.example@example.com",

                FirstName = "API",
                LastName = "Example",

                EmployeeID = randomNumber,      // Employe ID must be unique

                BusinessEntity = hierarchyRowUID,
                Position = positionRowUID,
                Employer = employerRowUID,

                RoleAssignmentType = "DirectAssignment"
            };

            // Add roles, which drive permissions in the site. In this example, we are adding "WfIncidentRead" for the entire hierarchy.
            var siteUserRole = new UserContactUserRoles()
            {
                RowUID = Guid.NewGuid(),

                RoleUID = roleList.First(x => x.RoleCode == "WfIncidentRead").RowUID,

                // Find the "root" of the hierarchy tree.
                BusinessEntity = hierarchyList.First(x => x.ParentHierarchyUID == null).RowUID
            };

            newUser.UserRoles = new List<UserContactUserRoles> { siteUserRole };

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

            // Grab the RowUID Guids that we will use.
            var hierarchyRowUID = hierarchyList.First(x => x.Title == "Scranton").RowUID;
            var positionRowUID = positionList.First(x => x.Title == "Bookkeeper").RowUID;
            var employerRowUID = employerList.First(x => x.Title == "Contoso").RowUID;

            // Create a new object to represent the JSON that we will eventually post to the API.
            var newContact = new UserContact
            {
                RowUID = Guid.NewGuid(),
                
                UserContactType = "Contact",

                IsEnabled = 1,

                FullName = "API Example",
                
                FirstName = "API",
                LastName = "Example",

                BusinessEntity = hierarchyRowUID,
                Position = positionRowUID,
                Employer = employerRowUID,

                EmailAddress = $"api.example@example.com",      // Additional optional field for a Contact
                EmployeeID = "234567"       // Additional optional field for a Contact
            };

            // Call to the API to add the record and receive the new contact's RowUID.
            return await UserContactService.AddUserContactAsync(newContact);
        }

        /// <summary>
        /// Update an existing user example.
        /// </summary>
        /// <returns></returns>
        public static async Task UpdateUserAsync(Guid userContactRowUID)
        {
            // Fetch some lists that we will need for foreign key value lookups.
            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();
            var positionList = await PositionService.FetchPositionListAsync();

            // Grab the RowUID Guids that we will use for update.
            var hierarchyRowUID = hierarchyList.First(x => x.Title == "Houston").RowUID;
            var positionRowUID = positionList.First(x => x.Title == "Accountant").RowUID;

            // Fetch the full record
            var existingContact = await UserContactService.FetchUserContactAsync(userContactRowUID);

            // Update some of the properties
            existingContact.BusinessEntity = hierarchyRowUID;
            existingContact.Position = positionRowUID;

            // Call to the API to update the record.
            await UserContactService.UpdateUserContactAsync(existingContact);
        }
    }
}
