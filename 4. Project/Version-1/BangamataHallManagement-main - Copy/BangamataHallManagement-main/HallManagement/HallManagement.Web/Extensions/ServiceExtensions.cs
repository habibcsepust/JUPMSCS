using Microsoft.Extensions.DependencyInjection;
using HallManagement.Service.Interfaces;
using HallManagement.Service;
using HallManagement.Core.Interfaces;
using HallManagement.Core.Repositories;
using HallManagement.SmsServiceReference;

namespace Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IReportRepository, ReportRepository>();
            
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuRoleRepository, MenuRoleRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomSeatRepository, RoomSeatRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IEstablishmentChargeRepository, EstablishmentChargeRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<INationalityRepository, NationalityRepository>();
            services.AddScoped<IReligionRepository, ReligionRepository>();
            services.AddScoped<IBloodGroupRepository, BloodGroupRepository>();
            services.AddScoped<IBatchRepository, BatchRepository>();
            services.AddScoped<IRoomSeatHistoryRepository, RoomSeatHistoryRepository>();
            services.AddScoped<IPasswordResetHistoryRepository, PasswordResetHistoryRepository>();
        }

        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IReportService, ReportService>();
            
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserCredentialService, UserCredentialService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenuRoleService, MenuRoleService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomSeatService, RoomSeatService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IEstablishmentChargeService, EstablishmentChargeService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<INationalityService, NationalityService>();
            services.AddScoped<IReligionService, ReligionService>();
            services.AddScoped<IBloodGroupService, BloodGroupService>();
            services.AddScoped<IBatchService, BatchService>();
            services.AddScoped<IRoomSeatHistoryService, RoomSeatHistoryService>();
            services.AddScoped<IPasswordResetHistoryService, PasswordResetHistoryService>();
        }
    }
}
