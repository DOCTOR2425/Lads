using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;

namespace WebApiForLabs.Services
{
	public class SignalHub : Hub
	{
		private static ConcurrentDictionary<string, string> _userConnections
			= new ConcurrentDictionary<string, string>();

		public override async Task OnConnectedAsync()
		{
			var userName = Context.GetHttpContext().Request.Query["userName"];
			if (_userConnections.ContainsKey(userName))
			{
				await Clients.Caller.SendAsync("ReceiveMessage", $"Такой пользователь уже есть");
				await OnDisconnectedAsync(new Exception());
				return;
			}

			_userConnections[userName] = Context.ConnectionId;

			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			var userName = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
			if (userName != null)
			{
				_userConnections.TryRemove(userName, out _);
			}


			await base.OnDisconnectedAsync(exception);
		}

		public async Task SendMessage(string recipient, string message)
		{
			if (_userConnections.TryGetValue(recipient, out var recipientConnectionId))
			{
				await Clients.Client(recipientConnectionId).SendAsync("ReceiveMessage", $"" +
					$"sender '{_userConnections
						.FirstOrDefault(x => x.Value == Context.ConnectionId).Key}': {message}");
			}
			else
			{
				await Clients.Caller.SendAsync("ReceiveMessage", $"Пользователь {recipient} не найден.");
			}
		}
	}
}
