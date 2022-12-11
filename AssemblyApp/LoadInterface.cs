using PluginBase;
using System.Reflection;

namespace AssemblyApp
{
	public class LoadInterface<T>
	{
		public string[] PluginsPaths { get; set; }
		public LoadInterface(string[] pluginsPaths)
		{
			PluginsPaths = pluginsPaths;
		}
		public IEnumerable<T> GetObjects() 
		{
			IEnumerable<T> commands = PluginsPaths.SelectMany(pluginPath =>
			{
				Assembly pluginAssembly = LoadPlugin(pluginPath);
				return CreateInstances(pluginAssembly);
			}).ToList();
			return commands;
		}
		private Assembly LoadPlugin(string relativePath)
		{
			// Navigate up to the solution root
			string root = Path.GetFullPath(Path.Combine(
				Path.GetDirectoryName(
					Path.GetDirectoryName(
						Path.GetDirectoryName(
							Path.GetDirectoryName(
								Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

			string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
			PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
			return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
		}
		private IEnumerable<T> CreateInstances(Assembly assembly)
		{
			int count = 0;
			foreach (Type type in assembly.GetTypes())
			{
				if (typeof(T).IsAssignableFrom(type))
				{
					T? result = (T?)Activator.CreateInstance(type);
					if (result != null)
					{
						count++;
						yield return result;
					}
				}
			}
			if (count == 0)
			{
				string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
				throw new ApplicationException(
					$"Can't find any type which implements ICommand in {assembly} from {assembly.Location}.\n" +
					$"Available types: {availableTypes}");
			}
		}
	}
}
