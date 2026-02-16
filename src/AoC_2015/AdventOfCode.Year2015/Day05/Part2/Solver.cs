using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day05.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day05.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		int result = 0;
		var inputSpan = input.AsSpan();

		var hasOneRepeatLetter = false;
		char beforeLastChar = '\0';
		char lastChar = '\0';
		var index = 0;
		var currentPairs = new List<CharPair>();
		foreach (var c in inputSpan)
		{
			if (c == '\n')
			{			
				if (hasOneRepeatLetter && HasRepeatPair(currentPairs))
					result++;

				hasOneRepeatLetter = false;
				beforeLastChar = '\0';
				lastChar = '\0';
				currentPairs.Clear();
				index = 0;
				continue;
			}

			if (index > 0)
					currentPairs.Add(new CharPair { First = lastChar, Second = c, Index = index });

			if (beforeLastChar == c)
				hasOneRepeatLetter = true;

			beforeLastChar = lastChar;
			lastChar = c;
			index++;
		}

		return result.ToString();
	}

	private bool HasRepeatPair(List<CharPair> currentPairs)
	{
		foreach (var pair in currentPairs)
		{
			if (currentPairs.Any(p => p.First == pair.First && p.Second == pair.Second && Math.Abs(p.Index - pair.Index) > 1))
				return true;
		}

		return false;
	}
}
