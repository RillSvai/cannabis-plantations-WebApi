using CannabisPlantations.WebApi.Data.Repositories.IRepositories;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ProductRepo = new ProductRepository(db);
        }
        public IProductRepository ProductRepo { get; }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
