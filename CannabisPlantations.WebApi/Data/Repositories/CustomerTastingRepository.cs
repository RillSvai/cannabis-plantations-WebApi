using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class CustomerTastingRepository : Repository<CustomerTastings>, ICustomerTastingRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerTastingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
