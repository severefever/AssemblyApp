using PluginBase;
using Newtonsoft.Json;

namespace JsonTestApp
{
	public class JsonCommand : ICommand
	{
		public string Name { get => "Newest version of Json"; }
		public string Description { get => GetVersion(); }
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
		private string? GetVersion()
		{
			foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (asm.GetName().Name == "Newtonsoft.Json")
					return asm.GetName().Version.ToString();
			}
			return null;
		}
	}

	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
	}
}
