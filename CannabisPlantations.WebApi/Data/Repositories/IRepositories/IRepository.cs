using System.Linq.Expressions;

namespace CannabisPlantations.WebApi.Data.Repositories.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null, string includeProperties = "");
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter = null, string includeProperties = "", bool isTracked = false);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
