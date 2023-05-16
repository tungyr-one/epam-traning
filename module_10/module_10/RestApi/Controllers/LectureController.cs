using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("/api/lecture")]
    public class LectureController : ControllerBase
    {
        private readonly ILecturesService _lecturesService;
        private readonly ILogger _logger;

        public LectureController(ILecturesService lecturesService, ILogger<LectureController> logger)
        {
            this._lecturesService = lecturesService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<Lecture> GetLecture(int id)
        {
            return _lecturesService.Get(id) switch
            {
                null => NotFound("No such lecture."),
                var lecture => lecture
            };
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<Lecture>> GetLectures()
        {
            return _lecturesService.GetAll().ToArray();
        }

        [HttpPost]
        public IActionResult AddLecture(Lecture lecture)
        {
            var newLectureId = _lecturesService.Create(lecture);
            return Ok($"api/lecture/{newLectureId}");
        }

        [HttpPut("{id}")]
        public ActionResult<string> UpdateLecture(int id, Lecture lecture)
        {
            var lectureId = _lecturesService.Edit(lecture with { Id = id });
            return Ok($"api/lecture/{lectureId}");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteLecture(int id)
        {
            _lecturesService.Delete(id);
            return Ok();
        }
    }
}