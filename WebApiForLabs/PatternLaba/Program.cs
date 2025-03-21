
using PatternLaba.ComandFactory;
using PatternLaba.ComandFactory.Comands;

namespace PatternLaba
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			AddCommands(builder.Services);


			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}

		private static void AddCommands(IServiceCollection services)
		{
			services.AddScoped<ICommand, CommandA>();
			services.AddScoped<ICommand, CommandB>();
			services.AddScoped<ICommand, CommandC>();
			services.AddScoped<ICommand, CommandD>();
			services.AddScoped<ICommandFactory, CommandFactory>();
		}
	}
}
