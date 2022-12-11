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
			Person person = new Person();
			person.Name = "Some Person";
			person.Age = 44;
			person.Id = 1;
			string json = JsonConvert.SerializeObject(person);
			Console.WriteLine($"Name: {Name}\t Description: {Description}");
			Console.WriteLine(json);
			return 0;
		}
	}

	public class AnotherJsonCommand : ICommand
	{
		public string Name { get => "Another older version of Json"; }
		public string Description { get => "Version is 1.0.1"; }
		public int Execute()
		{
			Console.WriteLine($"Name: {Name}\t Description: {Description}");
			return 0;
		}
	}

	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
	}
}