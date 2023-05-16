using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    internal class DatabaseInitializer
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentAttendanceDb>()
                                    .HasKey(ls => new { ls.LectureId, ls.StudentId });

            modelBuilder.Entity<StudentAttendanceDb>()
                .HasOne(ls => ls.Lecture)
                .WithMany(l => l.StudentAttendances)
                .HasForeignKey(ls => ls.LectureId);

            modelBuilder.Entity<StudentAttendanceDb>()
                .HasOne(ls => ls.Student)
                .WithMany(l => l.StudentAttendances)
                .HasForeignKey(ls => ls.StudentId);

            modelBuilder.Entity<ProfessorDb>().HasData(
                new ProfessorDb[]
                {
                    new ProfessorDb { Id=1, Name="Albert Einstein", Email="a.einstein@university.com"},
                    new ProfessorDb { Id=2, Name="Dmitrii Mendeleev", Email="d.mendeleev@university.com"},
                    new ProfessorDb { Id=3, Name="Aleksandr Popov", Email="a.popov@university.com"},
                });

            modelBuilder.Entity<StudentDb>().HasData(
                new StudentDb[]
                {
                    new StudentDb { Id=1, Name="Vasya Pupkin", Email="v.pupkin@university.com", Phone="89543876435", Age=18 },
                    new StudentDb { Id=2, Name="Ivan Ivanov", Email="i.ivanov@university.com", Phone="89253781286", Age=19},
                    new StudentDb { Id=3, Name="Sergei Sidorov", Email="s.sidorov@university.com", Phone="89115732138", Age=20},
                });

            modelBuilder.Entity<LectureDb>().HasData(
                new LectureDb[]
                {
                    new LectureDb { Id=1, Name="Physics", Date= new System.DateTime(2021, 10, 1), ProfessorId = 1 },
                    new LectureDb { Id=2, Name="Chemistry", Date= new System.DateTime(2021, 10, 5), ProfessorId = 2 },
                    new LectureDb { Id=3, Name="History", Date= new System.DateTime(2021, 10, 10), ProfessorId = 3 },
                });

            modelBuilder.Entity<StudentAttendanceDb>().HasData(
                new StudentAttendanceDb[]
                {
                    new StudentAttendanceDb { LectureId=1, StudentId=1, IsPresent=true, HomeworkMark=5 },
                    new StudentAttendanceDb { LectureId=1, StudentId=2, IsPresent=true, HomeworkMark=0 },
                    new StudentAttendanceDb {  LectureId=1, StudentId=3, IsPresent=false, HomeworkMark=0 },

                    new StudentAttendanceDb { LectureId=2, StudentId=1, IsPresent=true, HomeworkMark=4 },
                    new StudentAttendanceDb { LectureId=2, StudentId=2, IsPresent=false, HomeworkMark=0 },
                    new StudentAttendanceDb {  LectureId=2, StudentId=3, IsPresent=true, HomeworkMark=3 },

                    new StudentAttendanceDb { LectureId=3, StudentId=1, IsPresent=false, HomeworkMark=0 },
                    new StudentAttendanceDb { LectureId=3, StudentId=2, IsPresent=true, HomeworkMark=4 },
                    new StudentAttendanceDb {  LectureId=3, StudentId=3, IsPresent=false, HomeworkMark=0 },
                });
        }
    }
}