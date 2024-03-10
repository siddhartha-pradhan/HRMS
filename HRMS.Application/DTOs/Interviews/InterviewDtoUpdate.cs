using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Interviews;

public class InterviewDtoUpdate
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Link { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Description { get; set; }

    public static implicit operator Interview(InterviewDtoUpdate interviewDtoUpdate)
    {
        return new Interview
        {
            Id = interviewDtoUpdate.Id,
            Title = interviewDtoUpdate.Title,
            Link = interviewDtoUpdate.Link ?? "",
            InterviewDate = interviewDtoUpdate.InterviewDate,
            Description = interviewDtoUpdate.Description,
            LastModifiedAt = DateTime.UtcNow
        };
    }

    public static explicit operator InterviewDtoUpdate(Interview interview)
    {
        return new InterviewDtoUpdate
        {
            Id = interview.Id,
            Title = interview.Title,
            Link = interview.Link,
            InterviewDate = interview.InterviewDate,
            Description = interview.Description,
        };
    }
}
