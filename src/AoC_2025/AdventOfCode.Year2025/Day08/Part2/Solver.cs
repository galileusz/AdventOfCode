using AdventOfCode.Commons;
using AdventOfCode.Year2025.Day08.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day08.Part2;

public class Solver : BaseResolver, IAdventOfCode
{
	private int _result;
	private int _idCounter;
	private int _numberOfBoxes;

	public override string Solve(string input)
	{
		_idCounter = 1;
		var lines = input.Split("\n");

		Process(lines);

		return _result.ToString();
	}

	private void Process(string[] lines)
	{
		var junctionBoxes = lines.Select(CreateJunctionBox).ToArray();
		_numberOfBoxes = junctionBoxes.Length;

		var pairs = new List<JunctionBoxesPair>();

		for (int i = 0; i < _numberOfBoxes - 1; ++i)
		{
			for (int j = i + 1; j < _numberOfBoxes; ++j)
				pairs.Add(new JunctionBoxesPair(junctionBoxes[i], junctionBoxes[j]));
		}

		pairs = pairs.OrderBy(x => x.Distance).ToList();

		ProcessGroups(pairs);
	}

	private JunctionBox CreateJunctionBox(string x)
	{
		var coordinates = x.Split(',').Select(int.Parse).ToArray();
		return new JunctionBox(_idCounter++, coordinates[0], coordinates[1], coordinates[2]);
	}

	private void ProcessGroups(List<JunctionBoxesPair> pairs)
	{
		var groups = new List<List<JunctionBox>>();

		foreach (var pair in pairs)
		{
			var isFound = FindInGroupes(pair, groups);
			if (!isFound)
				groups.Add([pair.FirstBox, pair.SecondBox]);

			if (groups.Count != 1)
				continue;

			if (groups[0].Count < _numberOfBoxes)
				continue;

			_result = pair.FirstBox.X * pair.SecondBox.X;
			break;
		}
	}

	private bool FindInGroupes(JunctionBoxesPair pair, List<List<JunctionBox>> groups)
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
				if (item.Id == pair.FirstBox.Id)
				{
					isFound_1 = true;
					inGroup_1 = i;
				}
				if (item.Id == pair.SecondBox.Id)
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
			groups[inGroup_1].Add(pair.SecondBox);
			return true;
		}

		if (!isFound_1 && isFound_2)
		{
			groups[inGroup_2].Add(pair.FirstBox);
			return true;
		}

		if (inGroup_1 == inGroup_2)
			return true;

		groups[inGroup_1] = groups[inGroup_1].Concat(groups[inGroup_2]).ToList();
		groups.RemoveAt(inGroup_2);
		return true;
	}
}
