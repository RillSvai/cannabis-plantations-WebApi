using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters.TastingActionFilters
{
    public class TastingUpsertFilterAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public TastingUpsertFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            TastingUpsertDto? tastingDto = context.ActionArguments["tastingDto"] as TastingUpsertDto;
            if (tastingDto is null)
            {
                context.ModelState.AddModelError("Tasting", "Object is null.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (tastingDto.CustomerIds is null || tastingDto.CustomerIds.Length == 0)
            {
                await next();
                return;
            }
            if (tastingDto.CustomerIds.Any(i => i <= 0))
            {
                context.ModelState.AddModelError("CustomerId", "At least one customer`s id is not valid.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (tastingDto.CustomerIds.Any(i => _unitOfWork.CustomerRepo.Get(t => t.Id == i) is null))
            {
                context.ModelState.AddModelError("Customer", "At least one customer doesn`t exist.");
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
