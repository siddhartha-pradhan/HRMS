using HRMS.Application.DTOs.Grades;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Utility.Handler;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Services;

public class GradeService
{
    private readonly ApplicationDbContext _context;
    
    private IGradeRepository _gradeRepository;

    public GradeService(IGradeRepository gradeRepository, ApplicationDbContext context)
    {
        _gradeRepository = gradeRepository;
        _context = context;
    }

    public IEnumerable<GradeDtoGet> Get()
    {
        var grades = _gradeRepository.GetAll().ToList();
        
        return !grades.Any() ? Enumerable.Empty<GradeDtoGet>() : grades.Cast<GradeDtoGet>().ToList();
    }

    public GradeDtoGet? Get(Guid guid)
    {
        var grade = _gradeRepository.GetByGuid(guid);
        
        if (grade is null) return null!;

        return (GradeDtoGet)grade;
    }

    public GradeDtoCreate? Create(GradeDtoCreate gradeDtoCreate)
    {
        var gradeCreated = _gradeRepository.Create(gradeDtoCreate);
        
        if (gradeCreated is null) return null!;

        return (GradeDtoCreate)gradeCreated;
    }

    public int Update(GradeDtoUpdate gradeDtoUpdate)
    {
        var grade = _gradeRepository.GetByGuid(gradeDtoUpdate.Id);
        
        if (grade is null) return -1;

        var gradeUpdated = _gradeRepository.Update(gradeDtoUpdate);
        
        return !gradeUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var grade = _gradeRepository.GetByGuid(guid);
        
        if (grade is null) return -1;

        var gradeDeleted = _gradeRepository.Delete(grade);
        
        return !gradeDeleted ? 0 : 1;
    }

    public GradeDtoCreate? CreateGenerateScore(GradeDtoGenerateScore gradeDtoGenerateScore)
    {
        using var transaction = _context.Database.BeginTransaction();
        
        try
        {
            var gradeData = new GradeDtoCreate
            {
                FirstScoreSegment = gradeDtoGenerateScore.FirstScoreSegment,
                SecondScoreSegment = gradeDtoGenerateScore.SecondScoreSegment,
                ThirdScoreSegment = gradeDtoGenerateScore.ThirdScoreSegment,
                FourthScoreSegment = gradeDtoGenerateScore.FourthScoreSegment,
            };
            
            gradeData.TotalScore = GenerateHandler.GenerateTotalScore(gradeData.FirstScoreSegment, gradeData.SecondScoreSegment, gradeData.ThirdScoreSegment, gradeData.FourthScoreSegment);
            
            gradeData.GradeLevel = GenerateHandler.GenerateGradeLevel(gradeData.FirstScoreSegment, gradeData.SecondScoreSegment, gradeData.ThirdScoreSegment, gradeData.FourthScoreSegment);

            var gradeCreated = _gradeRepository.Create(gradeData);
            
            if (gradeCreated is null) return null!;

            transaction.Commit();
            
            return (GradeDtoCreate)gradeCreated;
        }
        catch
        {
            transaction.Rollback();
            
            return null;
        }
    }

    public int UpdateGenerateScore(GradeDtoUpdateGenerateScore gradeDtoUpdateGenerateScore)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var grade = _gradeRepository.GetByGuid(gradeDtoUpdateGenerateScore.Id);
            
            if (grade is null) return -1;

            var gradeData = new GradeDtoUpdate
            {
                Id = gradeDtoUpdateGenerateScore.Id,
                FirstScoreSegment = gradeDtoUpdateGenerateScore.FirstScoreSegment,
                SecondScoreSegment = gradeDtoUpdateGenerateScore.SecondScoreSegment,
                ThirdScoreSegment = gradeDtoUpdateGenerateScore.ThirdScoreSegment,
                FourthScoreSegment = gradeDtoUpdateGenerateScore.FourthScoreSegment,
            };
            
            gradeData.TotalScore = GenerateHandler.GenerateTotalScore(gradeData.FirstScoreSegment, gradeData.SecondScoreSegment, gradeData.ThirdScoreSegment, gradeData.FourthScoreSegment);
            
            gradeData.GradeLevel = GenerateHandler.GenerateGradeLevel(gradeData.FirstScoreSegment, gradeData.SecondScoreSegment, gradeData.ThirdScoreSegment, gradeData.FourthScoreSegment);

            var gradeUpdated = _gradeRepository.Update(gradeData);
            
            if (!gradeUpdated) return 0;

            transaction.Commit();
            
            return 1;
        }
        catch
        {
            transaction.Rollback();
            return 0;
        }
    }
}
