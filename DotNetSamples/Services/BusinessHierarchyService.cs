using DotNetSamples.Converters;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace DotNetSamples.Services
{
    public class BusinessHierarchyService
    {
        /// <summary>
        /// Fetch list of business hierarchies.
        /// </summary>
        /// <returns>List of business hierarchy objects.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<BusinessHierarchy>> FetchHierarchyListAsync()
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest("/api/v4/hierarchy/list");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<BusinessHierarchyListResponse>(request);

            // if check on response success - this is not the same as the result code. Check restsharp success and then the statuscode from api
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
        /// Fetch a business hierarchy.
        /// </summary>
        /// <param name="rowUID">Unique identifier of the item.</param>
        /// <returns>Business hierarchy object.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<BusinessHierarchy> FetchHierarchyAsync(Guid rowUID)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest($"/api/v4/hierarchy/fetch/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<BusinessHierarchyFetchResponse>(request);

            // if check on response success - this is not the same as the result code. Check restsharp success and then the statuscode from api
            if (response.IsSuccessful && response.Data?.ResultCode == "OK")
            {
                return response.Data.Hierarchy;
            }
            else
            {
                throw new Exception($"Error: {response.ErrorMessage ?? response.ErrorException?.Message}");
            }
        }

        /// <summary>
        /// Add a business hierarchy.
        /// </summary>
        /// <param name="businessHierarchy">Business hierarchy to add.</param>
        /// <returns>RowUID of newly added business hierarchy.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<Guid> AddHierarchyAsync(BusinessHierarchy businessHierarchy)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest("/api/v4/hierarchy/add");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(businessHierarchy);

            var response = await client.ExecutePostAsync<BusinessHierarchyPostResponse>(request);

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
        /// Update a business hierarchy.
        /// </summary>
        /// <param name="businessHierarchy">Business hierarchy to update.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task UpdateHierarchyAsync(BusinessHierarchy businessHierarchy)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest("/api/v4/hierarchy/update");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(businessHierarchy);

            var response = await client.ExecutePostAsync<BusinessHierarchyPostResponse>(request);

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

        /// <summary>
        /// Delete a business hierarchy.
        /// </summary>
        /// <param name="rowUID">Unique identifier of the item.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task DeleteHierarchyAsync(Guid rowUID)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest($"/api/v4/hierarchy/delete/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<BusinessHierarchyPostResponse>(request);

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
