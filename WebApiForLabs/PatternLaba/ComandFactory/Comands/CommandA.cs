namespace PatternLaba.ComandFactory.Comands
{
	public class CommandA : ICommand
	{
		public async Task<T> ExecuteAsync<T>(Guid clientIdentifier, DownloadRequest downloadRequest)
		{
			Console.WriteLine("Executing Command A");
			return await Task.FromResult((T)(object)"Result from Command A");
		}
	}
}
