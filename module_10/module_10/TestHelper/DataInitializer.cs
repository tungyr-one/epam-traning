using DataAccess;
using Domain;
using System;
using System.Collections.Generic;

namespace TestHelper
{
    public class DataInitializer
    {
        public static List<Lecture> GetAllLectures()
        {
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id=1,
                    Name="Physics",
                    Date=new DateTime(2021, 10, 1),
                    ProfessorName = "Albert Einstein",
                    StudentAttendances = new List<StudentAttendance>
                    {
                        new StudentAttendance
                        {
                            LectureId = 1,
                            StudentId = 1,
                            LectureName = "Physics",
                            StudentName = "Vasya Pupkin",
                            HomeworkMark = 3,
                            isPresent= false
                        }
                    }
                },
                new Lecture
                {
                    Id=2,
                    Name="Physics",
                    Date=new DateTime(2021, 10, 3),
                    ProfessorName = "Albert Einstein"
                },
                new Lecture
                {
                    Id=3,
                    Name="Physics",
                    Date=new DateTime(2021, 10, 6),
                    ProfessorName = "Albert Einstein"
                },
                new Lecture
                {
                    Id=4,
                    Name="Chemistry",
                    Date=new DateTime(2021, 10, 9),
                    ProfessorName = "Dmitrii Mendeleev"
                },
            };
            return lectures;
        }

        public static List<Professor> GetAllProfessors()
        {
            var professors = new List<Professor>
            {
                new Professor
                {
                    Id = 1,
                    Name = "Albert Einstein",
                    Email = "a.einstein@university.com",
                },
                new Professor
                {
                    Id = 2,
                    Name = "Dmitrii Mendeleev",
                    Email = "d.mendeleev@university.com",
                },

                new Professor
                {
                    Id = 3,
                    Name = "Aleksandr Popov",
                    Email = "a.popov@university.com",
                },

                new Professor
                {
                    Id = 4,
                    Name = "Elon Musk",
                    Email = "elon.musk@tesla.com",
                },
            };
            return professors;
        }

        public static List<StudentAttendance> GetAllStudentAttendances()
        {
            var professors = new List<StudentAttendance>
            {
                new StudentAttendance
                {
                    LectureId = 1,
                    StudentId = 4,
                    LectureName = "Physics",
                    StudentName = "Petya Nakhimov",
                    HomeworkMark = 5,
                    isPresent = true,
                },
                new StudentAttendance
                {
                    LectureId = 1,
                    StudentId = 2,
                    LectureName = "Physics",
                    StudentName = "Ivan Ivanov",
                    HomeworkMark = 1,
                    isPresent = true,
                },

                new StudentAttendance
                {
                    LectureId = 1,
                    StudentId = 1,
                    LectureName = "Physics",
                    StudentName = "Vasya Pupkin",
                    HomeworkMark = 4,
                    isPresent = true,
                },

                new StudentAttendance
                {
                    LectureId = 1,
                    StudentId = 3,
                    LectureName = "Physics",
                    StudentName = "Sergei Sidorov",
                    HomeworkMark = 0,
                    isPresent = false,
                },
            };
            return professors;
        }

        public static List<Student> GetAllStudents()
        {
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Vasya Pupkin",
                    Email = "v.pupkin@university.com",
                    Phone = "89543876435",
                    Age = 18,
                },
                new Student
                {
                    Id = 2,
                    Name = "Ivan Ivanov",
                    Email = "i.ivanov@university.com",
                    Phone = "8954388925378128676435",
                    Age = 19,
                },
                new Student
                {
                    Id = 3,
                    Name = "Sergei Sidorov",
                    Email = "s.sidorov@university.com",
                    Phone = "89115732138",
                    Age = 20,
                },
                new Student
                {
                    Id = 4,
                    Name = "Petya Nakhimov",
                    Email = "p.n@university.com",
                    Phone = "89278437834",
                    Age = 23,
                },
            };
            return students;
        }
    }
}