using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tuya.Transversal.ResponseModel;

namespace Tuya.Transversal.Exceptions
{
    public class ExceptionMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                ForbiddenException => StatusCodes.Status403Forbidden,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = exception is BadRequestException ?
                JsonSerializer.Deserialize<MessageResponse>(exception.Message) :
                new MessageResponse() { Status = statusCode, Message = $"{GetCustomMessage(statusCode)}: {exception.Message}" };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }

        private static string GetCustomMessage(int statusCode) => statusCode switch
        {
            StatusCodes.Status400BadRequest => "Error en los parametros",
            StatusCodes.Status401Unauthorized => "No autorizado",
            StatusCodes.Status403Forbidden => "Usuario no autorizado",
            StatusCodes.Status404NotFound => "Recurso no encontrado",
            StatusCodes.Status500InternalServerError => "Ha ocurrido un error interno",
            _ => "Error inesperado."
        };
    }
}
