using DotNetSamples.Converters;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace DotNetSamples.Services
{
    public class UserRoleService
    {
        /// <summary>
        /// Fetch list of user roles.
        /// </summary>
        /// <returns>List of user role objects.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<UserRole>> FetchUserRoleListAsync()
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));

            var request = new RestRequest("/api/v4/role/list");

            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<UserRoleListResponse>(request);

            if (response.IsSuccessful && response.Data?.ResultCode == "OK")
            {
                return response.Data.List;
            }
            else
            {
                throw new Exception($"Error: {response.ErrorMessage ?? response.ErrorException?.Message}");
            }
        }
    }
}
