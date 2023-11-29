using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public override void Delete(Product entity)
        {
            _db.OrderDetails.RemoveRange(_db.OrderDetails.Where(od => od.ProductId == entity.Id));
            _db.ProductStorage.RemoveRange(_db.ProductStorage.Where(ps => ps.ProductId == entity.Id));
            _db.ReturnDetails.RemoveRange(_db.ReturnDetails.Where(rd => rd.ProductId == entity.Id));
            base.Delete(entity);
        }
        public override void DeleteRange(IEnumerable<Product> entities)
        {
            IEnumerable<OrderDetail> orderDetails = _db.OrderDetails.Where(od => entities.Any(p => od.ProductId == p.Id));
            IEnumerable<ProductStorage> productStorages = _db.ProductStorage.Where(ps => entities.Any(p => ps.ProductId == p.Id));
            IEnumerable<ReturnDetail> returnDetails = _db.ReturnDetails.Where(rd => entities.Any(p => rd.ProductId == p.Id));
            _db.ReturnDetails.RemoveRange(returnDetails);
            _db.OrderDetails.RemoveRange(orderDetails);
            _db.ProductStorage.RemoveRange(productStorages);
            base.DeleteRange(entities);
        }
    }
}
