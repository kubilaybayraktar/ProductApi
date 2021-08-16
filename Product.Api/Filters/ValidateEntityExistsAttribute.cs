using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Product.Api.Filters
{
    public class ValidateEntityExistsAttribute<T> : IActionFilter where T : class, IItem
    {
        private readonly DataContext _context;

        public ValidateEntityExistsAttribute(DataContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int id = 0;

            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (int)context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }

            T entity = _context.Set<T>().SingleOrDefault(x => x.Id == id);

            if (entity.IsNull())
                context.Result = new NotFoundResult();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
