namespace WebApiForLabs.MiddleWares
{
	public class NoCacheMiddleWare
	{
		private readonly RequestDelegate _next;

		public NoCacheMiddleWare(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			// его достаточно
			context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, private";
			context.Response.Headers["Pragma"] = "no-cache";//http 1.1
			context.Response.Headers["Expires"] = "0";//rfc http1.1

			await _next(context);
		}
	}
}
