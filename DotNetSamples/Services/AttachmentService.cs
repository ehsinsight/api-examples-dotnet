using System.Text.Json;
using DotNetSamples.Models;
using RestSharp;
using RestSharp.Serializers.Json;

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
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest($"/api/v6/attachment/fetch/{rowUID}");
            request.AddHeader("X-ApiKey", Settings.ApiKey);

            var result = await client.DownloadStreamAsync(request);

            using var fileBytes = new MemoryStream();

            if (result != null)
            {
                await result.CopyToAsync(fileBytes);
                return fileBytes.ToArray();
            }
            else
            {
                throw new Exception("NotFound: Attachment not found.");
            }
        }

        /// <summary>
        /// Add an attachment.
        /// </summary>
        /// <param name="attachment">Attachment object to be created.</param>
        /// <returns>RowUID of newly added attachment.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<Guid> AddAttachmentAsync(AttachmentDef attachment)
        {
            var client = new RestClient(Settings.SiteUrl, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions()));

            var request = new RestRequest("/api/v6/attachment/add");
            request.AddHeader("X-ApiKey", Settings.ApiKey);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(attachment);

            var response = await client.ExecutePostAsync<AttachmentDefAddResponse>(request);

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
    }
}
