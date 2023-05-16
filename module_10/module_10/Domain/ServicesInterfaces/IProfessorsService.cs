using System.Collections.Generic;

namespace Domain
{
    public interface IProfessorsService
    {
        Professor? Get(int id);

        IReadOnlyCollection<Professor> GetAll();

        int Create(Professor professor);

        int Edit(Professor professor);

        int Delete(int id);
    }
}