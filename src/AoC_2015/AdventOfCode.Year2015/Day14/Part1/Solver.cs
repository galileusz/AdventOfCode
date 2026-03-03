using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;
using System.Collections.Immutable;

namespace AdventOfCode.Year2015.Day14.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	private const int _time = 2503;

	public override string Solve(string input)
	{
		var span = input.AsSpan().Trim();
		var lines = span.Split('\n');

		var max = 0;
		foreach (var line in lines)
		{
			var distance = GetDistance(span[line]);
			if (distance > max)
				max = distance;
		}

		return max.ToString();
	}

	private int GetDistance(ReadOnlySpan<char> span)
	{
		var indexFly = span.IndexOf(" can fly ");
		var indexKms = span.IndexOf(" km/s ");
		var indexSeconds = span.IndexOf(" seconds,");
		var indexRest = span.IndexOf(" rest for ");

		var speed = GetIntValue(span, indexFly + 9, indexKms);
		var flyTime = GetIntValue(span, indexKms + 10, indexSeconds);
		var restTime = GetIntValue(span, indexRest + 10, span.Length - 9);

		return CalculateDistance(speed, flyTime, restTime);
	}

	private int GetIntValue(ReadOnlySpan<char> span, int startIndex, int endIndex)
	{
		var value = 0;
		for (var i = startIndex; i < endIndex; i++)
			value = value * 10 + (span[i] - '0');
		return value;
	}

	private int CalculateDistance(int speed, int flyTime, int restTime)
	{
		var fullCycles = _time / (flyTime + restTime);
		var remainingTime = _time % (flyTime + restTime);

		return (fullCycles * flyTime + Math.Min(remainingTime, flyTime)) * speed;
	}
}
