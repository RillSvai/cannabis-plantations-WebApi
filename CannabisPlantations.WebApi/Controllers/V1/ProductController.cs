using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.ProductActionFilters;
using CannabisPlantations.WebApi.Models;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDto>> GetAll() 
        {
            IEnumerable<ProductDto> productDtos = _mapper.Map<IEnumerable<ProductDto>>(_unitOfWork.ProductRepo.GetAll());
            return Ok(productDtos);
        }
        [HttpGet("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(ProductExistFilterAttribute))]
        public ActionResult<ProductDto> Get([FromRoute] int productId) 
        {
            ProductDto productDto = _mapper.Map<ProductDto>(HttpContext.Items["product"]);
            return Ok(productDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductCreateDto productDto, [FromQuery] int cannabisTypeId, [FromQuery] int agronomistId) 
        {
            Product product = new Product
            {
                Price = productDto.Price,
                CannabisTypeId = cannabisTypeId,
                AgronomistId = agronomistId
            };
            await _unitOfWork.ProductRepo.InsertAsync(product);
            await _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new { productId = product.Id }, _mapper.Map<ProductDto>(product));
        }
    }
}
