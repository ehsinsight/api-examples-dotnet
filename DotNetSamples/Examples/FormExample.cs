using DotNetSamples.Services;

namespace DotNetSamples.Examples
{
    public class FormExample
    {
        /// <summary>
        /// Add a CAPA form example.
        /// </summary>
        /// <returns>Guid of new CAPA form</returns>
        public static async Task<Guid> AddCAPAFormAsync()
        {
            // Fetch Business Hierarchies list
            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();

            // Find the RowUID for the Scranton Business Entity.
            var hierarchyRowUID = hierarchyList.First(x => x.Title == "Scranton").RowUID;

            var newCapa = new Models.CAPA
            {
                RowUID = Guid.NewGuid(),
                BusinessEntity = hierarchyRowUID,
                Findings = "These are findings from an API test.",
                ActionDescription = "This is an action description from an API test.",
                IdentificationDate = DateTime.UtcNow.Date,
                FormNumber = "API-1111-0001" // FormNumber is set explicitly for the purpose of this test. If not supplied then the API endpoint assigns a FormNumber automatically. It is unnecessary to set this in normal conditions.
            };

            // Call to the API to add the record and receive the new CAPA form's RowUID.
            return await CAPAFormService.AddCAPAFormAsync(newCapa);
        }

        /// <summary>
        /// Update a CAPA form example.
        /// </summary>
        /// <returns></returns>
        public static async Task UpdateCAPAFormAsync(Guid rowUID)
        {
            // Fetch the existing record.
            var apiCapaForm = await CAPAFormService.FetchCAPAFormAsync(rowUID);

            // Update some properties.
            apiCapaForm.IdentificationDate = DateTime.UtcNow.AddDays(-2).Date;
            apiCapaForm.DueDate = DateTime.UtcNow.AddDays(7).Date;
            apiCapaForm.Findings = "This is an updated findings value from an API test.";

            // Call to the API to update the CAPA form.
            await CAPAFormService.UpdateCAPAFormAsync(apiCapaForm);
        }
    }
}
