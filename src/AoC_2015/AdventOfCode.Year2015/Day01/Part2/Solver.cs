using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day01.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var result = 0;
		var span = input.AsSpan().Trim();
		var i = 0;

		do
		{
			result += 1 - ((span[i] - '(') << 1);
			i++;
		} while (result >= 0);

		return (i).ToString();
	}
}
