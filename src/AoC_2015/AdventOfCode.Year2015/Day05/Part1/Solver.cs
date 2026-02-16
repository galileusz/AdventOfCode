using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day05.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		int result = 0;
		var inputSpan = input.AsSpan();

		var vowelsCount = 0;
		var hasDoubleLetter = false;
		var hasCommonString = false;
		char lastChar = '\0';
		foreach (var c in inputSpan)
		{
			if (c == '\n')
			{
				if (vowelsCount > 2 && hasDoubleLetter && !hasCommonString)
					result++;

				vowelsCount = 0;
				hasDoubleLetter = false;
				hasCommonString = false;
				lastChar = '\0';
				continue;
			}

			if (hasCommonString)
				continue;

			if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
				vowelsCount++;

			if (lastChar == c)
				hasDoubleLetter = true;

			if ((lastChar == 'a' && c == 'b') ||
					(lastChar == 'c' && c == 'd') ||
					(lastChar == 'p' && c == 'q') ||
					(lastChar == 'x' && c == 'y'))
				hasCommonString = true;

			lastChar = c;
		}

		return result.ToString();
	}
}
