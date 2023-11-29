using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class BusinessTripRepository : Repository<BusinessTrip>, IBusinessTripRepository
    {
        private readonly ApplicationDbContext _db;
        public BusinessTripRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public override void Delete(BusinessTrip entity)
        {
            _db.AgronomistBusinessTrips.RemoveRange(_db.AgronomistBusinessTrips.Where(abt => abt.BusinessTripId == entity.Id));
            base.Delete(entity);
        }
        public override void DeleteRange(IEnumerable<BusinessTrip> entities)
        {
            IEnumerable<AgronomistBusinessTrips> agronomistBusinessTrips = _db.AgronomistBusinessTrips.Where(abt => entities.Any(bt => bt.Id == abt.BusinessTripId));
            base.DeleteRange(entities);
        }
    }
}
