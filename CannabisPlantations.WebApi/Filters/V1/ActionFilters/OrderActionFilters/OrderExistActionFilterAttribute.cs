using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters.OrderActionFilters
{
    public class OrderExistActionFilterAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderExistActionFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int? id = context.ActionArguments["orderId"] as int?;
            Order? order = await _unitOfWork.OrderRepo.GetAsync(o => o.Id == id);
            if (order is null)
            {
                context.ModelState.AddModelError("Order", "Order doesn`t exist.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState) 
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(details);
                return;
            }
            context.HttpContext.Items["order"] = order;
            await next();
        }
    }
}
