using System.Net;
using System.Text.Json;
using FluentValidation;

namespace SmartBlog.Api.Middleware;

// Middleware som sitter längst ut i request-pipelinen och fångar alla exceptions
// Returnerar strukturerade JSON-felsvar istället för att låta exceptions bubbla upp
public class ErrorHandlingMiddleware
{
    // _next är nästa steg i pipelinen - vi anropar det inuti try/catch
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Försök köra resten av pipelinen (controller, handler osv)
            await _next(context);
        }
        catch (ValidationException ex)
        {
            // Fångar valideringsfel från FluentValidation (t.ex. tomt Title)
            // Returnerar 400 Bad Request med en lista av vilka fält som felade
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors.Select(e => new
            {
                field = e.PropertyName,
                message = e.ErrorMessage
            });

            var response = new { status = 400, errors };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            // Fångar alla andra oväntade fel (databasfel, null references osv)
            // Returnerar 500 Internal Server Error med ett generiskt meddelande
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                status = 500,
                message = "An unexpected error occurred.",
                detail = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
