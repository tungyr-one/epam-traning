using System.Collections.Generic;

namespace Domain
{
    public interface IProfessorsRepository
    {
        int Delete(int id);

        int Edit(Professor professor);

        Professor? Get(int id);

        public Professor? GetByName(string name);

        IEnumerable<Professor> GetAll();

        int Create(Professor professor);
    }
}