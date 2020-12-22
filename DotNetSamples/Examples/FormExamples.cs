using DotNetSamples.Services;

namespace DotNetSamples.Examples
{
    public class FormExamples
    {
        /// <summary>
        /// Add a CAPA form example.
        /// </summary>
        /// <returns>Guid of new CAPA form</returns>
        public static async Task<Guid> AddCAPAFormAsync()
        {
            // Fetch Business Hierarchies list to build the filter.
            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();

            // For this example we will just grab the business hierarchy for Scranton.
            var hierarchyRowUID = hierarchyList.First(x => x.Title == "Scranton").RowUID;

            var newCapa = new Models.CAPA
            {
                BusinessEntity = hierarchyRowUID,
                Findings = "This is an API test.",
                ActionDescription = "Something needs to be done to resolve this",
                CreatedDtm = DateTime.UtcNow,
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
        public static async Task UpdateCAPAFormAsync()
        {
            // Fetch all CAPA forms.
            var capaFormList = await CAPAFormService.FetchCAPAFormListAsync(null);

            // Filter to get the API test form.
            var apiCapaForm = capaFormList.First(x => x.FormNumber.StartsWith("API"));

            // Update some properties.
            apiCapaForm.IdentificationDate = DateTime.UtcNow.AddDays(-2).Date;
            apiCapaForm.DueDate = DateTime.UtcNow.AddDays(7).Date;
            apiCapaForm.UpdatedDtm = DateTime.UtcNow;
            apiCapaForm.Findings = "API test update.";

            // Call to the API to update the CAPA form.
            await CAPAFormService.UpdateCAPAFormAsync(apiCapaForm);
        }
    }
}
