using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters.CannabisTypeActionFilters
{
    public class CannabisTypeExistFilterAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public CannabisTypeExistFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int? id = context.ActionArguments["cannabisTypeId"] as int?;
            CannabisType? cannabisType = await _unitOfWork.CannabisTypeRepo.GetAsync(ct => ct.Id == id);
            if (cannabisType is null) 
            {
                context.ModelState.AddModelError("Cannabis Type", "Cannabis type doesn`t exist.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState) 
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(details);
                return;
            }
            context.HttpContext.Items["cannabisType"] = cannabisType;
            await next();
        }
    }
}
