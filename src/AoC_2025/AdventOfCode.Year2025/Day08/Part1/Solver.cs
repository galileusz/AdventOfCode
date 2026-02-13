using AdventOfCode.Commons;
using AdventOfCode.Year2025.Day08.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day08.Part1;

public class Solver : BaseResolver, IAdventOfCode
{
	private int _result = 1;
	private int _idCounter = 1;
	private const int NUMBER_OF_PAIRS = 1000;

	public override string Solve(string input)
	{
		var lines = input.Split("\n");

		Process(lines);

		return _result.ToString();
	}

	private void Process(string[] lines)
	{
		var junctionBoxes = lines.Select(CreateJunctionBox).ToArray();
		var amount = junctionBoxes.Length;

		var pairs = new List<JunctionBoxesPair>();

		for (int i = 0; i < amount - 1; ++i)
		{
			for (int j = i + 1; j < amount; ++j)
				pairs.Add(new JunctionBoxesPair(junctionBoxes[i], junctionBoxes[j]));
		}

		pairs = pairs.OrderBy(x => x.Distance).Take(NUMBER_OF_PAIRS).ToList();

		ProcessGroups(pairs.Select(x => (x.FirstBox.Id, x.SecondBox.Id)).ToList());
	}

	private JunctionBox CreateJunctionBox(string x)
	{
		var coordinates = x.Split(',').Select(int.Parse).ToArray();
		return new JunctionBox(_idCounter++, coordinates[0], coordinates[1], coordinates[2]);
	}

	private void ProcessGroups(List<(int, int)> pairs)
	{
		var groups = new List<List<int>>();
		foreach (var pair in pairs)
		{
			var isFound = FindInGroupes(pair, groups);
			if (!isFound)
				groups.Add([pair.Item1, pair.Item2]);
		}

		var largestGroups = groups.Select(x => x.Count).OrderByDescending(x => x).Take(3).ToArray();
		_result = largestGroups[0] * largestGroups[1] * largestGroups[2];
	}

	private bool FindInGroupes((int, int) pair, List<List<int>> groups)
	{
		if (groups.Count == 0)
			return false;

		var isFound_1 = false;
		var isFound_2 = false;
		var inGroup_1 = -1;
		var inGroup_2 = -1;

		for (int i = 0; i < groups.Count; ++i)
		{
			foreach (var item in groups[i])
			{
				if (item == pair.Item1)
				{
					isFound_1 = true;
					inGroup_1 = i;
				}
				if (item == pair.Item2)
				{
					isFound_2 = true;
					inGroup_2 = i;
				}
			}
		}

		if (!isFound_1 && !isFound_2)
			return false;

		if (isFound_1 && !isFound_2)
		{
			groups[inGroup_1].Add(pair.Item2);
			return true;
		}

		if (!isFound_1 && isFound_2)
		{
			groups[inGroup_2].Add(pair.Item1);
			return true;
		}

		if (inGroup_1 == inGroup_2)
			return true;

		groups[inGroup_1] = groups[inGroup_1].Concat(groups[inGroup_2]).ToList();
		groups.RemoveAt(inGroup_2);
		return true;
	}
}
