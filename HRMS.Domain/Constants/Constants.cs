namespace HRMS.Domain.Constants;

public abstract class Constants
{
    public abstract class Roles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Trainer = "Trainer";
        public const string Employee = "Employee";
        public const string HR = "HR";
    }

    public abstract class Passwords
    {
        public const string Password = "radi0V!oleta";
    }
    
    public abstract class Entity
    {
        public const string System = "System";
        public const string Admin = "Admin";
        public const string Employee = "Employee";
    }
    
    public abstract class Appointment
    {
        public const string Booked = "Booked";
        public const string Completed = "Completed";
        public const string Pending = "Pending";
        public const string Approved = "Approved";
    }
    
    public abstract class FilePath
    {
        public static string UsersImagesPath => @"wwwroot\images\users\";
    }
}