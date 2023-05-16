using BusinessLogic;

namespace LecturesApp._BusinessLogic.ReportGeneration
{
    public interface IAttendanceReportManager
    {
        public string CreateReport(object reportData);

        public void SetGenerator(IReportGenerator generator);
    }
}