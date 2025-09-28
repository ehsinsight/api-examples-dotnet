using System.Text.Json;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;

namespace DotNetSamples.Services
{
    public class RoleService
    {
        public static async Task<List<RoleDef>> FetchRoleListAsync()
        {
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest("/api/v6/role/list");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<RoleDefListResponse>(request);

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
    }
}
