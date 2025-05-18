using DotNetSamples.Models;
using DotNetSamples.Services;

namespace DotNetSamples.Examples
{
    public class AttachmentExample
    {
        /// <summary>
        /// Add attachment example.
        /// </summary>
        /// <returns>Guid of new attachment</returns>
        public static async Task<Guid> AddAttachmentAsync()
        {
            // Create attachment model.
            var newAttachment = new AttachmentDef
            {
                RowUID = Guid.NewGuid(),
                FileName = "API.txt",
                ContentType = "text/plain",
                FileBytesBase64 = "QVBJIFRFU1Q=" // You do not need to initialize here, convert your file and assign it. This example is mocking a fake text file.
            };

            // Send to API.
            return await AttachmentService.AddAttachmentAsync(newAttachment);
        }
    }
}
