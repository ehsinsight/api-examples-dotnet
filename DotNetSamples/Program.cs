using DotNetSamples.Examples;
using DotNetSamples.Services;

namespace DotNetSamples
{
    internal class Program
    {
        static async Task Main()
        {
            try
            {
                // CAUTION:

                // DO NOT RUN API EXAMPLES AGAINST A PRODUCTION SITE.

                // These samples are not intended to be run in production. They are for demonstration purposes only.
                // They may create, update, or delete data in your system. Use with caution.

                // Several of the samples depend on the sample data included in TRIAL sites, such as a business entity named "Scranton" and a position named "Bookkeeper". 
                // If you are using a SANDBOX site, some adjustments will be required to adapt the samples to your data.

                
                await UserContactExamples();
                await ReportExamples();
                await FormExamples();
                await BusinessHierarchyExamples();
                await EmployerExamples();
                await PositionExamples();
                await AttachmentExamples();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static async Task UserContactExamples()
        {
            Console.WriteLine("================================================================================");
            
            var userContacts = await UserContactService.FetchUserContactListAsync();

            foreach (var userContact in userContacts)
            {
                Console.WriteLine($"RowUID: {userContact.RowUID} - FullName: {userContact.FullName} - BusinessEntity: {userContact.BusinessEntity}");
            }

            Console.WriteLine("================================================================================");

            var newRowUID = await UserContactExample.AddUserAsync();

            Console.WriteLine($"New User - RowUID: {newRowUID}");

            Console.WriteLine("================================================================================");

            var newUser = await UserContactService.FetchUserContactAsync(newRowUID);

            Console.WriteLine($"Fetched User - RowUID: {newUser.RowUID} - FullName: {newUser.FullName} - BusinessEntity: {newUser.BusinessEntity} - Position: {newUser.Position} - Employer: {newUser.Employer}");

            Console.WriteLine("================================================================================");

            await UserContactExample.UpdateUserAsync(newRowUID);

            newUser = await UserContactService.FetchUserContactAsync(newRowUID);

            Console.WriteLine($"Updated User - RowUID: {newUser.RowUID} - FullName: {newUser.FullName} - BusinessEntity: {newUser.BusinessEntity} - Position: {newUser.Position} - Employer: {newUser.Employer}");

            Console.WriteLine("================================================================================");

            await UserContactService.DeleteUserContactAsync(newRowUID);

            Console.WriteLine($"Deleted User - RowUID: {newRowUID}");
        }

        static async Task ReportExamples()
        {
            Console.WriteLine("================================================================================");

            var reportAllRows = await ReportExecuteExample.GetCAPARegisterReportWithDefaultParametersAsync();

            Console.WriteLine("CAPA Register report with default parameters:");

            foreach (var row in reportAllRows)
            {
                var daysOverdue = row.IsComplete == 1 || !row.DueDate.HasValue || row.DueDate.Value > DateTime.Today ? 0 : (DateTime.Today - row.DueDate.Value).Days;

                Console.WriteLine("{");
                Console.WriteLine($"    FormNumber: {row.FormNumber}");
                Console.WriteLine($"    BusinessEntity: {row.BusinessEntity}");
                Console.WriteLine($"    IdentificationDate: {row.IdentificationDate}");
                Console.WriteLine($"    ActionType: {row.ActionType}");
                Console.WriteLine($"    ActionDescription: {row.ActionDescription}");
                Console.WriteLine($"    AssignedTo: {row.AssignedTo}");
                Console.WriteLine($"    DueDate: {row.DueDate}");
                Console.WriteLine($"    CompletedDate: {row.CompletedDate}");
                Console.WriteLine($"    DaysOverDue: {daysOverdue}");
                Console.WriteLine("}");
                Console.WriteLine();
            }

            Console.WriteLine("================================================================================");

            var reportScrantonRows = await ReportExecuteExample.GetCAPARegisterReportPastYearScrantonAsync();

            Console.WriteLine("CAPA Register report for past year at Scranton Business Entity:");

            foreach (var row in reportScrantonRows)
            {
                var daysOverdue = row.IsComplete == 1 || !row.DueDate.HasValue || row.DueDate.Value > DateTime.Today ? 0 : (DateTime.Today - row.DueDate.Value).Days;

                Console.WriteLine("{");
                Console.WriteLine($"    FormNumber: {row.FormNumber}");
                Console.WriteLine($"    BusinessEntity: {row.BusinessEntity}");
                Console.WriteLine($"    IdentificationDate: {row.IdentificationDate}");
                Console.WriteLine($"    ActionType: {row.ActionType}");
                Console.WriteLine($"    ActionDescription: {row.ActionDescription}");
                Console.WriteLine($"    AssignedTo: {row.AssignedTo}");
                Console.WriteLine($"    DueDate: {row.DueDate}");
                Console.WriteLine($"    CompletedDate: {row.CompletedDate}");
                Console.WriteLine($"    DaysOverDue: {daysOverdue}");
                Console.WriteLine("}");
                Console.WriteLine();
            }
        }

        static async Task FormExamples()
        {
            Console.WriteLine("================================================================================");

            var date = DateTime.UtcNow.AddMonths(-1);

            var queryString = $"updatedAfter={date}"; // createdAfter or updatedAfter should be used to target new additions or changes since the last time this request was made to avoid returning unnecessary results

            var capaFormsPastMonthList = await CAPAFormService.FetchCAPAFormListAsync(queryString);

            Console.WriteLine($"CAPA forms updated after: {date}");

            foreach (var capaForm in capaFormsPastMonthList)
            {
                Console.WriteLine($"  RowUID: {capaForm.RowUID} - FormNumber: {capaForm.FormNumber} - CreatedDtm: {capaForm.CreatedDtm}");
            }

            Console.WriteLine("================================================================================");

            var newRowUID = await FormExample.AddCAPAFormAsync();

            Console.WriteLine($"New CAPA Form - RowUID: {newRowUID}");

            Console.WriteLine("================================================================================");

            var newCapaForm = await CAPAFormService.FetchCAPAFormAsync(newRowUID);

            Console.WriteLine($"Fetched CAPA Form - RowUID: {newCapaForm.RowUID} - FormNumber: {newCapaForm.FormNumber} - CreatedDtm: {newCapaForm.CreatedDtm} - UpdatedDtm: {newCapaForm.UpdatedDtm} - DueDate: {newCapaForm.DueDate} - Findings: {newCapaForm.Findings}");

            Console.WriteLine("================================================================================");

            await FormExample.UpdateCAPAFormAsync(newRowUID);

            newCapaForm = await CAPAFormService.FetchCAPAFormAsync(newRowUID);

            Console.WriteLine($"Updated CAPA Form - RowUID: {newCapaForm.RowUID} - FormNumber: {newCapaForm.FormNumber} - CreatedDtm: {newCapaForm.CreatedDtm} - UpdatedDtm: {newCapaForm.UpdatedDtm} - DueDate: {newCapaForm.DueDate} - Findings: {newCapaForm.Findings}");

            Console.WriteLine("================================================================================");

            await CAPAFormService.DeleteCAPAFormAsync(newRowUID);

            Console.WriteLine($"Deleted CAPA Form - RowUID: {newRowUID}");

        }

        static async Task BusinessHierarchyExamples()
        {
            Console.WriteLine("================================================================================");

            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();

            Console.WriteLine("Business Entities:");

            foreach (var hierarchy in hierarchyList)
            {
                Console.WriteLine($"RowUID: {hierarchy.RowUID} - Title: {hierarchy.Title} - StartDate: {hierarchy.StartDate}");
            }

            Console.WriteLine("================================================================================");

            var newHierarchyUID = await BusinessHierarchyExample.AddBusinessHierarchyAsync();

            Console.WriteLine($"New Business Entity - RowUID: {newHierarchyUID}");

            Console.WriteLine("================================================================================");

            var newHierarchy = await BusinessHierarchyService.FetchHierarchyAsync(newHierarchyUID);

            Console.WriteLine($"Fetched Business Entity - RowUID: {newHierarchy.RowUID} - Title: {newHierarchy.Title} - StartDate: {newHierarchy.StartDate} - Comments: {newHierarchy.Comments}");

            Console.WriteLine("================================================================================");

            await BusinessHierarchyExample.UpdateBusinessHierarchyAsync(newHierarchyUID);

            newHierarchy = await BusinessHierarchyService.FetchHierarchyAsync(newHierarchyUID);

            Console.WriteLine($"Updated Business Entity - RowUID: {newHierarchy.RowUID} - Title: {newHierarchy.Title} - StartDate: {newHierarchy.StartDate} - Comments: {newHierarchy.Comments}");

            Console.WriteLine("================================================================================");

            await BusinessHierarchyService.DeleteHierarchyAsync(newHierarchyUID);

            Console.WriteLine($"Deleted Business Entity - RowUID: {newHierarchyUID}");

        }

        static async Task EmployerExamples()
        {
            Console.WriteLine("================================================================================");

            var newRowUID = await EmployerExample.AddEmployerAsync();

            Console.WriteLine($"New Employer - RowUID: {newRowUID}");
            
            Console.WriteLine("================================================================================");

            await EmployerService.DeleteEmployerAsync(newRowUID);

            Console.WriteLine($"Deleted Employer - RowUID: {newRowUID}");
        }

        static async Task PositionExamples()
        {
            Console.WriteLine("================================================================================");

            var newRowUID = await PositionExample.AddPositionAsync();

            Console.WriteLine($"New Position - RowUID: {newRowUID}");

            Console.WriteLine("================================================================================");

            await PositionService.DeletePositionAsync(newRowUID);

            Console.WriteLine($"Deleted Position - RowUID: {newRowUID}");
        }

        static async Task AttachmentExamples()
        {
            Console.WriteLine("================================================================================");

            // To test this uncomment the following lines. Be aware the attachments will not be deleted. There is no API route for delete.
            var newAttachmentUID = await AttachmentExample.AddAttachmentAsync();
            
            Console.WriteLine($"New Attachment - RowUID: {newAttachmentUID}");

            Console.WriteLine("================================================================================");

            var attachmentBytes = await AttachmentService.FetchAttachmentAsync(newAttachmentUID);

            var attachmentText = System.Text.Encoding.UTF8.GetString(attachmentBytes); // the attachment in this example is a text file

            Console.WriteLine($"Fetched Attachment - RowUID: {newAttachmentUID} - Content: {attachmentText}");
        }

    }
}
