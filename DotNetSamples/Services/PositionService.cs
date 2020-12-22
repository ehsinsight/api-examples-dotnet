using DotNetSamples.Converters;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace DotNetSamples.Services
{
    public class PositionService
    {
        /// <summary>
        /// Fetch list of positions.
        /// </summary>
        /// <returns>List of position objects.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<Position>> FetchPositionListAsync()
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest("/api/v4/entity/Position/list");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<PositionListResponse>(request);

            if (response.IsSuccessful && response.Data?.ResultCode == "OK")
            {
                return response.Data.List;
            }
            else
            {
                throw new Exception($"Error: {response.ErrorMessage ?? response.ErrorException?.Message}");
            }
        }

        /// <summary>
        /// Add a position.
        /// </summary>
        /// <param name="position">Position to add.</param>
        /// <returns>RowUID of newly added position.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<Guid> AddPositionAsync(Position position)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest("/api/v4/entity/Position/add");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(position);

            var response = await client.ExecutePostAsync<PositionPostRepsonse>(request);

            // if check on response success - this is not the same as the result code. Check restsharp success and then the statuscode from api
            if (response.IsSuccessful && response.Data?.ResultCode == "OK")
            {
                return response.Data.RowUID;
            }
            else
            {
                var errorMsg = $"Error: {response.ErrorMessage ?? response.ErrorException?.Message}";
                if (response.Data?.Messages?.Count > 0)
                {
                    errorMsg = $"Error(s): {string.Join(", ", response.Data.Messages.Select(x => x.Message))}";
                }
                else if (response.Data?.Description != null)
                {
                    errorMsg = $"Error: {response.Data?.Description}";
                }
                else if (response.ErrorMessage == null)
                {
                    errorMsg = "An unknown error occurred.";
                }

                throw new Exception(errorMsg);
            }
        }

        /// <summary>
        /// Delete a position.
        /// </summary>
        /// <param name="rowUID">Unique identifier of the item.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task DeletePositionAsync(Guid rowUID)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest($"/api/v4/entity/Position/delete/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<PositionPostRepsonse>(request);

            // if check on response success - this is not the same as the result code. Check restsharp success and then the statuscode from api
            if (!response.IsSuccessful || response.Data?.ResultCode != "OK")
            {
                var errorMsg = $"Error: {response.ErrorMessage ?? response.ErrorException?.Message}";
                if (response.Data?.Messages?.Count > 0)
                {
                    errorMsg = $"Error(s): {string.Join(", ", response.Data.Messages.Select(x => x.Message))}";
                }
                else if (response.Data?.Description != null)
                {
                    errorMsg = $"Error: {response.Data?.Description}";
                }
                else if (response.ErrorMessage == null)
                {
                    errorMsg = "An unknown error occurred.";
                }

                throw new Exception(errorMsg);
            }
        }
    }
}
