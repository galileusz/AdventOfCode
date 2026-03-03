using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;
using System.Collections.Immutable;

namespace AdventOfCode.Year2015.Day14.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	private const int _time = 2503;
	private const int _dataSize = 5;

	public override string Solve(string input)
	{
		var span = input.AsSpan().Trim();
		var lines = span.Split('\n');
		var data = new int[50];

		var index = 0;
		foreach (var line in lines)
		{
			FillData(span[line], data, index);
			index++;
		}

		RunRace(data, index);

		return GetWinnerPoints(data, index);
	}

	private void FillData(ReadOnlySpan<char> span, int[] data, int index)
	{
		var indexFly = span.IndexOf(" can fly ");
		var indexKms = span.IndexOf(" km/s ");
		var indexSeconds = span.IndexOf(" seconds,");
		var indexRest = span.IndexOf(" rest for ");

		var startIndex = index * _dataSize;
		data[startIndex] = GetIntValue(span, indexFly + 9, indexKms);
		data[startIndex + 1] = GetIntValue(span, indexKms + 10, indexSeconds);
		data[startIndex + 2] = GetIntValue(span, indexRest + 10, span.Length - 9);
		data[startIndex + 3] = 0;
	}

	private void RunRace(int[] data, int maxIndex)
	{
		for (var s = 1; s <= _time; s++)
		{
			var maxDistance = 0;
			for (int i = 0; i < maxIndex; i++)
			{
				var arrayIndex = i * _dataSize;
				var speed = data[arrayIndex];
				var flyTime = data[arrayIndex + 1];
				var restTime = data[arrayIndex + 2];

				var disatnceIndex = arrayIndex + 3;
				var interval = s % (flyTime + restTime);
				if (interval <= flyTime && interval > 0)
					data[disatnceIndex] += speed;

				if (data[disatnceIndex] > maxDistance)
					maxDistance = data[disatnceIndex];
			}
			for (int i = 0; i < maxIndex; i++)
			{
				var distanceIndex = i * _dataSize + 3;
				if (data[distanceIndex] == maxDistance)
					data[distanceIndex + 1]++;
			}
		}
	}

	private int GetIntValue(ReadOnlySpan<char> span, int startIndex, int endIndex)
	{
		var value = 0;
		for (var i = startIndex; i < endIndex; i++)
			value = value * 10 + (span[i] - '0');
		return value;
	}

	private string GetWinnerPoints(int[] data, int maxIndex)
	{
		var max = 0;
		for (int i = 0; i < maxIndex; i++)
		{
			var arrayIndex = i * _dataSize + 4;
			if (data[arrayIndex] > max)
				max = data[arrayIndex];
		}
		return max.ToString();
	}
}
