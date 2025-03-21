using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiForLabs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PrestigeBankController : ControllerBase
	{
		[HttpGet("get-data")]
		public async Task<ActionResult<string>> GetData()
		{
			return Ok(new { text = "HttpClientModule" });
		}

		[HttpGet("service-worker")]
		public async Task<ActionResult<string>> ServiceWorker()
		{
			return Ok(new { date = DateTime.Now.ToString() });
		}
	}
}
