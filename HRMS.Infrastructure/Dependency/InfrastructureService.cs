using System.Reflection;
using System.Text;
using API.Repositories;
using CloudinaryDotNet;
using FluentValidation;
using FluentValidation.AspNetCore;
using HRMS.Application.Interfaces.Contract;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Application.Interfaces.Repositories.Base;
using HRMS.Application.Utility.Handler;
using HRMS.Infrastructure.Implementation.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HRMS.Infrastructure.Implementation.Repository.Base;
using HRMS.Infrastructure.Implementation.Services;
using HRMS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TokenHandler = HRMS.Application.Utility.Handler.TokenHandler;

namespace HRMS.Infrastructure.Dependency;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("HRMS.Infrastructure")));

        services.AddTransient<IGenericRepository, GenericRepository>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeJobRepository, EmployeeJobRepository>();
        services.AddScoped<IEmployeeProjectRepository, EmployeeProjectRepository>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<IInterviewRepository, InterviewRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IPlacementRepository, PlacementRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITokenHandler, TokenHandler>();

        services.AddScoped<AccountService>();
        services.AddScoped<AccountRoleService>();
        services.AddScoped<DepartmentService>();
        services.AddScoped<DashboardService>();
        services.AddScoped<EmployeeService>();
        services.AddScoped<EmployeeJobService>();
        services.AddScoped<EmployeeProjectService>();
        services.AddScoped<GradeService>();
        services.AddScoped<InterviewService>();
        services.AddScoped<JobService>();
        services.AddScoped<PlacementService>();
        services.AddScoped<ProfileService>();
        services.AddScoped<ProjectService>();
        services.AddScoped<RoleService>();

        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        var smtpServer = configuration["EmailService:SmtpServer"]!;
        var smtpPort = int.Parse(configuration["EmailService:SmtpPort"]!);
        var fromEmailAddress = configuration["EmailService:FromEmailAddress"]!;
        var fromEmailPassword = configuration["EmailService:FromEmailPassword"]!;
        
        services.AddTransient<IEmailHandler, EmailHandler>(_ => new EmailHandler(
            smtpServer, smtpPort, fromEmailAddress, fromEmailPassword));

        var issuer = configuration["JWTService:Issuer"];
        var secretKey = configuration["JWTService:Key"];
        var audience = configuration["JWTService:Audience"];

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; 
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        const string cloudName = "dmtlqo6b2";
        const string apiKey = "348352326853286";
        const string apiSecret = "x4oMVQ6SkEFksHnPdSbSB7KLxqo";
        
        services.AddSingleton(new Cloudinary(new Account(cloudName, apiKey, apiSecret)));
        
        return services;
    }
    
}