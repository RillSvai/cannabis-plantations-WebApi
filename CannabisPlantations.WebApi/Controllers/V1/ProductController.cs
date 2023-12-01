using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.AgronomistActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.CannabisTypeActionFilters;
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
            IEnumerable<ProductDto> productDtos = _mapper
                .Map<IEnumerable<ProductDto>>
                (_unitOfWork.ProductRepo.GetAll());
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
            ProductDto productDto = _mapper
                .Map<ProductDto>
                (HttpContext.Items["product"]);
            return Ok(productDto);
        }
        [HttpGet("{productId:int}/tastings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(ProductExistFilterAttribute))]
        public ActionResult<IEnumerable<TastingDto>> GetTastings([FromRoute] int productId)
        {
            IEnumerable<TastingDto> tastingDtos = _mapper
                .Map<IEnumerable<TastingDto>>
                (_unitOfWork.TastingRepo.GetAll(t => t.ProductId == productId));
            return Ok(tastingDtos);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [IdFilter]
        [TypeFilter(typeof(CannabisTypeExistFilterAttribute))]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]

        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductUpsertDto productDto, [FromQuery] int cannabisTypeId, [FromQuery] int agronomistId) 
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
        [HttpPut("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(CannabisTypeExistFilterAttribute))]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        [TypeFilter(typeof(ProductExistFilterAttribute))]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] ProductUpsertDto productDto, [FromQuery] int cannabisTypeId, [FromQuery] int agronomistId) 
        {
            Product product = new Product
            {
                Id = productId,
                Price = productDto.Price,
                CannabisTypeId = cannabisTypeId,
                AgronomistId = agronomistId
            };
            _unitOfWork.ProductRepo.Update(product);
            await _unitOfWork.Save();
            return NoContent();
        }
        [HttpDelete("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(ProductExistFilterAttribute))]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            Product? product = HttpContext.Items["product"] as Product;
            _unitOfWork.TastingRepo.DeleteRange(_unitOfWork.TastingRepo.GetAll(t => t.ProductId == productId));
            _unitOfWork.ProductRepo.Delete(product!);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
