using DotNetSamples.Models;
using DotNetSamples.Services;

namespace DotNetSamples.Examples
{
    public class AttachmentExamples
    {
        /// <summary>
        /// Add attachment example.
        /// </summary>
        /// <returns>Guid of new attachment</returns>
        public static async Task<Guid> AddAttachmentAsync()
        {
            // Create attachment model.
            var newAttachment = new Attachment
            {
                RowUID = Guid.Empty,
                FileName = "API.txt",
                ContentType = "text/html",
                FileBytesBase64 = "QVBJIFRFU1Q=" // You do not need to initialize here, convert your file and assign it. This example is mocking a fake text file.
            };

            // Send to API.
            return await AttachmentService.AddAttachmentAsync(newAttachment);
        }
    }
}
