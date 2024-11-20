namespace ApiGateway.Presentation.Middleware
{
    public class AttachSignatureToRequest
    {
        private readonly RequestDelegate _next;

        // Constructor accepting RequestDelegate
        public AttachSignatureToRequest(RequestDelegate next)
        {
            _next = next;
        }

        // Middleware logic
        public async Task InvokeAsync(HttpContext context)
        {
            // Add custom header to the request
            context.Request.Headers["Api-Gateway"] = "Signed";

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
