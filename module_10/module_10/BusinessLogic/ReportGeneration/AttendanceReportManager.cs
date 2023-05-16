using LecturesApp._BusinessLogic.ReportGeneration;

namespace BusinessLogic
{
    internal class AttendanceReportManager : IAttendanceReportManager
    {
        public AttendanceReportManager()
        { }

        private IReportGenerator _reportGenerator = null!;

        public void SetGenerator(IReportGenerator generator)
        {
            _reportGenerator = generator;
        }

        public string CreateReport(object reportData)
        {
            return _reportGenerator.GenerateAttendanceReport(reportData);
        }
    }
}