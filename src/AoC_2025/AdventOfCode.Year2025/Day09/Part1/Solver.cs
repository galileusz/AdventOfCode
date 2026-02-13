using AdventOfCode.Commons;
using AdventOfCode.Year2025.Day09.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day09.Part1;

public class Solver : BaseResolver, IAdventOfCode
{
	private long _result = 0;

	public override string Solve(string input)
	{
		var lines = input.Split("\n");

		Process(lines);

		return _result.ToString();
	}

	private void Process(string[] lines)
	{
		var rectangles = new List<Rectangle>();
		for (int i = 0; i < lines.Length - 1; ++i)
		{
			for (int j = i + 1; j < lines.Length; ++j)
			{
				var coordinates_1 = lines[i].Split(',').Select(long.Parse).ToArray();
				var coordinates_2 = lines[j].Split(',').Select(long.Parse).ToArray();
				var first = new Tile(coordinates_1[0], coordinates_1[1]);
				var second = new Tile(coordinates_2[0], coordinates_2[1]);
				rectangles.Add(new Rectangle(first, second));
			}
		}

		_result = rectangles.Max(x => x.Area);
	}
}
