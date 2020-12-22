using DotNetSamples.Models;
using DotNetSamples.Services;

namespace DotNetSamples.Examples
{
    public class PositionExample
    {
        /// <summary>
        /// Add a new position example.
        /// </summary>
        /// <returns>Guid of new position</returns>
        public static async Task<Guid> AddPositionAsync()
        {
            // Grab an existing position family Guid.
            var positionList = await PositionService.FetchPositionListAsync();
            var positionFamilyUID = positionList.First(x => x.Title == "Bookkeeper").PostionFamily;

            // Construct a new position object.
            var newPosition = new Position
            {
                Title = "API Position",
                PostionFamily = positionFamilyUID
            };

            // Send to API.
            return await PositionService.AddPositionAsync(newPosition);
        }
    }
}
