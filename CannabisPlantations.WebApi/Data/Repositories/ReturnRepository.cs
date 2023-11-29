using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class ReturnRepository : Repository<Return>, IReturnRepository
    {
        private readonly ApplicationDbContext _db;
        public ReturnRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public override void Delete(Return entity)
        {
            _db.ReturnDetails.RemoveRange(_db.ReturnDetails.Where(rd => rd.ReturnId == entity.Id));
            base.Delete(entity);
        }
        public override void DeleteRange(IEnumerable<Return> entities)
        {
            IEnumerable<ReturnDetail> returnDetails = _db.ReturnDetails.Where(rd => entities.Any(r => r.Id == rd.ReturnId));
            base.DeleteRange(entities);
        }
    }
}
