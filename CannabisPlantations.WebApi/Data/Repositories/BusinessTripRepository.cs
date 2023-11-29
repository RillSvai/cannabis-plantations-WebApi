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
    }
}
