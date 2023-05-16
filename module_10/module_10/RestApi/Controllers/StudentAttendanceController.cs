using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("/api/studentAttendance")]
    public class StudentAttendanceController : ControllerBase
    {
        private readonly IStudentAttendancesService _studentAttendancesService;

        public StudentAttendanceController(IStudentAttendancesService studentAttendancesService)
        {
            _studentAttendancesService = studentAttendancesService;
        }

        [HttpGet("{lectureId}, {studentId}")]
        public ActionResult<StudentAttendance> GetStudentAttendance(int lectureId, int studentId)
        {
            return _studentAttendancesService.Get(lectureId, studentId) switch
            {
                null => NotFound(),
                var studentAttendance => studentAttendance
            };
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<StudentAttendance>> GetStudentAttendances()
        {
            return _studentAttendancesService.GetAll().ToArray();
        }

        [HttpPost]
        public IActionResult AddStudentAttendance(StudentAttendance studentAttendance)
        {
            var newStudentAttendanceCompositeId = _studentAttendancesService.Create(studentAttendance);
            return Ok($"api/studentAttendance/{newStudentAttendanceCompositeId}");
        }

        [HttpPut("{lectureId}, {studentId}")]
        public ActionResult<string> UpdateStudentAttendance(int lectureId, int studentId, StudentAttendance studentAttendance)
        {
            var studentAttendanceCompostieId = _studentAttendancesService.Edit(studentAttendance with { LectureId = lectureId, StudentId = studentId });
            return Ok($"api/studentAttendance/{studentAttendanceCompostieId}");
        }

        [HttpDelete("{lectureId}, {studentId}")]
        public ActionResult DeleteStudentAttendance(int lectureId, int studentId)
        {
            _studentAttendancesService.Delete(lectureId, studentId);
            return Ok();
        }
    }
}