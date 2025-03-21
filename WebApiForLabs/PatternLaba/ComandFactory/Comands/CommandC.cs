namespace PatternLaba.ComandFactory.Comands
{
	public class CommandC : ICommand
	{
		public async Task<T> ExecuteAsync<T>(Guid clientIdentifier, DownloadRequest downloadRequest)
		{
			Console.WriteLine("Executing Command C");
			return await Task.FromResult((T)(object)"Result from Command C");
		}
	}
}
