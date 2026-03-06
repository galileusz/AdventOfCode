using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day18.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var array1 = new bool[10000];
		var array2 = new bool[10000];

		FillArray(array1, input.AsSpan());

		bool[] source;
		bool[] target = array2;
		for (int i = 0; i < 100; i++)
		{
			source = i % 2 == 0 ? array1 : array2;
			target = i % 2 == 0 ? array2 : array1;

			RunLife(source, target);
		}

		return target.Count(x => x).ToString();
	}

	private void FillArray(bool[] array1, ReadOnlySpan<char> span)
	{
		var index = 0;
		foreach (var c in span)
		{
			if (c == '\n')
				continue;

			array1[index] = c == '#';
			if (index == 0 || index == 99 || index == 9900 || index == 9999)
				array1[index] = true;

			index++;
		}
	}

	private void RunLife(bool[] source, bool[] target)
	{
		for (int x = 0; x < 100; x++)
		{
			for (int y = 0; y < 100; y++)
			{
				var neighbors = GetNeighbors(source, x, y);

				ChangeTarget(neighbors, source, target, x, y);
			}
		}
	}

	private int GetNeighbors(bool[] source, int x, int y)
	{
		var sum = 0;
		for (int i = x - 1; i <= x + 1; i++)
		{
			for (int j = y - 1; j <= y + 1; j++)
			{
				if (i < 0 || j < 0 || i > 99 || j > 99 || (i == x && j == y))
					continue;

				if (source[i * 100 + j])
					sum++;
				if (sum > 3)
					return 4;
			}
		}
		return sum;
	}

	private void ChangeTarget(int neighbors, bool[] source, bool[] target, int x, int y)
	{
		if ((x == 0 && y == 0) || (x == 0 && y == 99) || (x == 99 && y == 0) || (x == 99 && y == 99))
		{
			target[x * 100 + y] = true;
			return;
		}

		if (source[x * 100 + y])
		{
			target[x * 100 + y] = neighbors == 2 || neighbors == 3;
		}
		else
		{
			target[x * 100 + y] = neighbors == 3;
		}
	}
}
