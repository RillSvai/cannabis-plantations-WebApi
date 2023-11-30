using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters.ReturnActionFilters
{
    public class ReturnUpsertFilterAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReturnUpsertFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ReturnUpsertDto? returnDto = context.ActionArguments["returnDto"] as ReturnUpsertDto;
            if (returnDto is null)
            {
                context.ModelState.AddModelError("Return", "Object is null.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            returnDto.ProductIds ??= Array.Empty<int>();
            returnDto.ProductQuantities ??= Array.Empty<int>();
            if (returnDto.ProductIds.Length == 0 && returnDto.ProductQuantities.Length == 0)
            {
                await next();
                return;
            }
            if (returnDto.ProductIds.Length != returnDto.ProductQuantities.Length)
            {
                context.ModelState.AddModelError("", "Number of products and number of quantities should be equal.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (returnDto.ProductIds.Any(i => i <= 0) || returnDto.ProductQuantities.Any(q => q <= 0))
            {
                context.ModelState.AddModelError("", "At least one return`s id or quantity is not valid.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (returnDto.ProductIds.Any(i => _unitOfWork.ProductRepo.Get(p => p.Id == i) is null))
            {
                context.ModelState.AddModelError("Product", "At least one product doesn`t exist.");
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
