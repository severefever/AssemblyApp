using PluginBase;
using System.Reflection;
using System.Runtime.Loader;
using Newtonsoft.Json;

namespace AssemblyApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try
			{
				string json = JsonConvert.SerializeObject("testPlugin");
				//	@"NoDependenciesTestApp\bin\Debug\net7.0\NoDependenciesTestApp.dll"
				//	@"JsonTestApp\bin\Debug\net7.0\JsonTestApp.dll"
				//	@"OldJsonTestApp\bin\Debug\net7.0\OldJsonTestApp.dll"

				LoadInterface testPlugin = new LoadInterface(@"OldJsonTestApp\bin\Debug\net7.0\OldJsonTestApp.dll");

				//Console.WriteLine("Assemblies in domain:");
				//foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
				//	Console.WriteLine(asm.GetName().Name);
				//Console.WriteLine();

				Console.WriteLine("Commands: ");
				foreach (var command in testPlugin.CreateInstances<ICommand>())
				{
					command.Execute();
				}

				testPlugin.Unload();
				for (int i = 0; i < 10; i++)
				{
					GC.Collect();
					GC.WaitForPendingFinalizers();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}