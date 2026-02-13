using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;
using System.Text;

namespace AdventOfCode.Year2025.Day03.Part1;

public class Solver : BaseResolver, IAdventOfCode
{
	private int _result = 0;

	public override string Solve(string input)
	{
		var banks = input.Split("\n");

		foreach (var bank in banks)
			ProcessBattery(bank);

		return _result.ToString();
	}

	private void ProcessBattery(string bank)
	{
		var sb = new StringBuilder();
		int index = -1;
		for (int j = 2; j > 0; j--)
		{
			(char c, index) = FindMaxChar(bank, index + 1, bank.Length - j);
			sb.Append(c);
		}

		var number = sb.ToString();

		_result += int.Parse(number);
	}

	private (char c, int index) FindMaxChar(string bank, int startIndex, int endIndex)
	{
		char max = '0';
		var index = 0;
		for (int i = startIndex; i <= endIndex; i++)
		{
			var c = bank[i];

			if (c > max)
			{
				max = c;
				index = i;
			}
		}

		return (max, index);
	}
}
