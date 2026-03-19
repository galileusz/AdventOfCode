using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day23.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var linesIndexes = new int[100];

		var a = 1;
		var b = 0;
		var span = input.AsSpan().Trim();
		var instructions = span.Split('\n');

		var index = 0;
		foreach (var instruction in instructions)
		{
			linesIndexes[index++] = instruction.End.Value;
		}

		var i = 0;
		do
		{
			var startIndex = i == 0 ? 0 : linesIndexes[i - 1] + 1;
			var endIndex = linesIndexes[i];

			var instruction = span[startIndex..endIndex];
			Process(instruction, ref a, ref b, ref i);
		} while (i < index);

		return b.ToString();
	}

	private void Process(ReadOnlySpan<char> instruction, ref int a, ref int b, ref int i)
	{
		if (instruction.StartsWith("h"))
		{
			RunHalf(instruction, ref a, ref b);
			i++;
		}
		if (instruction.StartsWith("t"))
		{
			RunTriple(instruction, ref a, ref b);
			i++;
		}
		if (instruction.StartsWith("i"))
		{
			RunIncrement(instruction, ref a, ref b);
			i++;
		}
		if (instruction.StartsWith("jm"))
		{
			RunJump(instruction, ref i);
		}
		if (instruction.StartsWith("jio"))
		{
			RunJumpOne(instruction, ref a, ref b, ref i);
		}
		if (instruction.StartsWith("jie"))
		{
			RunJumpEven(instruction, ref a, ref b, ref i);
		}
	}

	private void RunJumpEven(ReadOnlySpan<char> instruction, ref int a, ref int b, ref int i)
	{
		if (instruction[4] == 'a' && a % 2 == 0)
			RunJump(instruction, ref i);
		else if (instruction[4] == 'b' && b % 2 == 0)
			RunJump(instruction, ref i);
		else
			i++;
	}

	private void RunJumpOne(ReadOnlySpan<char> instruction, ref int a, ref int b, ref int i)
	{
		if (instruction[4] == 'a' && a == 1)
			RunJump(instruction, ref i);
		else if (instruction[4] == 'b' && b == 1)
			RunJump(instruction, ref i);
		else
			i++;
	}

	private void RunJump(ReadOnlySpan<char> instruction, ref int i)
	{
		var index = instruction.IndexOf('+');
		if (index != -1)
		{
			var offset = int.Parse(instruction[(index + 1)..]);
			i += offset;
		}
		else
		{
			index = instruction.IndexOf('-');
			var offset = int.Parse(instruction[(index + 1)..]);
			i -= offset;
		}
	}

	private void RunIncrement(ReadOnlySpan<char> instruction, ref int a, ref int b)
	{
		if (instruction[4] == 'a')
			a++;
		else
			b++;
	}

	private void RunTriple(ReadOnlySpan<char> instruction, ref int a, ref int b)
	{
		if (instruction[4] == 'a')
			a *= 3;
		else
			b *= 3;
	}

	private void RunHalf(ReadOnlySpan<char> instruction, ref int a, ref int b)
	{
		if (instruction[4] == 'a')
			a /= 2;
		else
			b /= 2;
	}
}