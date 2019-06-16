using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ServiceHost.API.Middlewares
{
    public sealed class GlobalExceptionMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        /// <summary>
        /// Log for GlobalExceptionMiddleware
        /// </summary>
        /// <param name="next">RequestDelage</param>
        /// <param name="logger">ILoggerFactory</param>
        public GlobalExceptionMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger("GlobalExceptionMiddleware");
        }
        /// <summary>
        /// Invoke loger
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns> Task HttpContext</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, ex.Message);
            }
        }
    }
}
