using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;
using System.Text;

namespace AdventOfCode.Year2015.Day12.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var sum = 0;
		var span = input.AsSpan().Trim();
		var stringStarted = false;
		var numberStarted = false;
		var numberBuilder = new StringBuilder();


		foreach (var c in span)
		{
			if (c == '\"')
				stringStarted = !stringStarted;

			if (stringStarted)
				continue;

			if (numberStarted && char.IsAsciiDigit(c))
			{
				numberBuilder.Append(c);
				continue;
			}

			if (numberStarted && !char.IsDigit(c))
			{
				if (int.TryParse(numberBuilder.ToString(), out var number))
					sum += number;

				numberBuilder.Clear();
				numberStarted = false;
				continue;
			}

			if (c == '-' || char.IsDigit(c))
			{
				numberStarted = true;
				numberBuilder.Append(c);
			}
		}
		
		return sum.ToString();
	}
}
