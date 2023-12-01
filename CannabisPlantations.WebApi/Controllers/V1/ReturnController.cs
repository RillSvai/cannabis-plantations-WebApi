using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.ProductActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.ReturnActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.AgronomistActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.CustomerActionFilters;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ReturnController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReturnController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ReturnDto>> GetAll()
        {
            IEnumerable<ReturnDto> returnDtos = _mapper
                .Map<IEnumerable<ReturnDto>>
                (_unitOfWork.ReturnRepo.GetAll());
            return Ok(returnDtos);
        }
        [HttpGet("{returnId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(ReturnExistFilterAttribute))]
        public ActionResult<ReturnDto> Get([FromRoute] int returnId)
        {
            ReturnDto returnDto = _mapper
                .Map<ReturnDto>
                (HttpContext.Items["return"]);
            return Ok(returnDto);
        }
        [HttpGet("{returnId}/return-details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(ReturnExistFilterAttribute))]
        public ActionResult<ReturnDetailDto> GetReturnDetails([FromRoute] int returnId) 
        {
            IEnumerable<ReturnDetailDto> returnDetailDtos = _mapper
                .Map<IEnumerable<ReturnDetailDto>>
                (_unitOfWork.ReturnDetailRepo.GetAll(rd => rd.ReturnId == returnId));
            return Ok(returnDetailDtos);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        [TypeFilter(typeof(ReturnUpsertFilterAttribute))]
        public async Task<ActionResult<ReturnDto>> Create([FromBody] ReturnUpsertDto returnDto, [FromQuery] int agronomistId, [FromQuery] int customerId) 
        {
            Return productReturn = new Return()
            {
                Date = (DateTime)returnDto.Date!,
                AgronomistId = agronomistId,
                CustomerId = customerId
            };
            await _unitOfWork.ReturnRepo.InsertAsync(productReturn);
            await _unitOfWork.Save();
            await _unitOfWork.ReturnDetailRepo
               .InsertRangeAsync(Enumerable.Range(0, returnDto.ProductQuantities?.Length ?? 0)
               .Select(i => new ReturnDetail
               {
                   ProductId = returnDto.ProductIds![i],
                   Quantity = returnDto.ProductQuantities![i],
                   ReturnId = productReturn.Id
               }));
            await _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new { returnId = productReturn.Id }, _mapper.Map<ReturnDto>(productReturn));
        }
        [HttpPut("{returnId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        [TypeFilter(typeof(ReturnExistFilterAttribute))]
        [TypeFilter(typeof(ReturnUpsertFilterAttribute))]
        public async Task<IActionResult> Update([FromRoute] int returnId, [FromBody] ReturnUpsertDto returnDto, [FromQuery] int agronomistId, [FromQuery] int customerId) 
        {
            Return productReturn = new Return()
            {
                Id = returnId,
                AgronomistId = agronomistId,
                CustomerId = customerId,
                Date = (DateTime)returnDto.Date!,
            }; 
            _unitOfWork.ReturnRepo.Update(productReturn);
            await _unitOfWork.Save();
            _unitOfWork.ReturnDetailRepo.DeleteRange(_unitOfWork.ReturnDetailRepo.GetAll(rd => rd.ReturnId == returnId));
            await _unitOfWork.ReturnDetailRepo
               .InsertRangeAsync(Enumerable.Range(0, returnDto.ProductQuantities?.Length ?? 0)
               .Select(i => new ReturnDetail
               {
                   ProductId = returnDto.ProductIds![i],
                   Quantity = returnDto.ProductQuantities![i],
                   ReturnId = productReturn.Id
               }));
            await _unitOfWork.Save();
            return NoContent();
        }
        [HttpDelete("{returnId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(ReturnExistFilterAttribute))]
        public async Task<IActionResult> Delete([FromRoute] int returnId) 
        {
            Return productReturn = (Return)HttpContext.Items["return"]!;
            _unitOfWork.ReturnRepo.Delete(productReturn);
            await _unitOfWork.Save();   
            return NoContent();
        }
    }
}
