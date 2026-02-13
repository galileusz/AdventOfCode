using AdventOfCode.Commons;
using AdventOfCode.Year2025.Day10.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day10.Part1;

public class Solver : BaseResolver, IAdventOfCode
{
	private int _result = 0;

	public override string Solve(string input)
	{
		var lines = input.Split("\n");

		Process(lines);

		return _result.ToString();
	}

	private void Process(string[] lines)
	{
		var lampSets = lines.Select(x => new LampSet(x)).ToList();

		foreach (var set in lampSets)
			ProcessSet(set);
	}

	private void ProcessSet(LampSet set)
	{
		var buttonsPossibilities = PossibilitiesHelper.CreatePossibilitiesArray(set.Buttons.Count);

		foreach (var bp in buttonsPossibilities)
		{
			var clickedButtons = new List<List<int>>();
			for (int i = 0; i < bp.Length; i++)
			{
				if (bp[i] == 1)
					clickedButtons.Add(set.Buttons[i]);
			}

			var checkResult = CheckButtons(set.LampConfig, clickedButtons);

			if (checkResult)
			{
				_result += bp.Sum();
				return;
			}
		}
	}

	private bool CheckButtons(bool[] lampConfig, List<List<int>> clickedButtons)
	{
		var numberOfClicksDictionary = new Dictionary<int, int>();
		foreach (var b in clickedButtons)
		{
			foreach (var lampIndex in b)
			{
				if (false == numberOfClicksDictionary.ContainsKey(lampIndex))
					numberOfClicksDictionary[lampIndex] = 0;

				numberOfClicksDictionary[lampIndex]++;
			}
		}

		for (int i = 0; i < lampConfig.Length; i++)
		{
			if (lampConfig[i])
			{
				if (false == numberOfClicksDictionary.TryGetValue(i, out int value) || value % 2 == 0)
					return false;
			}
			else
			{
				if (numberOfClicksDictionary.TryGetValue(i, out int value) && value % 2 != 0)
					return false;
			}
		}

		return true;
	}
}