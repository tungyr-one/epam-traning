using AutoMapper;
using DataAccess.Models;
using Domain;

namespace DataAccess
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<StudentDb, Student>().ReverseMap();

            //CreateMap<Lecture, LectureDb>()
            //    .ForMember(lectDb => lectDb.Name, opt => opt.MapFrom(lect => lect.ProfessorName.Split()[0]))
            //    .ForMember(lectDb => lectDb.Name, opt => opt.MapFrom(lect => lect.ProfessorName.Split()[1]));

            CreateMap<Lecture, LectureDb>()
                .ForMember(lectDb => lectDb.Professor, opt => opt.Ignore());

            CreateMap<LectureDb, Lecture>().
            ForMember(lect => lect.ProfessorName, opt => opt.MapFrom(lectDb => lectDb.Professor.Name));

            CreateMap<ProfessorDb, Professor>().ReverseMap();
            CreateMap<StudentAttendanceDb, StudentAttendance>().ReverseMap();
        }
    }
}