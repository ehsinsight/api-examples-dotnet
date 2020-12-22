using DotNetSamples.Models;
using DotNetSamples.Services;

namespace DotNetSamples.Examples
{
    public class BusinessHierarchyExamples
    {
        /// <summary>
        /// Create a new business hierarchy example.
        /// </summary>
        /// <returns>Guid of new business hierarchy.</returns>
        public static async Task<Guid> AddBusinessHierarchyAsync()
        {
            // Fetch list of business hierarchies and then get the  "Manufacturing" RowUID from that list to use as the ParentHierarchyUID for our example which so it becomes a sibling of "Scranton".
            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();
            var parentHierarchyRowUID = hierarchyList.First(x => x.Title == "Manufacturing").RowUID;

            // Create a new object to represent the JSON that we will eventually post to the API.
            var newHierarchy = new BusinessHierarchy
            {
                RowUID = Guid.Empty, // note: pass an empty guid to create a new hierarchy
                Title = "API",
                Comments = "This is an API example",
                StartDate = DateTime.UtcNow.Date.Date,
                ParentHierarchyUID = parentHierarchyRowUID
            };

            // Call to the API to add the business hierarchy and receive the new business hierarchy's RowUID.
            return await BusinessHierarchyService.AddHierarchyAsync(newHierarchy);
        }

        /// <summary>
        /// Update a business hierarchy example.
        /// </summary>
        /// <returns></returns>
        public static async Task UpdateBusinessHierarchyAsync()
        {
            // Fetch list of business hierarchies and then get the API one from that to update.
            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();
            var apiHierarchy = hierarchyList.First(x => x.Title == "API");

            // Update some properties.
            apiHierarchy.Title = "API EDIT";
            apiHierarchy.Comments = "We edited this!";

            // Call to the API to update the business hierarchy.
            await BusinessHierarchyService.UpdateHierarchyAsync(apiHierarchy);
        }
    }
}
