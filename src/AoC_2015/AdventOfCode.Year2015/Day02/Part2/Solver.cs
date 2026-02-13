using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;
using System.Text;

namespace AdventOfCode.Year2015.Day02.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var currentNumber = new StringBuilder();
		var numbers = new int[3];
		var index = 0;
		var result = 0;
		foreach (var c in input.AsSpan())
		{
			if (c == '\n')
			{
				numbers[2] = int.Parse(currentNumber.ToString());
				numbers.Sort();

				var abc = numbers[0] * numbers[1] * numbers[2];
				result += abc + 2 * (numbers[0] + numbers[1]);

				currentNumber.Clear();
				index = 0;
			}
			else if (c == 'x')
			{
				numbers[index] = int.Parse(currentNumber.ToString());
				currentNumber.Clear();
				index++;
			}
			else
			{
				currentNumber.Append(c);
			}
		}

		return result.ToString();
	}
}
