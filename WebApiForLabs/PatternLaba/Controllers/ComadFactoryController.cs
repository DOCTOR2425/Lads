using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatternLaba.ComandFactory;

namespace PatternLaba.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ComadFactoryController : ControllerBase
	{
		private readonly ICommandFactory _commandFactory;

		public ComadFactoryController(ICommandFactory commandFactory)
		{
			_commandFactory = commandFactory;
		}

		[HttpPost("commandA")]
		public async Task<IActionResult> ExecuteCommandA([FromBody] DownloadRequest request)
		{
			request.Condition1 = true;
			request.Condition2 = false;
			request.Condition3 = false;
			request.Condition4 = false;

			var command = _commandFactory.GetCommand(request);
			var result = await command.ExecuteAsync<object>(Guid.NewGuid(), request);
			return Ok(result);
		}

		[HttpPost("commandB")]
		public async Task<IActionResult> ExecuteCommandB([FromBody] DownloadRequest request)
		{
			request.Condition1 = false;
			request.Condition2 = true;
			request.Condition3 = false;
			request.Condition4 = false;

			var command = _commandFactory.GetCommand(request);
			var result = await command.ExecuteAsync<object>(Guid.NewGuid(), request);
			return Ok(result);
		}

		[HttpPost("commandC")]
		public async Task<IActionResult> ExecuteCommandC([FromBody] DownloadRequest request)
		{
			request.Condition1 = false;
			request.Condition2 = false;
			request.Condition3 = true;
			request.Condition4 = false;

			var command = _commandFactory.GetCommand(request);
			var result = await command.ExecuteAsync<object>(Guid.NewGuid(), request);
			return Ok(result);
		}

		[HttpPost("commandD")]
		public async Task<IActionResult> ExecuteCommandD([FromBody] DownloadRequest request)
		{
			request.Condition1 = false;
			request.Condition2 = false;
			request.Condition3 = false;
			request.Condition4 = true;

			var command = _commandFactory.GetCommand(request);
			var result = await command.ExecuteAsync<object>(Guid.NewGuid(), request);
			return Ok(result);
		}
	}
}
