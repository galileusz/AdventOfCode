using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day02.Part1;

public class Solver : BaseResolver, IAdventOfCode
{
	private long _result = 0;

	public override string Solve(string input)
	{
		var ranges = input.Split(',');
		foreach (var range in ranges)
			DetectInvalidsInRange(range);

		return _result.ToString();
	}

	private void DetectInvalidsInRange(string range)
	{
		var rangeEdges = range.Split('-');
		var startRange = long.Parse(rangeEdges[0]);
		var endRange = long.Parse(rangeEdges[1]);

		DetectInvalids(startRange, endRange);
	}

	private void DetectInvalids(long startRange, long endRange)
	{
		for (var i = startRange; i <= endRange; i++)
		{
			var isValid = IsValid(i);

			if (false == isValid)
				_result += i;
		}
	}

	private bool IsValid(long number)
	{
		var numberString = number.ToString();

		var digits = numberString.Length;

		if (digits % 2 != 0)
			return true;

		var half = digits / 2;

		return numberString[0..half] != numberString[half..];
	}
}