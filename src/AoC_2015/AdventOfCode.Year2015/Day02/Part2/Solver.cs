using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day02.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var currentNumber = 0;
		var numbers = new int[3];
		var index = 0;
		var result = 0;
		foreach (var c in input.AsSpan())
		{
			if (c == '\n')
			{
				numbers[2] = currentNumber;
				numbers.Sort();

				var abc = numbers[0] * numbers[1] * numbers[2];
				result += abc + 2 * (numbers[0] + numbers[1]);

				currentNumber = 0;
				index = 0;
			}
			else if (c == 'x')
			{
				numbers[index] = currentNumber;
				currentNumber = 0;
				index++;
			}
			else
			{
				currentNumber = currentNumber * 10 + (c - '0');
			}
		}

		return result.ToString();
	}
}
