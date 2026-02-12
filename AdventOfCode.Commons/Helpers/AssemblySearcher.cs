using System.Text.RegularExpressions;

namespace AdventOfCode.Commons.Helpers;

public class AssemblySearcher
{
	public static int? GetYearFromNamespace(Type type)
	{
		var ns = type.Namespace;
		if (string.IsNullOrWhiteSpace(ns))
			return null;

		foreach (var segment in ns.Split('.'))
		{
			var m = Regex.Match(segment, @"^Year(\d+)$");
			if (m.Success && int.TryParse(m.Groups[1].Value, out var year))
				return year;
		}

		return null;
	}

	public static int? GetDayFromNamespace(Type type)
	{
		var ns = type.Namespace;
		if (string.IsNullOrWhiteSpace(ns))
			return null;
		foreach (var segment in ns.Split('.'))
		{
			var m = Regex.Match(segment, @"^Day(\d+)$");
			if (m.Success && int.TryParse(m.Groups[1].Value, out var day))
				return day;
		}
		return null;
	}

	public static int? GetPartFromNamespace(Type type)
	{
		var ns = type.Namespace;
		if (string.IsNullOrWhiteSpace(ns))
			return null;
		foreach (var segment in ns.Split('.'))
		{
			var m = Regex.Match(segment, @"^Part(\d+)$");
			if (m.Success && int.TryParse(m.Groups[1].Value, out var part))
				return part;
		}
		return null;
	}
}
