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

        public IEnumerable<Customer?> GetCustomersByMinPurchasedDifferentProducts(int productNumber, DateTime since, DateTime until)
        {
            return _db.OrderDetails
                .Join(_db.Orders, od => od.OrderId, o => o.Id, (od, o) => new
                {
                    CustomerId = o.CustomerId,
                    ProductId = od.ProductId,
                    Date = o.Date
                })
                .Where(od_o => od_o.Date >= since && od_o.Date <= until)
                .GroupBy(o => o.CustomerId)
                .Where(g => g.Count() >= productNumber)
                .Select(g => _db.Customers.FirstOrDefault(c => c.Id == g.Key));
        }

        public IEnumerable<Product?> GetProductsPurchasedDifferentCustomers(int customerNumber, DateTime since, DateTime until)
        {
            return _db.Orders
                .Where(o => o.Date >= since && o.Date <= until)
                .Join(_db.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new
                {
                    ProductId = od.ProductId,
                    CustomerId = o.CustomerId,

                })
                .GroupBy(od_o => od_o.ProductId)
                .Where(g => g.Select(g => g.CustomerId).Distinct().Count() >= customerNumber)
                .OrderByDescending(g => _db.ReturnDetails.Count(rd => rd.ProductId == g.Key 
                && _db.Returns.FirstOrDefault(r => r.Id == rd.ReturnId)!.Date <= until 
                && _db.Returns.FirstOrDefault(r => r.Id == rd.ReturnId)!.Date >= since) / g.Count())
                .Select(g => _db.Products.FirstOrDefault(p => p.Id == g.Key));
        }
    }
}
