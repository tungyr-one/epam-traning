using System.Collections.Generic;

namespace Domain
{
    public interface ILecturesService
    {
        Lecture? Get(int id);

        IReadOnlyCollection<Lecture> GetAll();

        int Create(Lecture lecture);

        int Edit(Lecture lecture);

        int Delete(int id);
    }
}