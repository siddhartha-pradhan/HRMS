using System.Reflection;
using HRMS.Domain.Entities;
using HRMS.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = HRMS.Domain.Entities.Task;

namespace HRMS.Infrastructure.Persistence;

public sealed class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaims, UserRoles, UserLogin, RoleClaims, UserToken>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    #region Identity Tables
    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<UserRoles> UserRoles { get; set; }

    public DbSet<UserToken> UserToken { get; set; }
    
    public DbSet<UserLogin> UserLogin { get; set; }
    
    public DbSet<UserClaims> UserClaims { get; set; }
    
    public DbSet<RoleClaims> RoleClaims { get; set; }
    #endregion

    #region Other Entities
    public DbSet<Attendance> Attendances { get; set; }
    
    public DbSet<Department> Departments { get; set; }
    
    public DbSet<Employee> Employees { get; set; }
    
    public DbSet<JobTitle> JobTitles { get; set; }
    
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    
    public DbSet<LeaveType> LeaveTypes { get; set; }
    
    public DbSet<Notification> Notifications { get; set; }
    
    public DbSet<Payroll> Payrolls { get; set; }
    
    public DbSet<Review> Reviews { get; set; }
    
    public DbSet<Task> Tasks { get; set; }
    
    public DbSet<Training> Trainings { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(builder);

        #region Identity Entities Configuration
        builder.Entity<User>().ToTable("Users");
        builder.Entity<Role>().ToTable("Roles");
        builder.Entity<UserToken>().ToTable("Tokens");
        builder.Entity<UserRoles>().ToTable("UserRoles");
        builder.Entity<RoleClaims>().ToTable("RoleClaims");
        builder.Entity<UserClaims>().ToTable("UserClaims");
        builder.Entity<UserLogin>().ToTable("LoginAttempts");
        #endregion
    }
}