using System.Net;
using System.Text;

namespace HealthPadiWebApi.Middlewares
{
    public class GlobalJsonRequestFormatRequirementMiddleware(RequestDelegate next)
    {

        public async Task Invoke(HttpContext context)
        {
            if ((context.Request.Method == "POST" || context.Request.Method == "PUT" || context.Request.Method == "PATCH")
                && (context.Request.ContentType == null || !context.Request.ContentType.StartsWith("application/json")))
            {
                context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    status = "UNPROCESSABLE_ENTITY",
                    message = "Please supply the request data in json format"
                };

                var jsonError = Newtonsoft.Json.JsonConvert.SerializeObject(errorResponse);
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                await context.Response.WriteAsync(jsonError, Encoding.UTF8);

                return;
            }

            await next(context);
        }

    }
}
