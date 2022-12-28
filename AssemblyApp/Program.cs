using PluginBase;
using System.Reflection;
using Newtonsoft.Json;

namespace AssemblyApp
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				//string json = JsonConvert.SerializeObject("testPlugin");

				Console.WriteLine("Assemblies in domain before loading a plugin:");
				foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
				{
					if (asm.GetName().Name.StartsWith("Newtonsoft"))
						Console.WriteLine(asm.GetName());
				}
				Console.WriteLine();

				//	@"NoDependenciesTestApp\bin\Debug\net7.0\NoDependenciesTestApp.dll"
				//	@"JsonTestApp\bin\Debug\net7.0\JsonTestApp.dll"
				//	@"OldJsonTestApp\bin\Debug\net7.0\OldJsonTestApp.dll"

				LoadInterface secondTestPlugin = new LoadInterface(@"JsonTestApp\bin\Debug\net7.0\JsonTestApp.dll");
				LoadInterface testPlugin = new LoadInterface(@"OldJsonTestApp\bin\Debug\net7.0\OldJsonTestApp.dll");

				Console.WriteLine("Commands: ");
				foreach (var command in testPlugin.CreateInstances<ICommand>())
				{
					command.Execute();
				}
				foreach (var command in secondTestPlugin.CreateInstances<ICommand>())
				{
					command.Execute();
				}
				Console.WriteLine();

				//testPlugin.Unload();
				//for (int i = 0; i < 10; i++)
				//{
				//	GC.Collect();
				//	GC.WaitForPendingFinalizers();
				//}

				//Console.WriteLine("Commands: ");
				//foreach (var command in testPlugin.CreateInstances<ICommand>())
				//{
				//	command.Execute();
				//}
				//Console.WriteLine();

				Console.WriteLine("Assemblies in domain after loading a plugin:");
				foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
				{
					if (asm.GetName().Name.StartsWith("Newtonsoft"))
						Console.WriteLine(asm.GetName());
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

	}
}