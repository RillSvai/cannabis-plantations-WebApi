namespace CannabisPlantations.WebApi.Data.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepo { get; }
        Task Save();
    }
}
