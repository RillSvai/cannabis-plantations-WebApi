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
    }
}
