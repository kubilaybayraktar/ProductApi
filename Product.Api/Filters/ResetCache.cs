using Microsoft.AspNetCore.Mvc.Filters;
using Product.Cache;

namespace Product.Api.Filters
{
    public class ResetCache : ActionFilterAttribute
    {
        private readonly ICacheService _cacheService;
        public ResetCache(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _cacheService.ResetCache();
        }
    }
}
