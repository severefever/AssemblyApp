using System.Reflection;

namespace AssemblyApp
{
	public class LoadInterface
	{
		private PluginLoadContext _plugin;
		private Assembly _assembly;
		public string PluginAbsolutePath { get; private set; }
		public LoadInterface(string pluginPath)
		{
			// Navigate up to the solution root
			string root = Path.GetFullPath(Path.Combine(
				Path.GetDirectoryName(
					Path.GetDirectoryName(
						Path.GetDirectoryName(
							Path.GetDirectoryName(
								Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

			PluginAbsolutePath = Path.GetFullPath(Path.Combine(root, pluginPath.Replace('\\', Path.DirectorySeparatorChar)));
			_plugin = new PluginLoadContext(PluginAbsolutePath);
			_assembly = _plugin.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(PluginAbsolutePath)));
		}
		public IEnumerable<T> CreateInstances<T>()
		{
			int count = 0;
			foreach (Type type in _assembly.GetTypes())
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
				string availableTypes = string.Join(",", _assembly.GetTypes().Select(t => t.FullName));
				throw new ApplicationException(
					$"Can't find any type which implements ICommand in {_assembly} from {_assembly.Location}.\n" +
					$"Available types: {availableTypes}");
			}
		}

	}
}
