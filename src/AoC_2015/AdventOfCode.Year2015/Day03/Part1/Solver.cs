using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day03.Commons;
using AdventOfCodeGate.Interfaces;
namespace AdventOfCode.Year2015.Day03.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var housesList = new List<House>() { new House(0, 0) };

		var x = 0;
		var y = 0;
		foreach (var c in input.AsSpan().Trim())
		{
			if (c == '^')
				x++;
			else if (c == 'v')
				x--;
			else if (c == '>')
				y++;
			else if (c == '<')
				y--;

			housesList.Add(new House(x, y));
		}

		return housesList.Distinct().Count().ToString();
	}
}
