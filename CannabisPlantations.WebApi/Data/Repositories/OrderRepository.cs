using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public override void Delete(Order entity)
        {
            _db.OrderDetails.RemoveRange(_db.OrderDetails.Where(od => od.OrderId == entity.Id));
            base.Delete(entity);
        }
        public override void DeleteRange(IEnumerable<Order> entities)
        {
            IEnumerable<OrderDetail> orderDetails = _db.OrderDetails.Where(od => entities.Any(o => o.Id == od.OrderId));
            _db.OrderDetails.RemoveRange(orderDetails);
            base.DeleteRange(entities);
        }
    }
}
