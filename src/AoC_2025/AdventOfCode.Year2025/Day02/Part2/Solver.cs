using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day02.Part2;

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
		var half = digits / 2;


		for (int i = half; i >= 1; i--)
		{
			var segments = digits / i;
			if (i * segments != digits)
				continue;

			var same = true;
			var last = numberString[0..i];
			for (int j = 1; j < segments; j++)
			{
				var next = numberString[(j * i)..(i * (j + 1))];

				if (next != last)
				{
					same = false;
					break;
				}
				last = next;
			}
			if (same == true)
			{
				return false;
			}
		}
		return true;
	}
}
