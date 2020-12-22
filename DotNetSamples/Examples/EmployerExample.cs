using DotNetSamples.Models;
using DotNetSamples.Services;

namespace DotNetSamples.Examples
{
    public class EmployerExample
    {
        /// <summary>
        /// Add a new employer example.
        /// </summary>
        /// <returns>Guid of new employer</returns>
        public static async Task<Guid> AddEmployerAsync()
        {
            // Create a new employer model.
            var newEmployer = new Employer
            {
                Title = "API Employer"
            };

            // Send to API.
            return await EmployerService.AddEmployerAsync(newEmployer);
        }
    }
}
