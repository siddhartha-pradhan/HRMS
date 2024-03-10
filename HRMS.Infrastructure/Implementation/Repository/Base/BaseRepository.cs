using HRMS.Application.Interfaces.Repositories.Base;
using HRMS.Infrastructure.Persistence;

namespace HRMS.Infrastructure.Implementation.Repository.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _dbContext;

    protected BaseRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public ICollection<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>().ToList();
    }

    public TEntity? GetByGuid(Guid guid)
    {
        var entity = _dbContext.Set<TEntity>().Find(guid);
        _dbContext.ChangeTracker.Clear();
        return entity;
    }

    public TEntity Create(TEntity entity)
    {
        try
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null!;
        }
    }

    public bool Update(TEntity entity)
    {
        try
        {
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool Delete(TEntity entity)
    {
        try
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}