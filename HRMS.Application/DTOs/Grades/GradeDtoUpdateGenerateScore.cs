using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Grades;

public class GradeDtoUpdateGenerateScore
{
    public Guid Id { get; set; }
    public int FirstScoreSegment { get; set; }
    public int SecondScoreSegment { get; set; }
    public int ThirdScoreSegment { get; set; }
    public int FourthScoreSegment { get; set; }
    
    public static implicit operator Grade(GradeDtoUpdateGenerateScore gradeDtoUpdateGenerateScore)
    {
        return new Grade
        {
            Id = gradeDtoUpdateGenerateScore.Id,
            FirstScoreSegment = gradeDtoUpdateGenerateScore.FirstScoreSegment,
            SecondScoreSegment = gradeDtoUpdateGenerateScore.SecondScoreSegment,
            ThirdScoreSegment = gradeDtoUpdateGenerateScore.ThirdScoreSegment,
            FourthScoreSegment = gradeDtoUpdateGenerateScore.FourthScoreSegment,
            LastModifiedAt = DateTime.UtcNow
        };
    }

    public static explicit operator GradeDtoUpdateGenerateScore(Grade grade)
    {
        return new GradeDtoUpdateGenerateScore
        {
            Id = grade.Id,
            FirstScoreSegment = grade.FirstScoreSegment,
            SecondScoreSegment = grade.SecondScoreSegment,
            ThirdScoreSegment = grade.ThirdScoreSegment,
            FourthScoreSegment = grade.FourthScoreSegment
        };
    }
}
