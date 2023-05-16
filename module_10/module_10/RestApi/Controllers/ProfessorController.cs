using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("/api/professor")]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorsService _professorsService;

        public ProfessorController(IProfessorsService professorsService)
        {
            _professorsService = professorsService;
        }

        [HttpGet("{id}")]
        public ActionResult<Professor> GetProfessor(int id)
        {
            return _professorsService.Get(id) switch
            {
                null => NotFound(),
                var professor => professor // implicit cast to AcitonResult
            };
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<Professor>> GetProfessors()
        {
            return _professorsService.GetAll().ToArray();
        }

        [HttpPost]
        public IActionResult AddProfessor(Professor professor)
        {
            var newProfessorId = _professorsService.Create(professor);
            return Ok($"api/professor/{newProfessorId}");
        }

        [HttpPut("{id}")]
        public ActionResult<string> UpdateProfessor(int id, Professor professor)
        {
            var professorId = _professorsService.Edit(professor with { Id = id });
            return Ok($"api/professor/{professorId}");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProfessor(int id)
        {
            _professorsService.Delete(id);
            return Ok();
        }
    }
}