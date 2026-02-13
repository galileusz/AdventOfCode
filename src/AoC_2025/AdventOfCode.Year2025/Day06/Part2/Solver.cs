using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day06.Part2;

public class Solver : BaseResolver, IAdventOfCode
{
	private long _result = 0;
	private const int ASCII_DIGIT_OFFSET = 48;

	public override string Solve(string input)
	{
		var lines = input.Split('\n');

		Process(lines);

		return _result.ToString();
	}

	private void Process(string[] lines)
	{
		var signCollectionIndex = lines.Length - 1;
		var lastSing = lines[signCollectionIndex][0];
		var lastIndex = 0;

		for (int i = 1; i < lines[signCollectionIndex].Length; ++i)
		{
			var sign = lines[signCollectionIndex][i];
			if (sign == '*' || sign == '+')
			{
				Calculate(lines, lastSing, lastIndex, i);
				lastIndex = i;
				lastSing = sign;
			}
		}
		Calculate(lines, lastSing, lastIndex, lines[0].Length + 1);
	}

	private void Calculate(string[] lines, char sign, int signIndex, int next)
	{
		var numbers = new List<int>();
		var signCollectionIndex = lines.Length - 1;

		for (int i = next - 2; i >= signIndex; --i)
		{
			var digitChars = new List<char>();
			for (int j = 0; j < signCollectionIndex; ++j)
				digitChars.Add(lines[j][i]);

			numbers.Add(CreateNumber(digitChars));
		}

		_result += CalculateItem(numbers, sign);
	}

	private int CreateNumber(List<char> digitChars)
	{
		var result = 0;
		var factor = 1;

		for (int i = digitChars.Count - 1; i >= 0; --i)
		{
			if (digitChars[i] != ' ')
			{
				result += factor * (digitChars[i] - ASCII_DIGIT_OFFSET);
				factor *= 10;
			}
		}

		return result;
	}

	private long CalculateItem(List<int> numbers, char sign)
	{
		if (sign == '+')
			return numbers.Sum();

		long result = 1;
		foreach (var number in numbers)
			result *= number;

		return result;
	}
}
