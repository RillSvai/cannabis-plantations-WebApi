using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll() 
        {
            return Ok(_unitOfWork.ProductRepo.GetAll());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> Get([FromRoute] int id) 
        {
            return Ok(await _unitOfWork.ProductRepo.GetAsync(p => p.Id == id));
        }
    }
}
