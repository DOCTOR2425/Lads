using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiForLabs.DataBase;
using WebApiForLabs.DataBase.Models;

namespace WebApiForLabs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EFCoreController : ControllerBase
	{
		private readonly MyDBContext _db;

		public EFCoreController(MyDBContext myDBContext)
		{
			_db = myDBContext;
		}

		[HttpGet("insert")]
		public async Task<ActionResult> InsertAll()
		{
			_db.Table1.RemoveRange(_db.Table1);
			_db.Table2.RemoveRange(_db.Table2);
			_db.SaveChanges();

			Table1 table1;
			Table2 table2;
			for (int i = 0; i < 100; i++)
			{
				table1 = new Table1()
				{
					Name = (i % 2) + "Table1 - " + i
				};
				_db.Table1.Add(table1);
				_db.SaveChanges();
				table2 = new Table2()
				{
					Name = (i % 2) + "Table2 - " + i,
					Table1 = new List<Table1>() { table1 }
				};
				_db.Table2.Add(table2);
				_db.SaveChanges();
			}

			return Ok();
		}

		[HttpGet("eager-oading")]
		public async Task<ActionResult> GetEager()
		{
			var result = await _db.Table2
				.Include(t => t.Table1)
				.ToListAsync();

			foreach (var table2 in result)
			{
				var table1 = table2.Table1;
				Console.WriteLine($"Table2 ID: {table2.Id}, Table1 Count: {table1.Count}");
			}

			return Ok(result);
		}

		[HttpGet("lazy-loading")]
		public async Task<ActionResult> GetLazy()
		{
			var result = await _db.Table2.ToListAsync();

			foreach (var table2 in result)
			{
				var table1 = table2.Table1;
				Console.WriteLine($"Table2 ID: {table2.Id}, Table1 Count: {table1.Count}");
			}

			return Ok(result);
		}

		[HttpGet("explicit-loading")]
		public async Task<ActionResult> GetExplicit()
		{
			var result = await _db.Table2.ToListAsync();

			foreach (var table2 in result)
			{
				_db.Entry(table2).Collection(t2 => t2.Table1).Load();
				Console.WriteLine($"Table2 ID: {table2.Id}, Table1 Count: {table2.Table1.Count}");
			}

			return Ok(result);
		}

		[HttpGet("order-by")]
		public async Task<ActionResult> GetFiltered()
		{
			var result = _db.Table2
				.OrderBy(t2 => t2.Name)
				.ThenBy(t2 => t2.Id)
				.ToList();

			return Ok(result);
		}

		[HttpGet("dynamic-query-build")]
		public async Task<ActionResult> GetDynamic(string filter, bool sortAscending)
		{
			var query = _db.Table2.AsQueryable();

			if (!string.IsNullOrEmpty(filter))
			{
				query = query.Where(t2 => t2.Name.Contains(filter));
			}

			query = sortAscending
				? query.OrderBy(t2 => t2.Name)
				: query.OrderByDescending(t2 => t2.Name);

			var result = await query.ToListAsync();

			return Ok(result);
		}

		[HttpGet("get-with-delayed-execution-iqueryable")]
		public async Task<ActionResult> GetWithDelayedExecutionIQueryable()
		{
			var query = _db.Table2
				.Where(t2 => t2.Name.Contains("insert after buid query"))
				.OrderByDescending(t2 => t2.Id);

			_db.Table2.Add(new Table2()
			{
				Name = "insert after buid query",
				Table1 = new List<Table1>()
				{
					new Table1() { Name = "insert after buid query" }
				}
			});
			_db.SaveChanges();

			var result = await query.ToListAsync();

			return Ok(result);
		}

		[HttpGet("get-with-delayed-execution-ienumerable")]
		public async Task<ActionResult> GetWithDelayedExecutionIEnumerable()
		{
			var query = _db.Table2
				.AsEnumerable()
				.Where(t2 => t2.Name.Contains("insert after buid query"))
				.OrderByDescending(t2 => t2.Id);

			_db.Table2.Add(new Table2()
			{
				Name = "insert after buid query",
				Table1 = new List<Table1>()
				{
					new Table1() { Name = "insert after buid query" }
				}
			});
			_db.SaveChanges();

			var result = query.ToList();

			return Ok(result);
		}

		[HttpGet("get-with-transformation")]
		public async Task<ActionResult> GetWithTransformation()
		{
			var result = _db.Table2
				.Where(t2 => t2.Name.Contains("1"))
				.OrderBy(t2 => t2.Name)
				.ToList()
				.Where(t2 => t2.Id > 10);

			return Ok(result);
		}

		[HttpGet("order-by-to-object")]
		public ActionResult GetFilteredToObject()
		{
			var table2List = new List<Table2>
			{
				new Table2 { Id = 2, Name = "Table2-2" },
				new Table2
				{
					Id = 1, Name = "Table2-1", Table1 =
					new List<Table1>()
					{ new Table1() { Id = 1, Name = "Table1-2" } }
				}
			};

			var result = table2List
				.OrderBy(t2 => t2.Name)
				.ThenBy(t2 => t2.Id);

			return Ok(result.ToList());
		}

		[HttpGet("dynamic-query-build-to-object")]
		public ActionResult GetDynamicToObject(string filter, bool sortAscending)
		{
			var table2List = new List<Table2>
			{
				new Table2 { Id = 1, Name = "Table2-1" },
				new Table2 { Id = 2, Name = "Table2-2" }
			};

			var query = table2List.AsQueryable();

			if (!string.IsNullOrEmpty(filter))
			{
				query = query.Where(t2 => t2.Name.Contains(filter));
			}

			query = sortAscending
				? query.OrderBy(t2 => t2.Name)
				: query.OrderByDescending(t2 => t2.Name);

			var result = query.ToList();

			return Ok(result);
		}

		[HttpGet("get-with-delayed-execution-iqueryable-to-object")]
		public ActionResult GetWithDelayedExecutionIQueryableToObject()
		{
			var table2List = new List<Table2>
			{
				new Table2 { Id = 1, Name = "Table2-1" },
				new Table2 { Id = 2, Name = "Table2-2" }
			};

			var query = table2List
				.AsQueryable()
				.Where(t2 => t2.Name.Contains("insert after build query"))
				.OrderByDescending(t2 => t2.Id);

			table2List.Add(new Table2
			{
				Id = 3,
				Name = "insert after build query" + DateTime.Now.Millisecond,
				Table1 = new List<Table1> { new Table1 { Id = 3, Name = "Table1-3" } }
			});

			var result = query.ToList();

			return Ok(result);
		}

		[HttpGet("get-with-delayed-execution-ienumerable-to-object")]
		public ActionResult GetWithDelayedExecutionIEnumerableToObject()
		{
			var table2List = new List<Table2>
			{
				new Table2 { Id = 1, Name = "Table2-1" },
				new Table2 { Id = 2, Name = "Table2-2" }
			};

			var query = table2List
				.AsEnumerable()
				.Where(t2 => t2.Name.Contains("insert after build query"))
				.OrderByDescending(t2 => t2.Id);

			table2List.Add(new Table2
			{
				Id = 3,
				Name = "insert after build query" + DateTime.Now.Millisecond,
				Table1 = new List<Table1> { new Table1 { Id = 3, Name = "Table1-3" } }
			});

			var result = query.ToList();

			return Ok(result);
		}

		[HttpGet("get-with-transformation-to-object")]
		public ActionResult GetWithTransformationToObject()
		{
			var table2List = new List<Table2>
			{
				new Table2 { Id = 1, Name = "Table2-1" },
				new Table2 { Id = 2, Name = "Table2-2" },
				new Table2 { Id = 11, Name = "Table2-11" }
			};

			var result = table2List
				.Where(t2 => t2.Name.Contains("1"))
				.OrderBy(t2 => t2.Name)
				.ToList()
				.Where(t2 => t2.Id > 10);

			return Ok(result);
		}
	}
}
