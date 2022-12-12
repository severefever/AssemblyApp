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

				LoadInterface testPlugin = new LoadInterface(@"OldJsonTestApp\bin\Debug\net7.0\OldJsonTestApp.dll");

				Console.WriteLine("Commands: ");
				foreach (var command in testPlugin.CreateInstances<ICommand>())
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