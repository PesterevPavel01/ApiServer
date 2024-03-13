using WebApi.Domain.Interfaces.Repositories;

namespace WebApi.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {

        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null");

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return Task.FromResult(entity);
        }

        public Task<bool> CreateMultipleAsync(List<TEntity> entitys)
        {
            if (entitys.Count == 0)
                throw new ArgumentNullException("Entitys count 0");

            _dbContext.AddRange(entitys);
            _dbContext.SaveChanges();

            return Task.FromResult(true);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public Task<TEntity> RemoveAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null");

            _dbContext.Remove(entity);
            _dbContext.SaveChanges();

            return Task.FromResult(entity);
        }

        public Task<bool> SaveChangesAsync()
        {
            try 
            {
                _dbContext.SaveChanges();
                return Task.FromResult(true);
            }
            catch 
            { 
            return Task.FromResult(false);
            }
            
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null");

            _dbContext.Update(entity);
            _dbContext.SaveChanges();

            return Task.FromResult(entity);
        }
    }
}
