using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    internal class ProfessorsRepository : IProfessorsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProfessorsRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _context = applicationDbContext;
            _mapper = mapper;
        }

        public IEnumerable<Professor> GetAll()
        {
            var professorsDb = _context.Professors.Include(prof => prof.Lectures).ToList();
            return _mapper.Map<IReadOnlyCollection<Professor>>(professorsDb);
        }

        public Professor Get(int id)
        {
            var professorDb = _context.Professors.Where(x => x.Id == id).Include(prof => prof.Lectures).FirstOrDefault();
            return _mapper.Map<Professor?>(professorDb);
        }

        public Professor GetByName(string name)
        {
            var professorDb = _context.Professors.Where(x => x.Name == name).Include(prof => prof.Lectures).FirstOrDefault();
            return _mapper.Map<Professor>(professorDb);
        }

        //public Professor? GetByLecture(string lectureName)
        //{
        //    var professorDb = _context.Professors.Where(x => x.Id == id).Include(prof => prof.Lectures).FirstOrDefault();
        //    return _mapper.Map<Professor?>(professorDb);
        //}

        public int Create(Professor professor)
        {
            var professorDb = _mapper.Map<ProfessorDb>(professor);
            professorDb.Lectures = new List<LectureDb>();
            var result = _context.Professors.Add(professorDb);
            _context.SaveChanges();
            return result.Entity.Id;
        }

        public int Edit(Professor professor)
        {
            if (_context.Professors.Find(professor.Id) is ProfessorDb professorInDb)
            {
                professorInDb.Name = professor.Name;
                professorInDb.Email = professor.Email;
                _context.Entry(professorInDb).State = EntityState.Modified;
                _context.SaveChanges();
                return professorInDb.Id;
            }
            else
            {
                return 0;
            }
        }

        public int Delete(int id)
        {
            if (_context.Professors.Find(id) is ProfessorDb professorToDelete)
            {
                _context.Entry(professorToDelete).State = EntityState.Deleted;
                _context.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}