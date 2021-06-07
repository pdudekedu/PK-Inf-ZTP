using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.ErrorHandling.Exceptions;

namespace WorkManager.Validation
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                throw new ValidationException(context.ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToArray());
            }
            else
            {
                await next();
            }
        }
    }
}
