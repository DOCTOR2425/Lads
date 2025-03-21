using WebApiForLabs.DataBase;
using WebApiForLabs.MiddleWares;
using WebApiForLabs.Services;

namespace WebApiForLabs
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers(options =>
			{
				options.Filters.Add<ExceptionFilter>();
			});

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<MyDBContext>();

			builder.Services.AddSignalR();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCookiePolicy(new CookiePolicyOptions
			{
				MinimumSameSitePolicy = SameSiteMode.Lax,
				//HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
				//Secure = CookieSecurePolicy.Always,
			});

			app.UseCors(x =>
			{
				x.AllowAnyOrigin();
				x.AllowAnyMethod();
				x.AllowAnyHeader();

				x.WithOrigins("https://localhost:4200")
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials();

				x.WithOrigins("http://localhost:4200")
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials();
			});

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();

			app.MapHub<SignalHub>("/chatHub");
			app.MapHub<SignalHub2>("/chatHub2");
			//app.UseMiddleware<NoCacheMiddleWare>();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
