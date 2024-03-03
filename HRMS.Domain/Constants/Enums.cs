namespace HRMS.Domain.Constants;

public enum Gender
{
    Male = 1,
    Female = 2
}

public enum Grade
{
    A = 1,
    B = 2,
    C = 3
}

public enum ApprovalStatus
{
    Pending = 0,
    Accepted = 1,
    Rejected = -1
}

public enum ActionStatus
{
    Idle = 0,
    Working = 1
}

public enum AttendanceModes
{
    Online = 1,
    Physical = 2,
    Hybrid = 3
}

public enum Roles
{
    Admin = 1,
    Manager = 2,
    Trainer = 3,
    Employee = 4,
    HR = 5
}

public enum TaskStatus
{
    ToDo = 1,
    InProgress = 2,
    InTesting = 3,
    Done = 4
}

public enum TaskTypes
{
    Task = 1,
    Bug = 2,
    Other = 3
}