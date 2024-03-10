using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Grades;

public class GradeDtoGenerateScore
{
    public int FirstScoreSegment { get; set; }
    public int SecondScoreSegment { get; set; }
    public int ThirdScoreSegment { get; set; }
    public int FourthScoreSegment { get; set; }
    
    public static implicit operator Grade(GradeDtoGenerateScore gradeDtoGenerateScore)
    {
        return new Grade
        {
            FirstScoreSegment = gradeDtoGenerateScore.FirstScoreSegment,
            SecondScoreSegment = gradeDtoGenerateScore.SecondScoreSegment,
            ThirdScoreSegment = gradeDtoGenerateScore.ThirdScoreSegment,
            FourthScoreSegment = gradeDtoGenerateScore.FourthScoreSegment,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public static explicit operator GradeDtoGenerateScore(Grade grade)
    {
        return new GradeDtoGenerateScore
        {
            FirstScoreSegment = grade.FirstScoreSegment,
            SecondScoreSegment = grade.SecondScoreSegment,
            ThirdScoreSegment = grade.ThirdScoreSegment,
            FourthScoreSegment = grade.FourthScoreSegment
        };
    }
}
