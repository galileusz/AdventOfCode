using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day01.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	private static readonly Dictionary<char, int> _directions = new()
	{
		{ '(', 1 },
		{ ')', -1 }
	};

	public override string Solve(string input)
	{
		var result = 0;
		foreach (var c in input.AsSpan().Trim())
			result += _directions[c];

		return result.ToString();
	}
}
