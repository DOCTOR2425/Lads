using PatternLaba.ComandFactory.Comands;

namespace PatternLaba.ComandFactory
{
	public class CommandFactory : ICommandFactory
	{
		private readonly IEnumerable<ICommand> _commands;

		public CommandFactory(IEnumerable<ICommand> commands)
		{
			_commands = commands;
		}

		public ICommand GetCommand(DownloadRequest downloadRequest)
		{
			if (downloadRequest == null)
			{
				throw new ArgumentNullException(nameof(downloadRequest), "DownloadRequest cannot be null");
			}

			if (downloadRequest.Condition1)
			{
				if (downloadRequest.Condition2 || downloadRequest.Condition3 || downloadRequest.Condition4)
				{
					throw new ArgumentException("Only one condition can be true");
				}
				return _commands.OfType<CommandA>().FirstOrDefault();
			}
			else if (downloadRequest.Condition2)
			{
				if (downloadRequest.Condition1 || downloadRequest.Condition3 || downloadRequest.Condition4)
				{
					throw new ArgumentException("Only one condition can be true");
				}
				return _commands.OfType<CommandB>().FirstOrDefault();
			}
			else if (downloadRequest.Condition3)
			{
				if (downloadRequest.Condition1 || downloadRequest.Condition2 || downloadRequest.Condition4)
				{
					throw new ArgumentException("Only one condition can be true");
				}
				return _commands.OfType<CommandC>().FirstOrDefault();
			}
			else if (downloadRequest.Condition4)
			{
				if (downloadRequest.Condition1 || downloadRequest.Condition2 || downloadRequest.Condition3)
				{
					throw new ArgumentException("Only one condition can be true");
				}
				return _commands.OfType<CommandD>().FirstOrDefault();
			}

			throw new ArgumentException("No valid condition found for the given request");
		}
	}
}
