using System.Collections.Generic;

namespace Domain
{
    public interface IStudentsService
    {
        Student? Get(int id);

        IReadOnlyCollection<Student> GetAll();

        int Create(Student student);

        int Edit(Student student);

        int Delete(int id);
    }
}