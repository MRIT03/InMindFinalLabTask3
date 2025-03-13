using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;

namespace InMindLab1.Filters
{
    public class LoggingActionFilter : IActionFilter
    {
        private Stopwatch _stopwatch;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("[Start of Action Logging]");
            _stopwatch = Stopwatch.StartNew(); 

            string actionName = context.ActionDescriptor.DisplayName;
            string controllerName = context.Controller.ToString();

            Console.WriteLine($"[START] {controllerName} -> {actionName}");
            Console.WriteLine("   Parameters:");
            foreach (var param in context.ActionArguments)
            {
                Console.WriteLine($"     . {param.Key}: {param.Value}");
            }
            Console.WriteLine(new string('-', 50)); 
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("[End Of Action Logging]");
            _stopwatch.Stop(); 
            string actionName = context.ActionDescriptor.DisplayName;
            string status = context.Exception == null ? "SUCCESS :)" : " FAILED :(";

            Console.WriteLine($"[END] {actionName}");
            Console.WriteLine($"   Elapsed Time: {_stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"   Status: {status}");

            if (context.Exception != null)
            {
                Console.WriteLine($"   Exception: {context.Exception.Message}");
            }

            Console.WriteLine(new string('=', 50)); 

        }
    }
}