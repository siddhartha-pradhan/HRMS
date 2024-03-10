using HRMS.Domain.Constants;

namespace HRMS.Application.DTOs.Grades;

public class GradeDtoGet
{
    public Guid Id { get; set; }
    public Grade GradeLevel { get; set; }
    public int FirstScoreSegment { get; set; }
    public int SecondScoreSegment { get; set; }
    public int ThirdScoreSegment { get; set; }
    public int FourthScoreSegment { get; set; }
    public double TotalScore { get; set; }

    public static implicit operator HRMS.Domain.Entities.Grade(GradeDtoGet gradeDtoGet)
    {
        return new HRMS.Domain.Entities.Grade
        {
            Id = gradeDtoGet.Id,
            GradeLevel = gradeDtoGet.GradeLevel,
            FirstScoreSegment = gradeDtoGet.FirstScoreSegment,
            SecondScoreSegment = gradeDtoGet.SecondScoreSegment,
            ThirdScoreSegment = gradeDtoGet.ThirdScoreSegment,
            FourthScoreSegment = gradeDtoGet.FourthScoreSegment,
            TotalScore = gradeDtoGet.TotalScore
        };
    }

    public static explicit operator GradeDtoGet(HRMS.Domain.Entities.Grade grade)
    {
        return new GradeDtoGet
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
