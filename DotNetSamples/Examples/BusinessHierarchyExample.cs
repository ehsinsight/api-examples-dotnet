using DotNetSamples.Models;
using DotNetSamples.Services;

namespace DotNetSamples.Examples
{
    public class BusinessHierarchyExample
    {
        /// <summary>
        /// Create a new business hierarchy example.
        /// </summary>
        /// <returns>Guid of new business hierarchy.</returns>
        public static async Task<Guid> AddBusinessHierarchyAsync()
        {
            // Fetch list of business entities and then get the "Manufacturing" RowUID from that list to use as the ParentHierarchyUID for our example. This will create the new entry as a sibling of "Scranton".
            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();
            var parentHierarchyRowUID = hierarchyList.First(x => x.Title == "Manufacturing").RowUID;

            // Create a new object to represent the JSON that we will eventually post to the API.
            var newHierarchy = new HierarchyDef()
            {
                RowUID = Guid.NewGuid(),
                Title = "API",
                Comments = "This is an API example.",
                StartDate = DateTime.UtcNow.Date,
                ParentHierarchyUID = parentHierarchyRowUID
            };

            // Call to the API to add the business hierarchy and receive the new business hierarchy's RowUID.
            return await BusinessHierarchyService.AddHierarchyAsync(newHierarchy);
        }

        /// <summary>
        /// Update a business hierarchy example.
        /// </summary>
        /// <returns></returns>
        public static async Task UpdateBusinessHierarchyAsync(Guid rowUID)
        {
            // Fetch list of business hierarchies and then get the API one from that to update.
            var hierarchy = await BusinessHierarchyService.FetchHierarchyAsync(rowUID);

            // Update some properties.
            hierarchy.Title = "API UPDATED";
            hierarchy.Comments = "This is an updated API example.";

            // Call to the API to update the business hierarchy.
            await BusinessHierarchyService.UpdateHierarchyAsync(hierarchy);
        }
    }
}
