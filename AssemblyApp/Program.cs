using PluginBase;
using System.Reflection;
using System.Runtime.Loader;

namespace AssemblyApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try
			{
				//string[] pluginPaths = new string[]
				//{
				//	@"NoDependenciesTestApp\bin\Debug\net7.0\NoDependenciesTestApp.dll",
				//	@"JsonTestApp\bin\Debug\net7.0\JsonTestApp.dll",
				//	@"OldJsonTestApp\bin\Debug\net7.0\OldJsonTestApp.dll",
				//};

				LoadInterface<ICommand> testPlugin = new LoadInterface<ICommand>(@"OldJsonTestApp\bin\Debug\net7.0\OldJsonTestApp.dll");

				Console.WriteLine("Commands: ");
				foreach (var command in testPlugin.CreateInstances())
				{
					command.Execute();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}