namespace BusinessLogic
{
    public interface IReportGenerator
    {
        string GenerateAttendanceReport(object reportData);
    }
}