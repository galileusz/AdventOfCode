using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day01.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	private static readonly Dictionary<char, int> _directions = new()
	{
		{ '(', 1 },
		{ ')', -1 }
	};

	public override string Solve(string input)
	{
		var count = 0;
		var result = 0;

		foreach (var c in input.AsSpan().Trim())
		{
			count++;
			result += _directions[c];
			if (result < 0)
				break;
		}

		return count.ToString();
	}
}
