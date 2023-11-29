using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class AgronomistRepository : Repository<Agronomist>, IAgronomistRepository
    {
        private readonly ApplicationDbContext _db;
        public AgronomistRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public override void Delete(Agronomist entity)
        {
            _db.AgronomistBusinessTrips.RemoveRange(_db.AgronomistBusinessTrips.Where(abt => abt.AgronomistId == entity.Id));
            _db.Harvests.RemoveRange(_db.Harvests.Where(h => h.AgronomistId == entity.Id));
            base.Delete(entity);
        }
        public override void DeleteRange(IEnumerable<Agronomist> entities)
        {
            IEnumerable<AgronomistBusinessTrips> agronomistBusinessTrips = _db.AgronomistBusinessTrips.Where(abt => entities.Any(a => abt.AgronomistId == a.Id));
            IEnumerable<Harvest> harvests = _db.Harvests.Where(h => entities.Any(a => h.AgronomistId == a.Id));
            _db.AgronomistBusinessTrips.RemoveRange(agronomistBusinessTrips);
            _db.Harvests.RemoveRange(harvests);
            base.DeleteRange(entities);
        }
    }
}
