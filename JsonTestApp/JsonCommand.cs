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

	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
	}
}
