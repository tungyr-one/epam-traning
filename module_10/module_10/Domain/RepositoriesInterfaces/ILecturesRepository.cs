using System.Collections.Generic;

namespace Domain
{
    public interface ILecturesRepository
    {
        int Delete(int id);

        int Edit(Lecture lecture);

        Lecture? Get(int id);

        Lecture? GetByName(string name);

        IEnumerable<Lecture> GetAllIncludedEntities(string name);

        IEnumerable<Lecture> GetAll();

        int Create(Lecture lecture);

        Professor? GetLectureProfessor(string lectureName);
    }
}