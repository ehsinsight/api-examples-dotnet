using System.Text.Json;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;

namespace DotNetSamples.Services
{
    public class EmployerService
    {
        /// <summary>
        /// Fetch list of employers.
        /// </summary>
        /// <returns>List of employer objects.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<EmployerRow>> FetchEmployerListAsync()
        {
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest("/api/v6/entity/Employer/list");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<EmployerListResponse>(request);

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
        /// Add an employer.
        /// </summary>
        /// <param name="employer">Employer to add.</param>
        /// <returns>RowUID of newly added employer.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<Guid> AddEmployerAsync(Employer employer)
        {
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest("/api/v6/entity/Employer/add");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(employer);

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
        /// Delete an employer.
        /// </summary>
        /// <param name="rowUID">Unique identifier of the item.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task DeleteEmployerAsync(Guid rowUID)
        {
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest($"/api/v6/entity/Employer/delete/{rowUID}");
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
