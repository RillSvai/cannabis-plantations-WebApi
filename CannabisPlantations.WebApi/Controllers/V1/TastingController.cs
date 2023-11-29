using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.AgronomistActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.ProductActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.TastingActionFilters;
using CannabisPlantations.WebApi.Models;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TastingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TastingController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TastingDto>> GetAll() 
        {
            IEnumerable<TastingDto> tastingDtos = _mapper.Map<IEnumerable<TastingDto>>(_unitOfWork.TastingRepo.GetAll());
            return Ok(tastingDtos);
        }
        [HttpGet("{tastingId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(TastingExistFilterAttribute))]
        public ActionResult<TastingDto> Get([FromRoute] int tastingId) 
        {
            TastingDto tastingDto = _mapper.Map<TastingDto>(HttpContext.Items["tasting"]);
            return Ok(tastingDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(ProductExistFilterAttribute))]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public async Task<ActionResult<TastingDto>> Create([FromBody] TastingUpsertDto tastingDto, [FromQuery] int productId, [FromQuery] int agronomistId) 
        {
            Tasting tasting = new Tasting() 
            {
                Date = (DateTime)tastingDto.Date!,
                ProductId = productId,
                AgronomistId = agronomistId
            };
            await _unitOfWork.TastingRepo.InsertAsync(tasting);
            await _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new { tastingId = tasting.Id }, _mapper.Map<TastingDto>(tasting));
        }
        [HttpPut("{tastingId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(ProductExistFilterAttribute))]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        [TypeFilter(typeof(TastingExistFilterAttribute))]
        public async Task<IActionResult> Update([FromRoute] int tastingId, [FromBody] TastingUpsertDto tastingDto, [FromQuery] int productId, [FromQuery] int agronomistId) 
        {
            Tasting tasting = new Tasting()
            {
                Id = tastingId,
                Date = (DateTime)tastingDto.Date!,
                AgronomistId = agronomistId,
                ProductId = productId,
            };
            _unitOfWork.TastingRepo.Update(tasting);
            await _unitOfWork.Save();
            return NoContent();
        }
        [HttpDelete("{tastingId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(TastingExistFilterAttribute))]
        public async Task<IActionResult> Delete([FromRoute] int tastingId) 
        {
            Tasting tasting = (Tasting)HttpContext.Items["tasting"]!; 
            _unitOfWork.TastingRepo.Delete(tasting);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
