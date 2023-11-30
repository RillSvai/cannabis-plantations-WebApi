using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters.BusinessTripActionFilters
{
    public class BusinessTripUpsertFilterAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public BusinessTripUpsertFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            BusinessTripUpsertDto? businessTripDto = context.ActionArguments["businessTripDto"] as BusinessTripUpsertDto;
            if (businessTripDto is null)
            {
                context.ModelState.AddModelError("BusinessTrip", "Object is null.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (businessTripDto.AgronomistIds is null || businessTripDto.AgronomistIds.Length == 0)
            {
                await next();
                return;
            }
            if (businessTripDto.AgronomistIds.Any(i => i <= 0)) 
            {
                context.ModelState.AddModelError("AgronomistId", "At least one agronomist`s id is not valid.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (businessTripDto.AgronomistIds.Any(i => _unitOfWork.AgronomistRepo.Get(a => a.Id == i) is null))
            {
                context.ModelState.AddModelError("Agronomist", "At least one agronomist doesn`t exist.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(details);
                return;
            }
            await next();
        }
    }
}
