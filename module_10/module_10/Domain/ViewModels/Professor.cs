using System.Collections.Generic;

namespace Domain
{
    public record Professor
    {
        public Professor()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<Lecture> Lectures { get; set; } = null!;
    }
}