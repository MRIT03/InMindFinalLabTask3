using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace InMindLab1.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorResponse = new
            {
                Message = "An error occurred while processing your request.",
                Error = exception.Message,
                Timestamp = DateTime.UtcNow
            };

            Console.WriteLine("ERROR: Custom Exception Handler");
            Console.WriteLine($"Message: {exception.Message}");

            Console.WriteLine(new string('=', 50));

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                WriteIndented = true
            }), cancellationToken);

            return true; 
        }
    }
}