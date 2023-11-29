using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.ProductActionFilters;
using CannabisPlantations.WebApi.Models;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(ProductStorageExistFilterAttribute))]
        public ActionResult<ProductStorageDto> Get([FromRoute] int productStorageId) 
        {
            ProductStorageDto productStorageDto = _mapper.Map<ProductStorageDto>(HttpContext.Items["productStorage"]);
            return Ok(productStorageDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(ProductExistFilterAttribute))]
        public async Task<ActionResult<ProductStorageDto>> Create([FromBody] ProductStorageUpsertDto productStorageDto, [FromQuery] int productId) 
        {
            ProductStorage productStorage = new ProductStorage() 
            {
                Quantity = (int)productStorageDto.Quantity!,
                ProductId = productId
            };
            await _unitOfWork.ProductStorageRepo.InsertAsync(productStorage);
            await _unitOfWork.Save();
            return NoContent();
        }
        [HttpPut("{productStorageId:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(ProductStorageExistFilterAttribute))]
        public async Task<IActionResult> Update([FromRoute] int productStorageId, [FromBody] ProductStorageUpsertDto productStorageDto) 
        {
            ProductStorage? productStorage = HttpContext.Items["productStorage"] as ProductStorage;
            productStorage!.Quantity = (int)productStorageDto.Quantity!;
            _unitOfWork.ProductStorageRepo.Update(productStorage);
            await _unitOfWork.Save();
            return NoContent();
        }

    }
}
