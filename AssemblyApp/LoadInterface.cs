using System;
using PluginBase;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyApp
{
	public class LoadInterface
	{
		private PluginLoadContext _plugin;
		public Assembly Assembly { get; private set; }
		public string PluginAbsolutePath { get; private set; }
		public LoadInterface(string pluginPath)
		{
			PluginAbsolutePath = GetFullPath(pluginPath);
			_plugin = new PluginLoadContext(PluginAbsolutePath);
			Assembly = _plugin.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(PluginAbsolutePath)));
		}
		public IEnumerable<T> CreateInstances<T>()
		{
			int count = 0;
			foreach (Type type in Assembly.GetTypes())
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
				string availableTypes = string.Join(",", Assembly.GetTypes().Select(t => t.FullName));
				throw new ApplicationException(
					$"Не найдено типа, который реализует данный интерфейс в {Assembly} из {Assembly.Location}.\n" +
					$"Доступные типы: {availableTypes}");
			}
		}
		public void Unload()
		{
			_plugin.Unload();
		}
		private static string GetFullPath(string relativePath)
		{
			// Navigate up to the solution root
			string root = Path.GetFullPath(Path.Combine(
				Path.GetDirectoryName(
					Path.GetDirectoryName(
						Path.GetDirectoryName(
							Path.GetDirectoryName(
								Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

			return Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
		}
	}
}
