using Microsoft.AspNetCore.Mvc;
using WebApiForLabs.Stuff;

namespace WebApiForLabs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MainController : ControllerBase
	{
		private readonly IWebHostEnvironment _environment;

		public MainController(IWebHostEnvironment environment)
		{
			_environment = environment;
		}

		[HttpPost("upload")]
		public async Task<ActionResult<string>> UploadFile(
			[AllowedExtensions([".txt"])] IFormFile file)
		{
			var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
			if (!Directory.Exists(uploadsFolder))
				Directory.CreateDirectory(uploadsFolder);

			var filePath = Path.Combine(uploadsFolder, file.FileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			return Ok(new { text = filePath });
		}

		[HttpPost("post-null")]
		public ActionResult<string> PostNull([FromBody] object? payload)
		{
			Console.WriteLine("qwerty");
			Console.WriteLine(payload);
			return Ok(new { text = "qwerty" });
		}
	}
}
