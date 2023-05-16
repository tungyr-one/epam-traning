using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("/api/student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentsService _studentsService;
        private readonly ILogger _logger;

        public StudentController(IStudentsService studentsService, ILogger<StudentController> logger)
        {
            _studentsService = studentsService;
            _logger = logger;
            _logger.LogInformation("Created StudentController logger.");
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            return _studentsService.Get(id) switch
            {
                null => NotFound(),
                var student => student // implicit cast to AcitonResult
            };
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<Student>> GetStudents()
        {
            _logger.LogInformation("Get all students.");
            return _studentsService.GetAll().ToArray();
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            var newStudentId = _studentsService.Create(student);
            return Ok($"api/student/{newStudentId}");
        }

        [HttpPut("{id}")]
        public ActionResult<string> UpdateStudent(int id, Student student)
        {
            var studentId = _studentsService.Edit(student with { Id = id });
            return Ok($"api/student/{studentId}");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(int id)
        {
            _studentsService.Delete(id);
            return Ok();
        }
    }
}