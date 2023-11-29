using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.ProductActionFilters;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductStorageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductStorageController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductStorageDto>> GetAll() 
        {
            IEnumerable<ProductStorageDto> productStorageDtos = _mapper.Map<IEnumerable<ProductStorageDto>>(_unitOfWork.ProductStorageRepo.GetAll());
            return Ok(productStorageDtos);
        }
        [HttpGet("{productStorageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(ProductStorageExistFilterAttribute))]
        public ActionResult<ProductStorageDto> Get([FromRoute] int productStorageId) 
        {
            ProductStorageDto productStorageDto = _mapper.Map<ProductStorageDto>(HttpContext.Items["productStorage"]);
            return Ok(productStorageDto);
        }
    }
}
