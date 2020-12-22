using DotNetSamples.Converters;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace DotNetSamples.Services
{
    public class AttachmentService
    {
        /// <summary>
        /// Fetch an attachment.
        /// </summary>
        /// <param name="rowUID">Unique identifier of the item.</param>
        /// <returns>Attachment object.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<byte[]> FetchAttachmentAsync(Guid rowUID)
        {
            var client = new RestClient(Settings.SiteUrl);
            var request = new RestRequest($"/api/v4/attachment/fetch/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var results = await client.DownloadStreamAsync(request);

            using var fileBytes = new MemoryStream();
            await results.CopyToAsync(fileBytes);
            return fileBytes.ToArray();
        }

        /// <summary>
        /// Add an attachment.
        /// </summary>
        /// <param name="attachment">Attachment object to be created.</param>
        /// <returns>RowUID of newly added attachment.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<Guid> AddAttachmentAsync(Attachment attachment)
        {
            // Options must be included for correct DateTime parsing from the API.
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeStringConverter());
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(options));
            var request = new RestRequest("/api/v4/attachment/add");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(attachment);

            var response = await client.ExecutePostAsync<AttachmentPostResponse>(request);

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
    }
}
