using DotNetSamples.Converters;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace DotNetSamples.Services
{
    public class UserContactService
    {
        /// <summary>
        /// Fetch list of UserContacts.
        /// </summary>
        /// <returns>List of UserContact objects.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<UserContact>> FetchUserContactListAsync()
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest("/api/v4/entity/UserContact/list");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<UserContactListResponse>(request);

            if (response.IsSuccessful && response.Data?.ResultCode == "OK")
            {
                return response.Data.List;
            }
            else
            {
                // If you get an unauthorized message here then your API key may not be set correctly.
                throw new Exception($"Error: {response.ErrorMessage ?? response.ErrorException?.Message}");
            }
        }

        /// <summary>
        /// Fetch a UserContact.
        /// </summary>
        /// <param name="rowUID">Unique identifier of the item.</param>
        /// <returns>UserContact object.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<UserContact> FetchUserContactAsync(Guid rowUID)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest($"/api/v4/entity/UserContact/fetch/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<UserContactFetchResponse>(request);

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
        /// Add a UserContact.
        /// </summary>
        /// <param name="userContact">UserContact to add.</param>
        /// <returns>RowUID of the newly added UserContact.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<Guid> AddUserContactAsync(UserContact userContact)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest("/api/v4/entity/UserContact/add");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(userContact);

            var response = await client.ExecutePostAsync<UserContactPostResponse>(request);

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
        /// Update a UserContact.
        /// </summary>
        /// <param name="userContact">UserContact to update.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task UpdateUserContactAsync(UserContact userContact)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest("/api/v4/entity/UserContact/update");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(userContact);

            var response = await client.ExecutePostAsync<UserContactPostResponse>(request);

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
        /// Delete a UserContact.
        /// </summary>
        /// <param name="rowUID">Unique identifier of the item.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task DeleteUserContactAsync(Guid rowUID)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest($"/api/v4/entity/UserContact/delete/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var response = await client.ExecuteGetAsync<UserContactPostResponse>(request);

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
