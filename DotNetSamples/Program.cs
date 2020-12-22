using DotNetSamples.Examples;
using DotNetSamples.Services;

namespace DotNetSamples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                #region UserContact Examples

                var userContacts = await UserContactService.FetchUserContactListAsync();
                if (userContacts.Count > 0)
                {
                    foreach (var userContact in userContacts)
                    {
                        Console.WriteLine($"RowUID: {userContact.RowUID} - FullName: {userContact.FullName} - BusinessEntity: {userContact.BusinessEntity}");
                    }
                }
                else
                {
                    Console.WriteLine("There was an error fetching the list of users");
                }

                var newUserUID = await UserContactExample.AddUserAsync();
                Console.WriteLine($"New user RowUID: {newUserUID}");
                var newUser = await UserContactService.FetchUserContactAsync(newUserUID);
                Console.WriteLine($"RowUID: {newUser.RowUID} - FullName: {newUser.FullName} - BusinessEntity: {newUser.BusinessEntity} - Position: {newUser.Position} - Employer: {newUser.Employer}");

                await UserContactExample.UpdateUserAsync();
                newUser = await UserContactService.FetchUserContactAsync(newUserUID);
                Console.WriteLine($"UPDATED USER - RowUID: {newUser.RowUID} - FullName: {newUser.FullName} - BusinessEntity: {newUser.BusinessEntity} - Position: {newUser.Position} - Employer: {newUser.Employer}");

                await UserContactService.DeleteUserContactAsync(newUserUID);
                #endregion

                #region Report Examples
                var capaRegisterOverdueRows = await ReportExecuteExample.GetPastYearOpenCAPARegisterReportAsync();
                Console.WriteLine("CAPA Register report rows that are open and within the past year:");
                if (capaRegisterOverdueRows != null)
                {
                    foreach (var row in capaRegisterOverdueRows)
                    {
                        Console.WriteLine("{");
                        Console.WriteLine($"    Form: {row?.Form}");
                        Console.WriteLine($"    BusinessEntity: {row?.BusinessEntity}");
                        Console.WriteLine($"    IdentificationDate: {row?.IdentificationDate}");
                        Console.WriteLine($"    ActionType: {row?.ActionType}");
                        Console.WriteLine($"    Source: {row?.Source}");
                        Console.WriteLine($"    RecommendedActionDescription: {row?.RecommendedActionDescription}");
                        Console.WriteLine($"    AssignedTo: {row?.AssignedTo}");
                        Console.WriteLine($"    CurrentDueDate: {row?.CurrentDueDate}");
                        Console.WriteLine($"    DaysOverdue: {row?.DaysOverdue}");
                        Console.WriteLine($"    CompletedDate: {row?.CompletedDate}");
                        Console.WriteLine($"    CompletionDays: {row?.CompletionDays}");
                        Console.WriteLine($"    Workflow: {row?.Workflow}");
                        Console.WriteLine($"    Status: {row?.Status}");
                        Console.WriteLine("}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("There was an error fetching the CAPA Register Report rows.");
                }

                var capaRegisterCapaNqRows = await ReportExecuteExample.GetCAPARegisterReportWithNamedQueryAsync();
                Console.WriteLine("CAPA Register report with named query CAPA rows:");
                if (capaRegisterCapaNqRows != null)
                {
                    foreach (var row in capaRegisterCapaNqRows)
                    {
                        Console.WriteLine("{");
                        Console.WriteLine($"    Form: {row?.Form}");
                        Console.WriteLine($"    BusinessEntity: {row?.BusinessEntity}");
                        Console.WriteLine($"    IdentificationDate: {row?.IdentificationDate}");
                        Console.WriteLine($"    ActionType: {row?.ActionType}");
                        Console.WriteLine($"    Source: {row?.Source}");
                        Console.WriteLine($"    RecommendedActionDescription: {row?.RecommendedActionDescription}");
                        Console.WriteLine($"    AssignedTo: {row?.AssignedTo}");
                        Console.WriteLine($"    CurrentDueDate: {row?.CurrentDueDate}");
                        Console.WriteLine($"    DaysOverdue: {row?.DaysOverdue}");
                        Console.WriteLine($"    CompletedDate: {row?.CompletedDate}");
                        Console.WriteLine($"    CompletionDays: {row?.CompletionDays}");
                        Console.WriteLine($"    Workflow: {row?.Workflow}");
                        Console.WriteLine($"    Status: {row?.Status}");
                        Console.WriteLine("}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("There was an error fetching the CAPA Register Report with named query CAPA rows.");
                }
                #endregion

                #region Form Examples
                var date = DateTime.UtcNow.AddMonths(-1);
                var queryString = $"createdAfter={date}"; // createdAfter or UpdatedAfter should be used to target new additions or changes since the last time this request was made to avoid returning unnecessary results
                var capaFormsPastMonthList = await CAPAFormService.FetchCAPAFormListAsync(queryString);
                if (capaFormsPastMonthList.Count > 0)
                {
                    foreach (var capaForm in capaFormsPastMonthList)
                    {
                        Console.WriteLine($"RowUID: {capaForm.RowUID} - FormNumber: {capaForm.FormNumber} - CreatedDtm: {capaForm?.CreatedDtm}");
                    }
                }
                else
                {
                    Console.WriteLine("There was an error fetching the list of business hierarchies.");
                }

                var newCapaFormUID = await FormExamples.AddCAPAFormAsync();
                Console.WriteLine($"New CAPA Form RowUID: {newCapaFormUID}");
                var newCapaForm = await CAPAFormService.FetchCAPAFormAsync(newCapaFormUID);
                Console.WriteLine($"RowUID: {newCapaForm.RowUID} - FormNumber: {newCapaForm.FormNumber} - CreatedDtm: {newCapaForm.CreatedDtm} - UpdatedDtm: {newCapaForm?.UpdatedDtm} - DueDate: {newCapaForm?.DueDate} - Findings: {newCapaForm?.Findings}");

                await FormExamples.UpdateCAPAFormAsync();
                newCapaForm = await CAPAFormService.FetchCAPAFormAsync(newCapaFormUID);
                Console.WriteLine($"UPDATED CAPA FORM - RowUID: {newCapaForm.RowUID} - FormNumber: {newCapaForm.FormNumber} - CreatedDtm: {newCapaForm.CreatedDtm} - UpdatedDtm: {newCapaForm?.UpdatedDtm} - DueDate: {newCapaForm?.DueDate} - Findings: {newCapaForm?.Findings}");

                await CAPAFormService.DeleteCAPAFormAsync(newCapaFormUID);
                #endregion

                #region Business Hierarchy Examples
                var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();
                if (hierarchyList.Count > 0)
                {
                    foreach (var hierarchy in hierarchyList)
                    {
                        Console.WriteLine($"RowUID: {hierarchy.RowUID} - Title: {hierarchy.Title} - StartDate: {hierarchy?.StartDate}");
                    }
                }
                else
                {
                    Console.WriteLine("There was an error fetching the list of business hierarchies.");
                }

                var newHierarchyUID = await BusinessHierarchyExamples.AddBusinessHierarchyAsync();
                Console.WriteLine($"New business hierarchy RowUID: {newHierarchyUID}");
                var newHierarchy = await BusinessHierarchyService.FetchHierarchyAsync(newHierarchyUID);
                Console.WriteLine($"RowUID: {newHierarchy.RowUID} - Title: {newHierarchy.Title} - StartDate: {newHierarchy?.StartDate} - Comments: {newHierarchy?.Comments}");

                await BusinessHierarchyExamples.UpdateBusinessHierarchyAsync();
                newHierarchy = await BusinessHierarchyService.FetchHierarchyAsync(newHierarchyUID);
                Console.WriteLine($"UPDATED BUSINESS HIERARCHY - RowUID: {newHierarchy.RowUID} - Title: {newHierarchy.Title} - StartDate: {newHierarchy?.StartDate} - Comments: {newHierarchy?.Comments}");

                await BusinessHierarchyService.DeleteHierarchyAsync(newHierarchyUID);
                #endregion

                #region Employer Examples
                var newEmployerUID = await EmployerExample.AddEmployerAsync();
                Console.WriteLine($"New employer RowUID: {newEmployerUID}");

                await EmployerService.DeleteEmployerAsync(newEmployerUID);
                #endregion

                #region Position Examples
                var newPositionUID = await PositionExample.AddPositionAsync();
                Console.WriteLine($"New position RowUID: {newPositionUID}");

                await PositionService.DeletePositionAsync(newPositionUID);
                #endregion

                #region Attachment Examples
                // To test this uncomment the following lines. Be aware the attachments will not be deleted. There is no API route for delete.
                // var newAttachmentUID = await AttachmentExamples.AddAttachmentAsync();
                // Console.WriteLine($"New attachment RowUID: {newAttachmentUID}");
                // var attachmentBytes = await AttachmentService.FetchAttachmentAsync(newAttachmentUID);
                // var attachmentText = System.Text.Encoding.UTF8.GetString(attachmentBytes); // the attachment in this example is a text file
                // Console.WriteLine($"RowUID: {newAttachmentUID} - Content: {attachmentText}");
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
