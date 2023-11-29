using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.CustomerActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.FeedbackActionFilters;
using CannabisPlantations.WebApi.Models;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FeedbackController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FeedbackController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<FeedbackDto>> GetAll() 
        {
            IEnumerable<FeedbackDto> feedbackDtos = _mapper.Map<IEnumerable<FeedbackDto>>(_unitOfWork.FeedbackRepo.GetAll());
            return Ok(feedbackDtos);
        }
        [HttpGet("{feedbackId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(FeedbackExistFilterAttribute))]
        public ActionResult<FeedbackDto> Get([FromRoute] int feedbackId) 
        {
            FeedbackDto feedbackDto = _mapper.Map<FeedbackDto>(HttpContext.Items["feedback"]);
            return Ok(feedbackDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        public async Task<ActionResult<FeedbackDto>> Create([FromBody] FeedbackUpsertDto feedbackDto, [FromQuery] int customerId)
        {
            Feedback feedback = new Feedback() 
            {
                Date = (DateTime)feedbackDto.Date!,
                Text = feedbackDto.Text,
                CustomerId = customerId,
            };
            await _unitOfWork.FeedbackRepo.InsertAsync(feedback);
            await _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new {feedbackId = feedback.Id}, _mapper.Map<FeedbackDto>(feedback));
        }
        [HttpPut("{feedbackId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        [TypeFilter(typeof(FeedbackExistFilterAttribute))]
        public async Task<IActionResult> Update([FromRoute] int feedbackId, [FromBody] FeedbackUpsertDto feedbackDto, [FromQuery] int customerId) 
        {
            Feedback feedback = new Feedback() 
            {
                Id = feedbackId,
                Text = feedbackDto.Text,
                Date = (DateTime)feedbackDto.Date!,
                CustomerId = customerId,
            };
            _unitOfWork.FeedbackRepo.Update(feedback);
            await _unitOfWork.Save();
            return NoContent();
        }
        [HttpDelete("{feedbackId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(FeedbackExistFilterAttribute))]
        public async Task<IActionResult> Delete([FromRoute] int feedbackId) 
        {
            Feedback feedback = (Feedback)HttpContext.Items["feedback"]!;
            _unitOfWork.FeedbackRepo.Delete(feedback);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
