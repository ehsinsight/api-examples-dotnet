using System.Text.Json;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;

namespace DotNetSamples.Services
{
    public class BusinessHierarchyService
    {
        /// <summary>
        /// Fetch list of business hierarchies.
        /// </summary>
        /// <returns>List of business hierarchy objects.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<HierarchyDefRow>> FetchHierarchyListAsync()
        {
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest("/api/v6/hierarchy/list");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<HierarchyDefListResponse>(request);

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
        /// Fetch a business hierarchy.
        /// </summary>
        /// <param name="rowUID">Unique identifier of the item.</param>
        /// <returns>Business hierarchy object.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<HierarchyDef> FetchHierarchyAsync(Guid rowUID)
        {
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest($"/api/v6/hierarchy/fetch/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<HierarchyDefFetchResponse>(request);

            if (response.IsSuccessful || (response.Data != null && response.Data.ResultCode != null))
            {
                switch (response.Data?.ResultCode)
                {
                    case "OK":
                        return response.Data.Hierarchy;

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
        /// Add a business hierarchy.
        /// </summary>
        /// <param name="businessHierarchy">Business hierarchy to add.</param>
        /// <returns>RowUID of newly added business hierarchy.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<Guid> AddHierarchyAsync(HierarchyDef businessHierarchy)
        {
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest("/api/v6/hierarchy/add");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(businessHierarchy);

            var response = await client.ExecutePostAsync<HierarchyDefAddResponse>(request);

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
        /// Update a business hierarchy.
        /// </summary>
        /// <param name="businessHierarchy">Business hierarchy to update.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task UpdateHierarchyAsync(HierarchyDef businessHierarchy)
        {
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest("/api/v6/hierarchy/update");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(businessHierarchy);

            var response = await client.ExecutePostAsync<HierarchyDefUpdateResponse>(request);

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

        /// <summary>
        /// Delete a business hierarchy.
        /// </summary>
        /// <param name="rowUID">Unique identifier of the item.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task DeleteHierarchyAsync(Guid rowUID)
        {
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest($"/api/v6/hierarchy/delete/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<HierarchyDefDeleteResponse>(request);

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
