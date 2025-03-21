using Microsoft.AspNetCore.SignalR;

namespace WebApiForLabs.Services
{
	public class SignalHub2 : Hub
	{
		public async Task SendMessage(string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", message);
		}
	}
}
