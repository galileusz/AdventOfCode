using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day03.Commons;
using AdventOfCodeGate.Interfaces;
namespace AdventOfCode.Year2015.Day03.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var housesList = new List<House>() { new House(0, 0) };

		var x1 = 0;
		var y1 = 0;
		var x2 = 0;
		var y2 = 0;
		var index = 0;
		foreach (var c in input.AsSpan().Trim())
		{
			var isSanta = index % 2 == 0;
			var x = isSanta ? x1 : x2;
			var y = isSanta ? y1 : y2;

			if (c == '^')
				x++;
			else if (c == 'v')
				x--;
			else if (c == '>')
				y++;
			else if (c == '<')
				y--;

			if (isSanta)
			{
				x1 = x;
				y1 = y;
			}
			else
			{
				x2 = x;
				y2 = y;
			}

			housesList.Add(new House(x, y));
			index++;
		}

		return housesList.Distinct().Count().ToString();
	}
}
