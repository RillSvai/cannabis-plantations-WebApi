using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class TastingRepository : Repository<Tasting>, ITastingRepository
    {
        private readonly ApplicationDbContext _db;
        public TastingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public override void Delete(Tasting entity)
        {
            _db.CustomerTastings.RemoveRange(_db.CustomerTastings.Where(ct => ct.TastingId == entity.Id));
            base.Delete(entity);
        }
        public override void DeleteRange(IEnumerable<Tasting> entities)
        {
            IEnumerable<CustomerTastings> customerTastings = _db.CustomerTastings.Where(ct => entities.Any(t => t.Id == ct.TastingId));
            _db.RemoveRange(customerTastings);
            base.DeleteRange(entities);
        }
    }
}
