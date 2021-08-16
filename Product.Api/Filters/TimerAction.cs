using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Product.Api.Filters
{
    public class TimerAction : ActionFilterAttribute
    {
        private Stopwatch stopwatchWorker;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            stopwatchWorker = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatchWorker.Stop();
            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();
            Debug.WriteLine($"TimerAction: {controller} . {action}: {stopwatchWorker.ElapsedMilliseconds} ms");
        }
    }
}
