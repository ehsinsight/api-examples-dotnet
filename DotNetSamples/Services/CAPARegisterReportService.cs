using DotNetSamples.Converters;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace DotNetSamples.Services
{
    public class CAPARegisterReportService
    {
        /// <summary>
        /// Execute CAPA Register report.
        /// </summary>
        /// <param name="parameters">Parameters for the report.</param>
        /// <returns>List of CAPA Register report rows.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<CAPARegisterReport>> ExecuteReportAsync(string? parameters)
        {
            var apiUrl = parameters?.Length > 0 ? $"/api/v4/report/CAPARegister/execute?{parameters}" : $"/api/v4/report/CAPARegister/execute";
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest(apiUrl);
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<CAPARegisterReportResponse>(request);

            // if check on response success - this is not the same as the result code. Check restsharp success and then the statuscode from api
            if (response.IsSuccessful && response.Data?.ResultCode == "OK")
            {
                return response.Data.Rows;
            }
            else
            {
                throw new Exception($"Error: {response.ErrorMessage ?? response.ErrorException?.Message}");
            }
        }

        /// <summary>
        /// Execute CAPA Register report with the CAPA named query.
        /// </summary>
        /// <param name="parameters">Parameters for the report.</param>
        /// <returns>List of CAPA Register report rows.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<CAPARegisterReport>> ExecuteCAPANamedReportAsync(string? parameters)
        {
            var apiUrl = parameters?.Length > 0 ? $"/api/v4/report/CAPARegister/CAPA/execute?{parameters}" : $"/api/v4/report/CAPARegister/execute";
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest(apiUrl);
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<CAPARegisterReportResponse>(request);

            // if check on response success - this is not the same as the result code. Check restsharp success and then the statuscode from api
            if (response.IsSuccessful && response.Data?.ResultCode == "OK")
            {
                return response.Data.Rows;
            }
            else
            {
                throw new Exception($"Error: {response.ErrorMessage ?? response.ErrorException?.Message}");
            }
        }
    }
}
