using System.Reflection;

namespace AdventOfCodeManager.Helpers;

internal static class AssemblyLoader
{
	public static void LoadAll()
	{
		var basePath = AppDomain.CurrentDomain.BaseDirectory;

		foreach (var dll in Directory.GetFiles(basePath, "*.dll"))
		{
			try
			{
				Assembly.LoadFrom(dll);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to load assembly from {dll}: {ex.Message}");
			}
		}
	}
}
