
using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Interviews;

public class InterviewDtoGet
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Link { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Description { get; set; }

    public static implicit operator Interview(InterviewDtoGet interviewDtoGet)
    {
        return new Interview
        {
            Id = interviewDtoGet.Id,
            Title = interviewDtoGet.Title,
            Link = interviewDtoGet.Link ?? "",
            InterviewDate = interviewDtoGet.InterviewDate,
            Description = interviewDtoGet.Description
        };
    }

    public static explicit operator InterviewDtoGet(Interview interview)
    {
        return new InterviewDtoGet
        {
            Id = interview.Id,
            Title = interview.Title,
            Link = interview.Link,
            InterviewDate = interview.InterviewDate,
            Description = interview.Description
        };
    }
}
