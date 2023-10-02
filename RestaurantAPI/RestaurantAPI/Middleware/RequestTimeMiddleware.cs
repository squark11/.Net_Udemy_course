using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private Stopwatch _stopWatch;
        private readonly ILogger<RequestTimeMiddleware> _logger;

        public RequestTimeMiddleware( ILogger<RequestTimeMiddleware> logger)
        {
            _stopWatch = new Stopwatch();
            _logger = logger;
        }

        public ILogger<RequestTimeMiddleware> Logger { get; }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch.Stop();

            var ElapsedMilliseconds = _stopWatch.ElapsedMilliseconds;

            if(ElapsedMilliseconds/1000 > 4) 
            { 
                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {ElapsedMilliseconds}ms";

                _logger.LogInformation(message);
            }
        }
    }
}
