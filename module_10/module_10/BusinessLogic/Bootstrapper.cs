using BusinessLogic.BusinessLogic.Notifier;
using Domain;
using Domain.Domain.ServicesInterfaces;
using LecturesApp._BusinessLogic.ReportGeneration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BusinessLogic.Tests")]

namespace BusinessLogic
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            return services
                .AddScoped<IStudentsService, StudentsService>()
                .AddScoped<IProfessorsService, ProfessorsService>()
                .AddScoped<IStudentAttendancesService, StudentAttendancesService>()
                .AddScoped<ILecturesService, LecturesService>()
                .AddScoped<IReportsService, ReportsService>()
                .AddScoped<IReportGenerator, JsonReportGenerator>()
                .AddScoped<IAttendanceReportManager, AttendanceReportManager>()
                .AddScoped<IAttendanceAnalyzer, AttendanceAnalyzer>()
                .AddScoped<IStudyProgressAnalyzer, StudyProgressAnalyzer>()
                .AddScoped<INotifyManager, NotifyManager>();
        }
    }
}