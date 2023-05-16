using BusinessLogic.Exceptions;
using Domain;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BusinessLogic.Tests")]

namespace BusinessLogic
{
    internal class LecturesService : ILecturesService
    {
        private readonly ILecturesRepository _lecturesRepository;
        private readonly ILogger<LecturesService> _logger;

        public LecturesService(ILecturesRepository lecturesRepository, ILogger<LecturesService> logger)
        {
            _lecturesRepository = lecturesRepository;
            _logger = logger;
        }

        public Lecture? Get(int id)
        {            
            //return _lecturesRepository.Get(id);
            if (_lecturesRepository.Get(id) is Lecture lecture)
            {
                return lecture;
            }
            else
            {
                _logger.LogError($"Unable get lecture with id {id}");
                throw new LectureNotFoundException("No such lecture in DB.");
            }
        }

        public IReadOnlyCollection<Lecture> GetAll()
        {
            return _lecturesRepository.GetAll().ToArray();
        }

        public int Create(Lecture lecture)
        {
            if (_lecturesRepository.Create(lecture) is int lectureId && lectureId > 0)
            {
                return lectureId;
            }
            else
            {
                _logger.LogError($"Unable get lecture with name {lecture.Name}");
                throw new LectureException("Unable to create new lecture entry.");
            }
        }

        public int Edit(Lecture lecture)
        {
            if (_lecturesRepository.Edit(lecture) is int lectureId && lectureId > 0)
            {
                return lectureId;
            }
            else
            {
                _logger.LogError($"Unable update lecture with name {lecture.Name}");
                throw new LectureNotFoundException("No such lecture in DB.");
            }
        }

        public int Delete(int id)
        {
            if (_lecturesRepository.Delete(id) is int status && status > 0)
            {
                return status;
            }
            else
            {
                _logger.LogError($"Unable delete lecture with id {id}");
                throw new LectureNotFoundException("No such lecture in DB.");
            }
        }
    }
}