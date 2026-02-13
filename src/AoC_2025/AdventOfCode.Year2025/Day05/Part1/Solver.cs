using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day05.Part1;

public class Solver : BaseResolver, IAdventOfCode
{
	private int _result = 0;
	private List<(long, long)> _ranges;

	public override string Solve(string input)
	{
		_ranges = new List<(long, long)>();
		var lines = input.Split('\n');

		Process(lines);

		return _result.ToString();
	}

	private void Process(string[] lines)
	{
		var indexOfEmptyLine = 0;
		for (int i = 0; i < lines.Length; ++i)
		{
			if (lines[i].Contains('-'))
				ProcessRange(lines[i]);

			if (string.IsNullOrWhiteSpace(lines[i]))
			{
				indexOfEmptyLine = i;
				break;
			}
		}

		for (int i = indexOfEmptyLine + 1; i < lines.Length; ++i)
			ProcessNumber(long.Parse(lines[i]));
	}

	private void ProcessRange(string line)
	{
		var indexOfRangeSign = line.IndexOf('-');
		var startNumber = long.Parse(line[0..indexOfRangeSign]);
		var endNumber = long.Parse(line[(indexOfRangeSign + 1)..]);

		_ranges.Add((startNumber, endNumber));
	}

	private void ProcessNumber(long number)
	{
		foreach (var range in _ranges)
		{
			if (number >= range.Item1 && number <= range.Item2)
			{
				_result++;
				break;
			}
		}
	}
}
