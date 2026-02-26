using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day12.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var sum = 0;
		var span = input.AsSpan().Trim();
		var stringStarted = false;
		var numberStarted = false;
		var currentNumber = 0;
		var currentSign = 1;


		foreach (var c in span)
		{
			if (c == '\"')
				stringStarted = !stringStarted;

			if (stringStarted)
				continue;

			if (numberStarted && char.IsAsciiDigit(c))
			{
				currentNumber = currentNumber * 10 + (c - '0');
				continue;
			}

			if (numberStarted && !char.IsDigit(c))
			{
				sum += currentSign * currentNumber;

				numberStarted = false;
				continue;
			}

			if (!numberStarted)
			{
				if (c == '-')
				{
					currentSign = -1;
					numberStarted = true;
					currentNumber = 0;
				}
				if (char.IsAsciiDigit(c))
				{
					currentSign = 1;
					numberStarted = true;
					currentNumber = c - '0';
				}
			}
		}
		
		return sum.ToString();
	}
}
