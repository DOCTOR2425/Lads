namespace PatternLaba.ComandFactory
{
	public interface ICommand
	{
		Task<T> ExecuteAsync<T>(Guid clientIdentifier, DownloadRequest downloadRequest);
	}

	public interface ICommandFactory
	{
		ICommand GetCommand(DownloadRequest downloadRequest);
	}
}
