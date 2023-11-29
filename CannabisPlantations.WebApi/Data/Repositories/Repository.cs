using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Set<TEntity>();
        }
        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter is not null) 
            {
                query = query.Where(filter);
            }
            foreach (string includeProperty in includeProperties.Split(',',StringSplitOptions.RemoveEmptyEntries)) 
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter = null, string includeProperties = "", bool isTracked = false)
        {
            IQueryable<TEntity> query = isTracked ? _dbSet : _dbSet.AsNoTracking();
            if (filter is not null)
            {
                query = query.Where(filter);
            }
            foreach (string includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
