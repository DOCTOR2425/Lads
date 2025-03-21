namespace PatternLaba.ComandFactory.Comands
{
	public class CommandD : ICommand
	{
		public async Task<T> ExecuteAsync<T>(Guid clientIdentifier, DownloadRequest downloadRequest)
		{
			Console.WriteLine("Executing Command D");
			return await Task.FromResult((T)(object)"Result from Command D");
		}
	}
}
