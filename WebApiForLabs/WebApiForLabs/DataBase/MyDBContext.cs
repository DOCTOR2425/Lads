using Microsoft.EntityFrameworkCore;
using WebApiForLabs.DataBase.Models;

namespace WebApiForLabs.DataBase
{
	public class MyDBContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				.UseLazyLoadingProxies()//Lazi load
				.UseSqlServer(@$"Server=DESKTOP-0MK8KC9;Database=MyDB;
					Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;")
			.LogTo(Console.WriteLine, LogLevel.Information);
		}

		public required DbSet<Table1> Table1 { get; set; }
		public required DbSet<Table2> Table2 { get; set; }
	}
}
