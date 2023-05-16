using BusinessLogic.Exceptions;
using Domain;
using LecturesApp.BusinessLogic.Exceptions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    internal class ProfessorsService : IProfessorsService
    {
        private readonly IProfessorsRepository _professorsRepository;

        private readonly ILogger<ProfessorsService> _logger;

        public ProfessorsService(IProfessorsRepository professorsRepository, ILogger<ProfessorsService> logger)
        {
            _professorsRepository = professorsRepository;
            _logger = logger;
        }

        public Professor? Get(int id)
        {
            if (_professorsRepository.Get(id) is Professor professor)
            {
                return _professorsRepository.Get(id);
            }
            else
            {
                _logger.LogError($"Unable get professor with id {id}");
                throw new ProfessorNotFoundException("No such professor in DB.");
            }
        }

        public IReadOnlyCollection<Professor> GetAll()
        {
            return _professorsRepository.GetAll().ToArray();
        }

        public int Create(Professor professor)
        {
            if (_professorsRepository.Create(professor) is int professorId && professorId > 0)
            {
                return professorId;
            }
            else
            {
                _logger.LogError($"Unable get professor with name {professor.Name}");
                throw new ProfessorException("Unable to create new professor entry.");
            }
        }

        public int Edit(Professor professor)
        {
            if (_professorsRepository.Edit(professor) is int professorId && professorId > 0)
            {
                return professorId;
            }
            else
            {
                _logger.LogError($"Unable update professor with name {professor.Name}");
                throw new ProfessorNotFoundException("No such professor in DB.");
            }
        }

        public int Delete(int id)
        {
            if (_professorsRepository.Delete(id) is int status && status > 0)
            {
                return status;
            }
            else
            {
                _logger.LogError($"Unable delete professor with id {id}");
                throw new ProfessorNotFoundException("No such professor in DB.");
            }
        }
    }
}