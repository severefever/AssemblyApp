using PluginBase;

namespace NoDependenciesTestApp
{
	public class HelloCommand : ICommand
	{
		public string Name { get => "Hello!"; }
		public string Description { get => "There is no any specific packages..."; }

		public int Execute()
		{
			Console.WriteLine($"Name: {Name}\t Description: {Description}");
			return 0;
		}
	}
}