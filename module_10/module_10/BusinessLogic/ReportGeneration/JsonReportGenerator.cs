using System.Text.Json;

namespace BusinessLogic
{
    internal class JsonReportGenerator : IReportGenerator
    {
        public string GenerateAttendanceReport(object reportData)
        {
            string jsonData = JsonSerializer.Serialize(reportData);
            return jsonData;
        }
    }
}