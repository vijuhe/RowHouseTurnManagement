using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace UI.MobileAppService
{
    public class ApiKeyValidator
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyValidator(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            const string apiKeyKey = "ApiKey";
            if (context.Request.Headers.ContainsKey(apiKeyKey) &&
                context.Request.Headers[apiKeyKey].ToString().Equals(_configuration[apiKeyKey], StringComparison.OrdinalIgnoreCase))
            {
                await _next.Invoke(context);
                return;
            }
            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
        }
    }
}
