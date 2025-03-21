using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiForLabs.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MathController : ControllerBase
	{
		[HttpGet("add")]
		public ActionResult<int> Add()
		{
			//throw new NotImplementedException("No realization");
			int x = 0;
			int y = 8 / x;
			return Ok(y);
		}
	}

	[ApiController]
	[Route("api/[controller]")]
	public class WeatherController : ControllerBase
	{
		[HttpGet("forecast")]
		public async Task<ActionResult> GetForecast()
		{
			//throw new ArgumentNullException("Divisor cannot be zero");

			string[] strings = null;
			return Ok(strings.Length);
		}
	}

	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		[HttpGet("info")]
		public async Task<ActionResult> GetUserInfo()
		{
			try
			{
				throw new UnauthorizedAccessException("Access denied");
			}
			catch (Exception ex)
			{
				throw new UnauthorizedAccessException("Access denied");
			}
		}
	}
}
