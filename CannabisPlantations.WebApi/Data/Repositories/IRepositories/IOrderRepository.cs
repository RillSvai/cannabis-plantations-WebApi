﻿using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories.IRepositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Customer?> GetCustomersByMinPurchasedDifferentProducts(int productNumber, DateTime since, DateTime until);
        IEnumerable<Product?> GetProductsPurchasedDifferentCustomers(int customerNumber, DateTime since, DateTime until);
    }
}
