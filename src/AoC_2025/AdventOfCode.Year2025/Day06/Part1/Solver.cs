using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day06.Part1;

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
		var numbers = new List<int[]>();
		var signCollectionIndex = lines.Length - 1;

		for (int i = 0; i < signCollectionIndex; ++i)
			numbers.Add(lines[i].Split(' ').Where(x => false == string.IsNullOrEmpty(x)).Select(x => int.Parse(x.Trim())).ToArray());

		var signs = lines[signCollectionIndex].Split(' ').Where(x => false == string.IsNullOrEmpty(x)).Select(x => x.Trim().First()).ToArray();

		Calculate(numbers, signs);
	}

	private void Calculate(List<int[]> numbers, char[] signs)
	{
		for (int i = 0; i < numbers[0].Length; ++i)
		{
			var itemNumbers = new List<int>();
			for (int j = 0; j < numbers.Count(); ++j)
				itemNumbers.Add(numbers[j][i]);

			_result += CalculateItem(itemNumbers, signs[i]);
		}
	}

	private long CalculateItem(IEnumerable<int> numbers, char sign)
	{
		if (sign == '+')
			return numbers.Sum();

		long result = 1;
		foreach (var number in numbers)
			result *= number;

		return result;
	}
}
