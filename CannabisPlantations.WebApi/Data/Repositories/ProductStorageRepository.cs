using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class ProductStorageRepository : Repository<ProductStorage>, IProductStorageRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductStorageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
