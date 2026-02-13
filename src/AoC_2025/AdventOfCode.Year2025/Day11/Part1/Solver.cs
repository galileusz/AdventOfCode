using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day11.Part1;

public class Solver : BaseResolver, IAdventOfCode
{
	private long _result = 0;
	private Dictionary<string, List<string>> _gatewaysDictionary = new Dictionary<string, List<string>>();

	public override string Solve(string input)
	{
		var lines = input.Split("\n");

		Process(lines);

		return _result.ToString();
	}

	private void Process(string[] lines)
	{
		_gatewaysDictionary = new Dictionary<string, List<string>>();

		foreach (var line in lines)
			AddDictionaryItem(line);

		GoTo("you");
	}

	private void AddDictionaryItem(string line)
	{
		var parts = line.Split(' ');
		_gatewaysDictionary.Add(parts[0][..^1], [.. parts.Skip(1)]);
	}

	private void GoTo(string gateway)
	{
		if (_gatewaysDictionary.TryGetValue(gateway, out List<string> paths))
		{
			foreach (var path in paths)
			{
				if (path == "out")
					_result++;
				else
					GoTo(path);
			}
		}
	}
}