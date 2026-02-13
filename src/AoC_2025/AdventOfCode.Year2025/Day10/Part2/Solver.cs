using AdventOfCode.Commons;
using AdventOfCode.Year2025.Day10.Commons;
using AdventOfCodeGate.Interfaces;
using System.Diagnostics;

namespace AdventOfCode.Year2025.Day10.Part2;

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
		var lampSets = lines.Select(x => new LampSet(x)).ToList();

		var index = 0;
		var amount = lampSets.Count;
		foreach (var set in lampSets)
		{
			Console.WriteLine($"Processing set {++index} of {amount}...");
			ProcessSet(set);
		}
	}

	private void ProcessSet(LampSet set)
	{
		var buttonMaxClicks = new int[set.Buttons.Count];
		var buttonsCount = set.Buttons.Count;

		for (int i = 0; i < buttonsCount; ++i)
		{
			var max = 99999999;
			for (int j = 0; j < set.Joltage.Length; ++j)
			{
				if (set.Buttons[i].Contains(j) && max > set.Joltage[j])
					max = set.Joltage[j];
			}
			buttonMaxClicks[i] = max;
		}

		var functionsSet = new FunctionsSet(set);
		var buttonsToIterate = functionsSet.WhatHaveToIterate(buttonMaxClicks);

		var indexOfButtonsToIterate = GetIndexOfBUttonsToIterate(buttonsToIterate);
		var maxOfClicks = indexOfButtonsToIterate.Select(x => buttonMaxClicks[x]).ToArray();

		var allPossibilities = new List<int[]>();
		var tempPossibilities = new int[buttonMaxClicks.Length];

		var stopWatch = Stopwatch.StartNew();

		if (indexOfButtonsToIterate.Length > 3)
			ProcessParallel(functionsSet, allPossibilities, tempPossibilities, indexOfButtonsToIterate, maxOfClicks);
		else
			ProcessRecurrency(functionsSet, allPossibilities, tempPossibilities, indexOfButtonsToIterate, maxOfClicks, 0);

		stopWatch.Stop();

		var result = allPossibilities.OrderBy(x => x.Sum()).First();
		var clicksSum = result.Sum();
		Console.WriteLine($"Wynik: {string.Join('-', result)} SUMA: {clicksSum} CZAS: {stopWatch.Elapsed.TotalSeconds} sekund");

		_result += clicksSum;
	}

	private void ProcessParallel(FunctionsSet functionsSet, List<int[]> allPossibilities, int[] tempPossibilities, int[] indexOfButtonsToIterate, int[] maxOfClicks)
	{
		Parallel.For(0, maxOfClicks[0], i =>
		{
			var localAllPossibilities = new List<int[]>();
			var localTempPossibilities = tempPossibilities.ToArray();
			localTempPossibilities[indexOfButtonsToIterate[0]] = i;
			ProcessRecurrency(functionsSet, localAllPossibilities, localTempPossibilities, indexOfButtonsToIterate, maxOfClicks, 1);
			lock (allPossibilities)
			{
				allPossibilities.AddRange(localAllPossibilities);
			}
		});
	}

	private int[] GetIndexOfBUttonsToIterate(bool[] buttonsToIterate)
	{
		var indexes = new List<int>();
		for (int i = 0; i < buttonsToIterate.Length; ++i)
		{
			if (buttonsToIterate[i])
				indexes.Add(i);
		}
		return indexes.ToArray();
	}

	private void ProcessRecurrency(
			FunctionsSet functionsSet,
			List<int[]> allPossibilities,
			int[] tempPossibilities,
			int[] indexOfButtonsToIterate,
			int[] maxOfClicks,
			int level)
	{
		if (level == maxOfClicks.Length)
			return;

		for (int i = 0; i <= maxOfClicks[level]; ++i)
		{
			tempPossibilities[indexOfButtonsToIterate[level]] = i;

			if (level == maxOfClicks.Length - 1)
			{
				if (false == TryCalculateClicks(tempPossibilities, functionsSet, out var calculatedClicks))
					continue;

				if (functionsSet.IsClicksValid(calculatedClicks))
					allPossibilities.Add(calculatedClicks.ToArray());
			}

			ProcessRecurrency(functionsSet, allPossibilities, tempPossibilities, indexOfButtonsToIterate, maxOfClicks, level + 1);
		}
	}

	private bool TryCalculateClicks(int[] tempPossibilities, FunctionsSet functionsSet, out int[] clicks)
	{
		if (functionsSet.CalculationPattern == null || functionsSet.CalculationPattern.Count == 0)
		{
			clicks = tempPossibilities.ToArray();
			return true;
		}

		clicks = tempPossibilities.ToArray();

		foreach (var func in functionsSet.CalculationPattern)
		{
			var value = func.Item2.CalculateClick(func.Item1, clicks);

			if (value < 0)
				return false;

			clicks[func.Item1] = value;
		}

		return true;
	}
}