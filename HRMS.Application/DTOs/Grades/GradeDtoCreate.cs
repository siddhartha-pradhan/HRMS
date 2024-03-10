using HRMS.Domain.Constants;

namespace HRMS.Application.DTOs.Grades;

public class GradeDtoCreate
{
    public Grade GradeLevel { get; set; }
    public int FirstScoreSegment { get; set; }
    public int SecondScoreSegment { get; set; }
    public int ThirdScoreSegment { get; set; }
    public int FourthScoreSegment { get; set; }
    public double TotalScore { get; set; }

    public static implicit operator Domain.Entities.Grade(GradeDtoCreate gradeDtoCreate)
    {
        return new Domain.Entities.Grade
        {
            GradeLevel = gradeDtoCreate.GradeLevel,
            FirstScoreSegment = gradeDtoCreate.FirstScoreSegment,
            SecondScoreSegment = gradeDtoCreate.SecondScoreSegment,
            ThirdScoreSegment = gradeDtoCreate.ThirdScoreSegment,
            FourthScoreSegment = gradeDtoCreate.FourthScoreSegment,
            TotalScore = gradeDtoCreate.TotalScore,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public static explicit operator GradeDtoCreate(Domain.Entities.Grade grade)
    {
        return new GradeDtoCreate
        {
            GradeLevel = grade.GradeLevel,
            FirstScoreSegment = grade.FirstScoreSegment,
            SecondScoreSegment = grade.SecondScoreSegment,
            ThirdScoreSegment = grade.ThirdScoreSegment,
            FourthScoreSegment = grade.FourthScoreSegment,
            TotalScore = grade.TotalScore
        };
    }
}
