using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day10.Commons;
using AdventOfCodeGate.Interfaces;
using System.Text;

namespace AdventOfCode.Year2015.Day10.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var span = input.AsSpan().Trim();

		for (int i = 0; i < 50; i++)
		{
			span = NumberProcessor.Process(span);
		}

		return span.Length.ToString();
	}
}
