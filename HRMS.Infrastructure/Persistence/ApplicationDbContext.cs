using System.Reflection;
using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = HRMS.Domain.Entities.Task;
using RoleModel = HRMS.Domain.Entities.Role;

namespace HRMS.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    // #region Identity Tables
    // public DbSet<User> Users { get; set; }
    //
    // public DbSet<UserRoles> UserRoles { get; set; }
    //
    // public DbSet<UserToken> UserToken { get; set; }
    //
    // public DbSet<UserLogin> UserLogin { get; set; }
    //
    // public DbSet<UserClaims> UserClaims { get; set; }
    //
    // public DbSet<RoleClaims> RoleClaims { get; set; }
    // #endregion

    #region Other Entities
    public DbSet<Account> Accounts { get; set; }
    
    public DbSet<AccountRole> AccountRoles { get; set; }
    
    public DbSet<Attendance> Attendances { get; set; }
    
    public DbSet<Department> Departments { get; set; }
    
    public DbSet<Employee> Employees { get; set; }
    
    public DbSet<EmployeeJob> EmployeeJobs { get; set; }
    
    public DbSet<EmployeeProject> EmployeeProject { get; set; }
    
    public DbSet<EmployeeTraining> EmployeeTraining { get; set; }
    
    public DbSet<Grade> Grades { get; set; }
    
    public DbSet<Interview> Interviews { get; set; }
    
    public DbSet<Job> Jobs { get; set; }
    
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    
    public DbSet<LeaveType> LeaveTypes { get; set; }
    
    public DbSet<Notification> Notifications { get; set; }
    
    public DbSet<Payroll> Payrolls { get; set; }

    public DbSet<Placement> Placements { get; set; }

    public DbSet<Profile> Profile { get; set; }
    
    public DbSet<Project> Projects { get; set; }
    
    public DbSet<Review> Reviews { get; set; }

    public DbSet<RoleModel> Roles { get; set; }
    
    public DbSet<Task> Tasks { get; set; }
    
    public DbSet<Training> Trainings { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        builder.Entity<Employee>()
            .HasOne(e => e.Grade)
            .WithMany()
            .HasForeignKey(e => e.GradeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        base.OnModelCreating(builder);

        // #region Identity Entities Configuration
        // builder.Entity<User>().ToTable("Users");
        // builder.Entity<Role>().ToTable("Roles");
        // builder.Entity<UserToken>().ToTable("Tokens");
        // builder.Entity<UserRoles>().ToTable("UserRoles");
        // builder.Entity<RoleClaims>().ToTable("RoleClaims");
        // builder.Entity<UserClaims>().ToTable("UserClaims");
        // builder.Entity<UserLogin>().ToTable("LoginAttempts");
        // #endregion
    }
}