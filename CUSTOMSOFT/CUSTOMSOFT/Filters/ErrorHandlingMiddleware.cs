using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CUSTOMSOFT.API.Filters
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (!context.Response.HasStarted && context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    await HandleNotFoundAsync(context);
                }
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error occurred.");
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            var errorMessage = "Validation error occurred";
            var statusCode = (int)HttpStatusCode.BadRequest;

            var problemDetails = new ValidationProblemDetails
            {
                Status = statusCode,
                Title = errorMessage
            };

            //foreach (var validationResult in exception.ValidationResult.Errors)
            //{
            //    problemDetails.Errors.Add(validationResult.PropertyName, new[] { validationResult.ErrorMessage });
            //}

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorMessage = "An error occurred while processing your request";
            var statusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = errorMessage,
                Detail = exception.Message
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
        }

        private async Task HandleNotFoundAsync(HttpContext context)
        {
            var errorMessage = "Resource not found";
            var statusCode = (int)HttpStatusCode.NotFound;

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = errorMessage
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
        }
    }
}
