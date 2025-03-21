namespace PatternLaba.ComandFactory.Comands
{
	public class CommandB : ICommand
	{
		public async Task<T> ExecuteAsync<T>(Guid clientIdentifier, DownloadRequest downloadRequest)
		{
			Console.WriteLine("Executing Command B");
			return await Task.FromResult((T)(object)"Result from Command B");
		}
	}
}
