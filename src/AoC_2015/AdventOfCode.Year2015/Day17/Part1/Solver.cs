using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;
using System.Buffers;

namespace AdventOfCode.Year2015.Day17.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	private ArrayPool<int> _pool = null!;
	private int _results;
	public override string Solve(string input)
	{
		_results = 0;
		var span = input.AsSpan().Trim();
		_pool = ArrayPool<int>.Shared;
		var array = _pool.Rent(30);
		var maxIndex = 0;

		GetData(span, array, ref maxIndex);

		CheckPosibilities(array, maxIndex);

		return _results.ToString();
	}

	private void GetData(ReadOnlySpan<char> span, int[] array, ref int maxIndex)
	{
		var currentNumber = 0;
		maxIndex = 0;
		foreach (var c in span)
		{
			if (c == '\n')
			{
				array[maxIndex++] = currentNumber;
				currentNumber = 0;
				continue;
			}
			currentNumber = currentNumber * 10 + (c - '0');
		}
		if (currentNumber != 0)
			array[maxIndex] = currentNumber;
	}

	private void CheckPosibilities(int[] numbers, int maxIndex)
	{
		var referenceArray = _pool.Rent(maxIndex + 1);
		for (var i = 1; i <= maxIndex + 1; i++)
			referenceArray[i - 1] = i;

		Recurency(numbers, referenceArray, maxIndex, 0);
	}

	private void Recurency(int[] numbers, int[] referenceArray, int maxIndex, int minIndex)
	{
		if (CheckIsFullfilled(numbers, referenceArray, maxIndex))
		{
			_pool.Return(referenceArray);
			return;
		}

		for (var i = minIndex; i <= maxIndex; i++)
		{
			var newReference = _pool.Rent(referenceArray.Length);
			Array.Copy(referenceArray, newReference, referenceArray.Length);
			newReference[i] = 0;

			Recurency(numbers, newReference, maxIndex, i + 1);
		}
	}

	private bool CheckIsFullfilled(int[] numbers, int[] referenceArray, int maxIndex)
	{
		var sum = 0;
		for (var i = 0; i <= maxIndex; i++)
		{
			if (referenceArray[i] == 0)
			{
				sum += numbers[i];
			}
			if (sum > 150)
			{
				return true;
			}
		}
		if (sum == 150)
		{
			_results++;

			return true;
		}

		return false;
	}
}
