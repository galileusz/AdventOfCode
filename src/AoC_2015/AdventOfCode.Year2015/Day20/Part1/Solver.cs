using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day20.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var number = int.Parse(input) / 10;

		int current;
		var currentHouse = 1;
		var i = 2;
		do
		{
			current = 0;
			var isPrime = true;
			for (int j = 2; j < i; j++)
			{
				if (i % j == 0)
				{
					isPrime = false;
					break;
				}
			}
			if (isPrime)
			{
				current += i;
				currentHouse *= i;
			}

			i++;
		} while (current < number);

		return string.Empty;
	}
}
