namespace CannabisPlantations.WebApi.Data.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepo { get; }
        ICannabisTypeRepository CannabisTypeRepo { get; }
        IAgronomistRepository AgronomistRepo { get; }
        ICustomerRepository CustomerRepo { get; }
        Task Save();
    }
}
