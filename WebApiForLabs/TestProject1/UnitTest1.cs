using NSubstitute;
using PatternLaba.ComandFactory;
using PatternLaba.ComandFactory.Comands;

namespace TestProject1
{
	public class CommandFactoryTests
	{
		private IEnumerable<ICommand> _commands;
		private CommandFactory _factory;

		[SetUp]
		public void Setup()
		{
			// Создаем моки для всех команд
			var commandA = Substitute.For<CommandA>();
			var commandB = Substitute.For<CommandB>();
			var commandC = Substitute.For<CommandC>();
			var commandD = Substitute.For<CommandD>();

			// Создаем список команд
			_commands = new List<ICommand> { commandA, commandB, commandC, commandD };

			// Создаем экземпляр CommandFactory с моками команд
			_factory = new CommandFactory(_commands);
		}

		[Test]
		public void GetCommand_ReturnsCommandA_WhenCondition1IsTrue()
		{
			var downloadRequest = new DownloadRequest { Condition1 = true };

			var command = _factory.GetCommand(downloadRequest);

			Assert.IsInstanceOf<CommandA>(command);
		}

		[Test]
		public void GetCommand_ReturnsCommandB_WhenCondition2IsTrue()
		{
			var downloadRequest = new DownloadRequest { Condition2 = true };

			var command = _factory.GetCommand(downloadRequest);

			Assert.IsInstanceOf<CommandB>(command);
		}

		[Test]
		public void GetCommand_ReturnsCommandC_WhenCondition3IsTrue()
		{
			var downloadRequest = new DownloadRequest { Condition3 = true };

			var command = _factory.GetCommand(downloadRequest);

			Assert.IsInstanceOf<CommandC>(command);
		}

		[Test]
		public void GetCommand_ReturnsCommandD_WhenCondition4IsTrue()
		{
			var downloadRequest = new DownloadRequest { Condition4 = true };

			var command = _factory.GetCommand(downloadRequest);

			Assert.IsInstanceOf<CommandD>(command);
		}

		[Test]
		public void GetCommand_ThrowsException_WhenNoConditionIsTrue()
		{
			var downloadRequest = new DownloadRequest();

			Assert.Throws<ArgumentException>(() => _factory.GetCommand(downloadRequest));
		}

		[Test]
		public void GetCommand_ThrowsException_WhenCondition1AndCondition2AreTrue()
		{
			var downloadRequest = new DownloadRequest { Condition1 = true, Condition2 = true };

			Assert.Throws<ArgumentException>(() => _factory.GetCommand(downloadRequest));
		}

		[Test]
		public void GetCommand_ThrowsException_WhenCondition1AndCondition3AreTrue()
		{
			var downloadRequest = new DownloadRequest { Condition1 = true, Condition3 = true };

			Assert.Throws<ArgumentException>(() => _factory.GetCommand(downloadRequest));
		}

		[Test]
		public void GetCommand_ThrowsException_WhenCondition2AndCondition4AreTrue()
		{
			var downloadRequest = new DownloadRequest { Condition2 = true, Condition4 = true };

			Assert.Throws<ArgumentException>(() => _factory.GetCommand(downloadRequest));
		}

		[Test]
		public void GetCommand_ThrowsException_WhenAllConditionsAreFalse()
		{
			var downloadRequest = new DownloadRequest { Condition1 = false, Condition2 = false, Condition3 = false, Condition4 = false };

			Assert.Throws<ArgumentException>(() => _factory.GetCommand(downloadRequest));
		}

		[Test]
		public void GetCommand_ThrowsException_WhenDownloadRequestIsNull()
		{
			DownloadRequest downloadRequest = null;

			Assert.Throws<ArgumentNullException>(() => _factory.GetCommand(downloadRequest));
		}
	}
}