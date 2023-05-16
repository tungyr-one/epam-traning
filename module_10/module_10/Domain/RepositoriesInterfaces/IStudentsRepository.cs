using System.Collections.Generic;

namespace Domain
{
    public interface IStudentsRepository
    {
        int Delete(int id);

        int Edit(Student student);

        Student? Get(int id);

        IEnumerable<Student> GetAll();

        int Create(Student student);

        Student? GetByName(string name);
    }
}