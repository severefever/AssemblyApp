using PluginBase;
using Newtonsoft.Json;

namespace JsonTestApp
{
	public class JsonCommand : ICommand
	{
		public string Name { get => "Newest version of Json"; }
		public string Description { get => "Version is 1.0.2"; }
		public int Execute()
		{
			Console.WriteLine($"Name: {Name}\t Description: {Description}");
			return 0;
		}
	}
}
