using System.Text.Json;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;

namespace DotNetSamples.Services
{
    public class PositionService
    {
        /// <summary>
        /// Fetch list of positions.
        /// </summary>
        /// <returns>List of position objects.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<PositionRow>> FetchPositionListAsync()
        {
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest("/api/v6/entity/Position/list");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<PositionListResponse>(request);

            if (response.IsSuccessful || (response.Data != null && response.Data.ResultCode != null))
            {
                switch (response.Data?.ResultCode)
                {
                    case "OK":
                        return response.Data.List;

                    case "Validation":
                        throw new Exception($"Validation: {response.Data.Description} - {string.Join(", ", response.Data.Messages?.Select(x => x.Message) ?? [])}");
                    case "Exception":
                        throw new Exception($"Exception: {response.Data.Description} ({response.Data.CorrelationID})");
                    case "NotFound":
                        throw new Exception($"NotFound: {response.Data.Description}");
                    case "Forbidden":
                        throw new Exception($"Forbidden: {response.Data.Description}");
                    default:
                        throw new Exception($"Error: {response.Data?.Description}");
                }
            }
            else
            {
                throw new Exception($"RequestError: {response.ErrorMessage ?? response.Content}", response.ErrorException);
            }
        }

        /// <summary>
        /// Fetch a Position.
        /// </summary>
        /// <param name="rowUID">Unique identifier of the item.</param>
        /// <returns>Position object.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<Position> FetchPositionAsync(Guid rowUID)
        {
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest($"/api/v6/entity/Position/fetch/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<PositionFetchResponse>(request);

            if (response.IsSuccessful || (response.Data != null && response.Data.ResultCode != null))
            {
                switch (response.Data?.ResultCode)
                {
                    case "OK":
                        return response.Data.Entity;

                    case "Validation":
                        throw new Exception($"Validation: {response.Data.Description} - {string.Join(", ", response.Data.Messages?.Select(x => x.Message) ?? [])}");
                    case "Exception":
                        throw new Exception($"Exception: {response.Data.Description} ({response.Data.CorrelationID})");
                    case "NotFound":
                        throw new Exception($"NotFound: {response.Data.Description}");
                    case "Forbidden":
                        throw new Exception($"Forbidden: {response.Data.Description}");
                    default:
                        throw new Exception($"Error: {response.Data?.Description}");
                }
            }
            else
            {
                throw new Exception($"NetworkError: {response.ErrorMessage}", response.ErrorException);
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
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest("/api/v6/entity/Position/add");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(position);

            var response = await client.ExecutePostAsync<EntityAddResponse>(request);

            if (response.IsSuccessful || (response.Data != null && response.Data.ResultCode != null))
            {
                switch (response.Data?.ResultCode)
                {
                    case "OK":
                        return response.Data.RowUID.Value;

                    case "Validation":
                        throw new Exception($"Validation: {response.Data.Description} - {string.Join(", ", response.Data.Messages?.Select(x => x.Message) ?? [])}");
                    case "Exception":
                        throw new Exception($"Exception: {response.Data.Description} ({response.Data.CorrelationID})");
                    case "NotFound":
                        throw new Exception($"NotFound: {response.Data.Description}");
                    case "Forbidden":
                        throw new Exception($"Forbidden: {response.Data.Description}");
                    default:
                        throw new Exception($"Error: {response.Data?.Description}");
                }
            }
            else
            {
                throw new Exception($"RequestError: {response.ErrorMessage ?? response.Content}", response.ErrorException);
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
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest($"/api/v6/entity/Position/delete/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<EntityAddResponse>(request);

            if (response.IsSuccessful || (response.Data != null && response.Data.ResultCode != null))
            {
                switch (response.Data?.ResultCode)
                {
                    case "OK":
                        return;

                    case "Validation":
                        throw new Exception($"Validation: {response.Data.Description} - {string.Join(", ", response.Data.Messages?.Select(x => x.Message) ?? [])}");
                    case "Exception":
                        throw new Exception($"Exception: {response.Data.Description} ({response.Data.CorrelationID})");
                    case "NotFound":
                        throw new Exception($"NotFound: {response.Data.Description}");
                    case "Forbidden":
                        throw new Exception($"Forbidden: {response.Data.Description}");
                    default:
                        throw new Exception($"Error: {response.Data?.Description}");
                }
            }
            else
            {
                throw new Exception($"RequestError: {response.ErrorMessage ?? response.Content}", response.ErrorException);
            }
        }
    }
}
