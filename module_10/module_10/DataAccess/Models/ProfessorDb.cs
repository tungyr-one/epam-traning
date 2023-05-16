using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    internal record ProfessorDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public virtual ICollection<LectureDb> Lectures { get; set; }
    }
}