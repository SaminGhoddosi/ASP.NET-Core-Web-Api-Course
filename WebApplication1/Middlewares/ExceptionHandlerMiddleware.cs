using System.Net;

namespace WebApplication1.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next; //representa o próximo middleware
        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate requestDelegate)
        {
            _logger = logger;
            _next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)//Task pode retornar nada, apenas sinalizar que terminou
        {
            try
            {
                await _next(httpContext); //vai para o próximo middleware
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid(); //é o "número de protocolo" do erro
                _logger.LogError(ex,$"{errorId} : {ex.Message}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;//500
                // redundante -> httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong! We are going to resolve this"
                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
