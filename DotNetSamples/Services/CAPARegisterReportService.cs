using System.Text.Json;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;

namespace DotNetSamples.Services
{
    public class CAPARegisterReportService
    {

        /// <summary>
        /// Execute CAPA Register report targeting the CAPA named query.
        /// </summary>
        /// <param name="parameters">Parameters for the report.</param>
        /// <returns>List of CAPA Register report rows.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<CAPARegisterCAPA>> ExecuteCAPAReportAsync(string parameters)
        {
            var apiUrl = !string.IsNullOrEmpty(parameters) ? $"/api/v6/report/CAPARegister/CAPA/execute?{parameters}" : $"/api/v6/report/CAPARegister/execute";

            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest(apiUrl);
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<CAPARegisterCAPAResponse>(request);

            if (response.IsSuccessful || (response.Data != null && response.Data.ResultCode != null))
            {
                switch (response.Data?.ResultCode)
                {
                    case "OK":
                        return response.Data.Rows;

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
