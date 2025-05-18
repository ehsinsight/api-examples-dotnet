using DotNetSamples.Models;
using DotNetSamples.Services;

namespace DotNetSamples.Examples
{
    public class ReportExecuteExample
    {
        /// <summary>
        /// Example to illustrate executing a report with default paramters.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<CAPARegisterCAPA>> GetCAPARegisterReportWithDefaultParametersAsync()
        {
            // Get report rows for the CAPA Register report with default parameters
            return await CAPARegisterReportService.ExecuteCAPAReportAsync(null);
        }

        /// <summary>
        /// Example to illustrate executing a report with explicit parameters.
        /// </summary>
        /// <returns>Overdue CAPA Register Report rows.</returns>
        public static async Task<List<CAPARegisterCAPA>> GetCAPARegisterReportPastYearScrantonAsync()
        {
            // Fetch Business Hierarchies list to build the filter.
            var hierarchyList = await BusinessHierarchyService.FetchHierarchyListAsync();

            // For this example we will just grab the business hierarchy for Scranton.
            var hierarchyRowUID = hierarchyList.First(x => x.Title == "Scranton").RowUID;

            // Build date filter for past year. We do this specifically to limit records returned.
            var currDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var startDate = DateTime.UtcNow.AddMonths(-12).ToString("yyyy-MM-dd");

            // Get report rows for the CAPA Register report with a date range between today and 1 year ago with hierarchy filter for Scranton.
            return await CAPARegisterReportService.ExecuteCAPAReportAsync($"DateRangeFilter_Start={startDate}&DateRangeFilter_End={currDate}&HierarchyFilter={hierarchyRowUID}");

        }
    
    }
}
