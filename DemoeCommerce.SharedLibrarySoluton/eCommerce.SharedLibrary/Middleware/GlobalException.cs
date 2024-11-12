using eCommerce.SharedLibrary.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;



namespace eCommerce.SharedLibrary.Middleware
{
    public  class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            string message = " sorry , internal server error occoured . kindly try again";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "Error";
            try
            {
                await next(context);
                //check if the Response  is Too many Request // 129 Status code .
                if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    title = "Warning";
                    message = "Too many request made .";
                    statusCode = (int)StatusCodes.Status429TooManyRequests;
                    await ModifyHeader(context, title, message, statusCode);

                }
                //If the response  is authorize //401  status code 
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    title = "Alert";
                    message = "You are not authorized to access .";
                    await ModifyHeader(context, title, message, statusCode);
                }
                if(context.Response.StatusCode== StatusCodes.Status401Unauthorized)
                {
                    title = "Out of Access";
                    message = " you are not allowed/required to access .";
                    statusCode = StatusCodes.Status403Forbidden;
                    await ModifyHeader(context, title, message, statusCode);

                }
            } catch(Exception ex)
            {
                //Log original Exception/Console
                LogException.LogExceptions(ex);
                //check if  Exception is TimeOut
                if(ex is TaskCanceledException || ex is TimeoutException)
                {
                    title = "Out os time";
                    message = "Request timeout .. try again";
                    statusCode = StatusCodes.Status408RequestTimeout;
                }
                // if the exception is caught .
                //  if hone of the exception is then do the deafault :
                await ModifyHeader(context, title, message, statusCode);
            }
        }

        private async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
        {
           //display scary-free message to client 
           context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails()
            {
                Detail= message,
                Status=statusCode,
                Title =title,

            }), CancellationToken.None);
            return;
        }
    }
}
