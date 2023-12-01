using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.AgronomistActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.CannabisTypeActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.HarvestActionFilters;
using CannabisPlantations.WebApi.Models;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class HarvestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HarvestController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HarvestDto>> GetAll() 
        {
            IEnumerable<HarvestDto> harvestDtos = _mapper.Map<IEnumerable<HarvestDto>>(_unitOfWork.HarvestRepo.GetAll());
            return Ok(harvestDtos);
        }
        [HttpGet("{harvestId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(HarvestExistFilterAttribute))]
        public ActionResult<HarvestDto> Get([FromRoute] int harvestId) 
        {
            HarvestDto harvestDto = _mapper.Map<HarvestDto>(HttpContext.Items["harvest"]);
            return Ok(harvestDto);
        }
        [HttpGet("harvested-different-n-types-duration/agronomists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        public ActionResult<AgronomistDto> GetAgronomistByDifferentHarvestedTypes([FromQuery] int cannabisTypeNumber, [FromQuery] DateTime since, [FromQuery] DateTime until) 
        {
            IEnumerable<AgronomistDto> agronomistDtos = _mapper.Map<IEnumerable<AgronomistDto>>
                (_unitOfWork.HarvestRepo.GetAgronomistByDifferentHarvestedTypes(cannabisTypeNumber, since, until));
            return Ok(agronomistDtos);  
        }
        [HttpGet("harvested-at-least-n-times-duration/cannabistypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        public ActionResult<CannabisTypeDto> GetCannabisTypesByMinHarvests([FromQuery] int harvestNumber, [FromQuery] DateTime since, [FromQuery] DateTime until) 
        {
            IEnumerable<CannabisTypeDto> cannabisTypeDtos = _mapper.Map<IEnumerable<CannabisTypeDto>>
                (_unitOfWork.HarvestRepo.GetCannabisTypesByMinHarvests(harvestNumber, since, until));
            return Ok(cannabisTypeDtos);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        [TypeFilter(typeof(CannabisTypeExistFilterAttribute))]
        public async Task<ActionResult<HarvestDto>> Create([FromBody] HarvestUpsertDto harvestDto, [FromQuery] int agronomistId, [FromQuery] int cannabisTypeId) 
        {
            Harvest harvest = new Harvest() 
            {
                Quantity = (int)harvestDto.Quantity!,
                Date = (DateTime)harvestDto.Date!,
                AgronomistId = agronomistId,
                CannabisTypeId = cannabisTypeId
            };
            await _unitOfWork.HarvestRepo.InsertAsync(harvest);
            await _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new { harvestId = harvest.Id }, _mapper.Map<HarvestDto>(harvest));
        }
        [HttpPut("{harvestId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        [TypeFilter(typeof(CannabisTypeExistFilterAttribute))]
        [TypeFilter(typeof(HarvestExistFilterAttribute))]
        public async Task<IActionResult> Update([FromRoute] int harvestId, [FromBody] HarvestUpsertDto harvestDto, [FromQuery] int agronomistId, [FromQuery] int cannabisTypeId) 
        {
            Harvest harvest = new Harvest() 
            {
                Id = harvestId,
                Quantity = (int)harvestDto.Quantity!,
                Date = (DateTime)harvestDto.Date!,
                AgronomistId = agronomistId,
                CannabisTypeId = cannabisTypeId
            };
            _unitOfWork.HarvestRepo.Update(harvest);
            await _unitOfWork.Save();
            return NoContent();
        }
        [HttpDelete("{harvestId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(HarvestExistFilterAttribute))]
        public async Task<IActionResult> Delete([FromRoute] int harvestId) 
        {
            Harvest harvest = (Harvest)HttpContext.Items["harvest"]!;
            _unitOfWork.HarvestRepo.Delete(harvest);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
