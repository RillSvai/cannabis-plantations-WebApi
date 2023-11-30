using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters.OrderActionFilters
{
    public class OrderUpsertFilterAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderUpsertFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            OrderUpsertDto? orderDto = context.ActionArguments["orderDto"] as OrderUpsertDto;
            if (orderDto is null)
            {
                context.ModelState.AddModelError("Order", "Object is null.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            orderDto.ProductIds ??= Array.Empty<int>();
            orderDto.ProductQuantities ??= Array.Empty<int>();
            if (orderDto.ProductIds.Length == 0 && orderDto.ProductQuantities.Length == 0)
            {
                await next();
                return;
            }
            if (orderDto.ProductIds.Length != orderDto.ProductQuantities.Length) 
            {
                context.ModelState.AddModelError("", "Number of products and number of quantities should be equal.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (orderDto.ProductIds.Any(i => i <= 0) || orderDto.ProductQuantities.Any(q => q <= 0))
            {
                context.ModelState.AddModelError("", "At least one order`s id or quantity is not valid.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (orderDto.ProductIds.Any(i => _unitOfWork.ProductRepo.Get(p => p.Id == i) is null))
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
