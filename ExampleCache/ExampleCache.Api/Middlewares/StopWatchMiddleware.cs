using System.Diagnostics;

namespace ExampleCache.Api.Middlewares;

public class StopWatchMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        Stopwatch sw = Stopwatch.StartNew();
        await next(context);
        long elapsedMilliseconds = sw.ElapsedMilliseconds;
        
        Console.WriteLine($"> {elapsedMilliseconds} ms for Request: {context.Request.Path.Value}");
    }
}