using Domain.Domain.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("/api/report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportsService _reportService;
        private readonly ILogger _logger;

        public ReportController(IReportsService reportService, ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        [HttpGet("lecture/{lectureName}")]
        public ActionResult<string> GetLectureReport(string lectureName)
        {
            _logger.LogInformation("Got query for lecture report composing");
            var report = _reportService.GenerateLectureAttendanceReport(lectureName);
            _reportService.LectureAttendanceCheck(lectureName);
            _reportService.LectureStudyProgressCheck(lectureName);
            return report;
        }

        [HttpGet("student/{studentName}")]
        public ActionResult<string> GetStudentReport(string studentName)
        {
            _logger.LogInformation("Got query for student report composing");
            var report = _reportService.GenerateStudentAttendanceReport(studentName);
            _reportService.StudentAttendanceCheck(studentName);
            _reportService.StudentStudyProgressCheck(studentName);
            return report;
        }
    }
}