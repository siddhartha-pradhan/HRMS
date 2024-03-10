using HRMS.Domain.Constants;

namespace HRMS.Application.DTOs.Grades;

public class GradeDtoUpdate
{
    public Guid Id { get; set; }
    public Grade GradeLevel { get; set; }
    public int FirstScoreSegment { get; set; }
    public int SecondScoreSegment { get; set; }
    public int ThirdScoreSegment { get; set; }
    public int FourthScoreSegment { get; set; }
    public double TotalScore { get; set; }

    public static implicit operator HRMS.Domain.Entities.Grade(GradeDtoUpdate gradeDtoUpdate)
    {
        return new HRMS.Domain.Entities.Grade
        {
            Id = gradeDtoUpdate.Id,
            GradeLevel = gradeDtoUpdate.GradeLevel,
            FirstScoreSegment = gradeDtoUpdate.FirstScoreSegment,
            SecondScoreSegment = gradeDtoUpdate.SecondScoreSegment,
            ThirdScoreSegment = gradeDtoUpdate.ThirdScoreSegment,
            FourthScoreSegment = gradeDtoUpdate.FourthScoreSegment,
            TotalScore = gradeDtoUpdate.TotalScore,
            LastModifiedAt = DateTime.UtcNow
        };
    }
    
    public static explicit operator GradeDtoUpdate(HRMS.Domain.Entities.Grade grade)
    {
        return new GradeDtoUpdate
        {
            Id = grade.Id,
            GradeLevel = grade.GradeLevel,
            FirstScoreSegment = grade.FirstScoreSegment,
            SecondScoreSegment = grade.SecondScoreSegment,
            ThirdScoreSegment = grade.ThirdScoreSegment,
            FourthScoreSegment = grade.FourthScoreSegment,
            TotalScore = grade.TotalScore
        };
    }
}
