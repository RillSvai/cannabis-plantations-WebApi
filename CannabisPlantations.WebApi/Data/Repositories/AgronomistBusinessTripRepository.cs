using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class AgronomistBusinessTripRepository : Repository<AgronomistBusinessTrips>, IAgronomistBusinessTripRepository
    {
        private readonly ApplicationDbContext _db;
        public AgronomistBusinessTripRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;            
        }

        public IEnumerable<Agronomist?> GetAgronomists(int businessTripId)
        {
            return _db.AgronomistBusinessTrips
                .Where(abt => abt.BusinessTripId == businessTripId)
                .Select(abt => _db.Agronomists.FirstOrDefault(a => a.Id == abt.AgronomistId));
        }

        public IEnumerable<BusinessTrip?> GetBusinessTrips(int agronomistId)
        {
            return _db.AgronomistBusinessTrips
                .Where(abt => abt.AgronomistId == agronomistId)
                .Select(abt => _db.BusinessTrips.FirstOrDefault(b => b.Id == abt.BusinessTripId));
        }
    }
}
