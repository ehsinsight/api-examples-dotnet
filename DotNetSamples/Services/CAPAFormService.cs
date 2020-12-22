using DotNetSamples.Converters;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace DotNetSamples.Services
{
    public class CAPAFormService
    {
        /// <summary>
        /// Fetch the list of CAPA forms.
        /// </summary>
        /// <param name="parameters">Query string to filter by.</param>
        /// <returns>List of CAPA form objects.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<CAPA>> FetchCAPAFormListAsync(string? parameters)
        {
            var apiUrl = parameters?.Length > 0 ? $"/api/v4/entity/CAPA/list?{parameters}" : $"/api/v4/entity/CAPA/list";

            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));

            var request = new RestRequest(apiUrl);
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<CAPAListResponse>(request);

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
        /// Fetch a CAPA form.
        /// </summary>
        /// <param name="rowUID">Unique identifier of the item.</param>
        /// <returns>CAPA form object.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<CAPA> FetchCAPAFormAsync(Guid rowUID)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest($"/api/v4/entity/CAPA/fetch/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<CAPAFetchResponse>(request);

            // if check on response success - this is not the same as the result code. Check restsharp success and then the statuscode from api
            if (response.IsSuccessful && response.Data?.ResultCode == "OK")
            {
                return response.Data.Entity;
            }
            else
            {
                throw new Exception($"Error: {response.ErrorMessage ?? response.ErrorException?.Message}");
            }
        }

        /// <summary>
        /// Add a CAPA form.
        /// </summary>
        /// <param name="capa">CAPA form to add.</param>
        /// <returns>RowUID of newly added CAPA form.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<Guid> AddCAPAFormAsync(CAPA capa)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest("/api/v4/entity/CAPA/add");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(capa);

            var response = await client.ExecutePostAsync<CAPAPostResponse>(request);

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
        /// Update a CAPA form.
        /// </summary>
        /// <param name="capa">CAPA form to update.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task UpdateCAPAFormAsync(CAPA capa)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest("/api/v4/entity/CAPA/update");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(capa);

            var response = await client.ExecutePostAsync<CAPAPostResponse>(request);

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
        /// Delete a CAPA form.
        /// </summary>
        /// <param name="rowUID">Unique identifier of the item.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task DeleteCAPAFormAsync(Guid rowUID)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest($"/api/v4/entity/CAPA/delete/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<CAPAPostResponse>(request);

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
