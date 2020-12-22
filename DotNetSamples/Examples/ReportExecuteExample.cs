using DotNetSamples.Models;
using DotNetSamples.Services;

namespace DotNetSamples.Examples
{
    public class ReportExecuteExample
    {
        /// <summary>
        /// Example to illustrate getting a report with parameters. This example gets the overdue CAPA Register report filtered by belonging to Scranton business entity and within the past 1 year.
        /// </summary>
        /// <returns>Overdue CAPA Register Report rows.</returns>
        public static async Task<List<CAPARegisterReport>> GetPastYearOpenCAPARegisterReportAsync()
        {
            // Fetch Business Hierarchies list to build the filter.
            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();

            // For this example we will just grab the business hierarchy for Scranton.
            var hierarchyRowUID = hierarchyList.First(x => x.Title == "Scranton").RowUID;

            // Build date filter for past year. We do this specifically to limit records returned.
            var currDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var startDate = DateTime.UtcNow.AddMonths(-12).ToString("yyyy-MM-dd");

            // Get report rows for the CAPA Register report with a date range between today and 1 year ago with hierarchy filter for Scranton.
            var reportRows = await CAPARegisterReportService.ExecuteReportAsync($"DateRangeFilter_Start={startDate}&DateRangeFilter_End={currDate}&HierarchyFilter={hierarchyRowUID}");

            var openCapaRegisters = reportRows.Where(x => x.CompletedDate == null).OrderBy(y => y.AssignedTo).ToList();
            return openCapaRegisters;
        }

        /// <summary>
        /// Example to illustrate getting a report with parameters and named query. This example gets the CAPA Register report with named query CAPA filtered by within the past 12 months.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<CAPARegisterReport>> GetCAPARegisterReportWithNamedQueryAsync()
        {
            // Build date filter for past year
            var currDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var startDate = DateTime.UtcNow.AddMonths(-12).ToString("yyyy-MM-dd");

            // Get report rows for the CAPA Register report with named query CAPA and with a date range between 01-01-2024 and 01-31-2024
            return await CAPARegisterReportService.ExecuteCAPANamedReportAsync($"DateRangeFilter_Start={startDate}&DateRangeFilter_End={currDate}");
        }
    }
}
