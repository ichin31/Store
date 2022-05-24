using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _enviroment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment enviroment) 
        {
            _next = next;
            _logger = logger;
            _enviroment = enviroment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var res = new ProblemDetails
                {
                    Status = 500,
                    Detail = _enviroment.IsDevelopment() ? ex.StackTrace?.ToString() : null,
                    Title = ex.Message,
                };
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy =
                    JsonNamingPolicy.CamelCase
                };
                var json = JsonSerializer.Serialize(res, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
