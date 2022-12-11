using System.Reflection;
using System.Runtime.Loader;

namespace AssemblyApp
{
	public class PluginLoadContext : AssemblyLoadContext
	{
		private AssemblyDependencyResolver _resolver;
		public PluginLoadContext(string pluginPath)
		{
			_resolver = new AssemblyDependencyResolver(pluginPath);
		}
		// The Load method override causes all the dependencies present in the plugin's binary directory to get loaded
		// into the PluginLoadContext together with the plugin assembly itself.
		// NOTE: The Interface assembly must not be present in the plugin's binary directory, otherwise we would
		// end up with the assembly being loaded twice. Once in the default context and once in the PluginLoadContext.
		// The types present on the host and plugin side would then not match even though they would have the same names.
		// https://learn.microsoft.com/en-us/dotnet/standard/assembly/unloadability
		// https://github.com/dotnet/samples/tree/main/core/tutorials/Unloading
		protected override Assembly? Load(AssemblyName assemblyName)
		{
			string? assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
			if (assemblyPath != null)
			{
				return LoadFromAssemblyPath(assemblyPath);
			}
			return null;
		}
		protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
		{
			string? libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
			if (libraryPath != null)
			{
				return LoadUnmanagedDllFromPath(libraryPath);
			}
			return IntPtr.Zero;
		}

	}
}
