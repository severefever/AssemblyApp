using PluginBase;
using Newtonsoft.Json;

namespace OldJsonTestApp
{
	public class OldJsonCommand : ICommand
	{
		public string Name { get => "Older version of Json"; }
		public string Description { get => "Version is 1.0.1"; }
		public int Execute()
		{
			Console.WriteLine($"Name: {Name}\t Description: {Description}");
			return 0;
		}
	}
}