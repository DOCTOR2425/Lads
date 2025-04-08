using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FolderViewer
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"Error in {context.ActionDescriptor.DisplayName}: {context.Exception.Message}");

            var controllerName = context.RouteData.Values["controller"].ToString();

            var statusCode = context.Exception switch
            {
                NotImplementedException => 501,
                ArgumentNullException => 400,
                UnauthorizedAccessException => 401,
                InvalidOperationException => 409,
                TimeoutException => 408,
                KeyNotFoundException => 404,
                FormatException => 400,
                DivideByZeroException => 422,
                StackOverflowException => 500,
                OutOfMemoryException => 507,
                _ => 500
            };


            context.Result = new ObjectResult(new
            {
                Error = context.Exception.Message,
                Controller = controllerName,
                Timestamp = DateTime.UtcNow,
                StatusCode = statusCode
            })
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
