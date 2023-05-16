using System;
using System.Collections.Generic;

namespace Students
{
    class Program
    {
        private static string[] subjects = new string[] { "Math", "Physics", "Biology", "History", "Chemistry", "Geography" };

        static void Main(string[] args)
        {
            var student1c1 = new Student("vasya.pupkin@epam.com");
            var student2c1 = new Student("sergey.ivanov@epam.com");
            var student3c1 = new Student("pavel.sidorov@epam.com");

            var student1c2 = new Student("Vasya", "Pupkin");
            var student2c2 = new Student("Sergey", "Ivanov");
            var student3c2 = new Student("Pavel", "Sidorov");

            Dictionary<Student, HashSet<string>> studentSubjectDict = new()
            {
                [student1c1] = SetStudentSubjects(),
                [student2c1] = SetStudentSubjects(),
                [student3c1] = SetStudentSubjects(),

                [student1c2] = SetStudentSubjects(),
                [student2c2] = SetStudentSubjects(),
                [student3c2] = SetStudentSubjects(),
            };

            Console.WriteLine("The quantity of students in dictionary: " + studentSubjectDict.Count);
        }

        static HashSet<string> SetStudentSubjects()
        {
            Random rnd = new();
            HashSet<string> studentSubjects = new();
            while (studentSubjects.Count < 3)
            {
                studentSubjects.Add(subjects[rnd.Next(0, subjects.Length)]);
            }
            return studentSubjects;
        }        
    }
}
