using System.Net;
using System.Text.Json;
using Azure;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;

namespace E_Commerce.API.Middelwares
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandling> _logger;

        // RequstDelegate
        // HttpContect

        public GlobalExceptionHandling(RequestDelegate next , ILogger<GlobalExceptionHandling> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
               await _next(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound) 
                {
                    await HandelNotFoundApiAsync(context); 
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"SomeThing went wrong : {ex.Message}");
                await HandlingExceptionAsync(context, ex);
            }
        }

        private async Task HandelNotFoundApiAsync(HttpContext context)
        {
            context.Response.ContentType= "application/json";
            var Responce = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"The EndPointWith Url {context.Request.Path} not found "

            }.ToString();
            await context.Response.WriteAsync(Responce);
        }

        private async Task HandlingExceptionAsync(HttpContext context, Exception ex)
        {
            //1] Change statusCode
            //2] change content type :
            //3]write Response in Body :
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var Response = new ErrorDetails()
            {
                ErrorMessage = ex.Message
            };

            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                RegisterationException registeration => HandleRegisterationException(registeration,Response),
                (_) => StatusCodes.Status500InternalServerError

            };
 
            Response.StatusCode = context.Response.StatusCode;
            
           // var result = JsonSerializer.Serialize(Response);
           //await context.Response.WriteAsJsonAsync(Response);
           await context.Response.WriteAsync(Response.ToString());
        }

        private int HandleRegisterationException(RegisterationException registerationException, ErrorDetails response)
        {
            response.Errors = registerationException.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
