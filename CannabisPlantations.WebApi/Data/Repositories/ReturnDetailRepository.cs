using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class ReturnDetailRepository : Repository<ReturnDetail>, IReturnDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public ReturnDetailRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;   
        }
    }
}
