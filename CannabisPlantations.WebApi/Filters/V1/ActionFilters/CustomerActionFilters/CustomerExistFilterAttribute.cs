using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters.CustomerActionFilters
{
    public class CustomerExistFilterAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerExistFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int? customerId = context.ActionArguments["customerId"] as int?;
            Customer? customer = await _unitOfWork.CustomerRepo.GetAsync(c => c.Id == customerId);
            if (customer is null) 
            {
                context.ModelState.AddModelError("Customer", "Customer doesn`t exist.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState) 
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(details);
                return;
            }
            context.HttpContext.Items["customer"] = customer;
            await next();
        }
    }
}
