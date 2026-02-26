using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day01.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var result = 0;
		foreach (var c in input.AsSpan().Trim())
			result += 1 - ((c - '(') << 1);

		return result.ToString();
	}
}
