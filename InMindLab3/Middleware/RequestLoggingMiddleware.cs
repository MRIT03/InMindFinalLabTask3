using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using InMindLab1.Services;
using InMindLab3.Models;

namespace InMindLab1.Middleware
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("[Start of Logging Middleware]");
            Stopwatch stopwatch = Stopwatch.StartNew();

            string requestMethod = context.Request.Method;
            string requestPath = context.Request.Path;
            string requestQueryString = context.Request.QueryString.ToString();

            Console.WriteLine($"[Request] {requestMethod} {requestPath}{requestQueryString}");
            Console.WriteLine("[Headers]");
            foreach (var header in context.Request.Headers)
            {
                //to print out all the headers
                Console.WriteLine($"  {header.Key}: {header.Value}");
            }
            
            Console.WriteLine(new string('-', 50)); 


            await next(context); 
            Console.WriteLine("[End of Logging Middleware]");
            

            stopwatch.Stop();
            var publisher = new RabbitMQPublisherService();
            var logEntry = new LogEntry
            {
                RequestId = Guid.NewGuid(),
                RequestData = new Dictionary<string, object>
                {
                    { "Status Code", context.Response.StatusCode },
                    { "Elapsed TIme", stopwatch.ElapsedMilliseconds },
                    { "RequestMethod", requestMethod },
                    { "RequestQueryString", requestQueryString },
                },
                RouteURL = requestPath,
                Timestamp = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified) 
            };
            await publisher.PublishLogAsync(logEntry);

            Console.WriteLine($"[Response] Status Code: {context.Response.StatusCode}, Elapsed Time: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine(new string('-', 50)); 
        }
    }
}