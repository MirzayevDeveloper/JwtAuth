using System.Net;
using Newtonsoft.Json;
using Serilog;

namespace Auth.Api.GlobalExceptionHandler
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An unhandled exception occurred.");

				httpContext.Response.StatusCode = 404;
				httpContext.Response.ContentType = "application/json";

				var errorResponse = new
				{
					StatusCode = httpContext.Response.StatusCode,
					Message = "An error occurred while processing your request."
				};

				var json = JsonConvert.SerializeObject(errorResponse);

				await httpContext.Response.WriteAsync(json);
			}
		}
	}

	public static class ExceptionHandlerMiddlewareExtensions
	{
		public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ExceptionHandlerMiddleware>();
		}
	}
}
