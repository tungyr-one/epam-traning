using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestHelper")]
[assembly: InternalsVisibleTo("RestApi.IntegrationTests")]
[assembly: InternalsVisibleTo("DataAccess.IntegrationTests")]
namespace DataAccess
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            return services
                .AddAutoMapper(typeof(MapperProfile))
                .AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString))
                .AddScoped<IStudentsRepository, StudentsRepository>()
                .AddScoped<IProfessorsRepository, ProfessorsRepository>()
                .AddScoped<IStudentAttendancesRepository, StudentAttendanceRepository>()
                .AddScoped<ILecturesRepository, LecturesRepository>();
        }
    }
}