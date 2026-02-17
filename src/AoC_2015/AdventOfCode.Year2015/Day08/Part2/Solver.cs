using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day08.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var span = input.AsSpan().Trim();
		var lineRanges = span.Split('\n');
		var result = 0;
		foreach ( var range in lineRanges )
		{
			var line = span[range];

			result += ProcessLine(line);
		}

		return result.ToString();
	}

	private int ProcessLine(ReadOnlySpan<char> line)
	{
		var length = line.Length;
		var result = 2;
		foreach (var c in line)
		{
			if (c == '\\' || c == '\"')
			{
				result += 2;
			}
			else
			{
				result++;
			}
		}

		return result - length;
	}
}
