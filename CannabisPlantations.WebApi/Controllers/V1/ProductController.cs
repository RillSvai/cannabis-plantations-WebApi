using CannabisPlantations.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll() 
        {
            return Ok(new List<Product>());
        }
    }
}
