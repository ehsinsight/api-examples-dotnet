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
            // For this example, first fetch the existing position list so that we find a valid position family UID.
            var positionList = await PositionService.FetchPositionListAsync();
            var position = positionList.First(x => x.Title == "Bookkeeper");
            var positionFamily = position?.PositionFamily;

            // Construct a new position object.
            var newPosition = new Position
            {
                Title = "API Position",
                PositionFamily = positionFamily
            };

            // Send to API.
            return await PositionService.AddPositionAsync(newPosition);
        }
    }
}
