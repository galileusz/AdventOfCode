using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day05.Part2;

public class Solver : BaseResolver, IAdventOfCode
{
	private long _result = 0;
	private List<(long, long)> _ranges;
	private List<(long, long)> _finalRanges;

	public override string Solve(string input)
	{
		_ranges = new List<(long, long)>();
		_finalRanges = new List<(long, long)>();

		var lines = input.Split('\n');

		Process(lines);

		return _result.ToString();
	}

	private void Process(string[] lines)
	{
		for (int i = 0; i < lines.Length; ++i)
		{
			if (lines[i].Contains('-'))
				ProcessRange(lines[i]);

			if (string.IsNullOrWhiteSpace(lines[i]))
				break;
		}

		for (int i = 0; i < _ranges.Count; ++i)
			ProcessFinalRanges(i);

		_result = _finalRanges.Select(x => x.Item2 - x.Item1 + 1).Sum();
	}

	private void ProcessRange(string line)
	{
		var indexOfRangeSign = line.IndexOf('-');
		var startNumber = long.Parse(line[0..indexOfRangeSign]);
		var endNumber = long.Parse(line[(indexOfRangeSign + 1)..]);

		_ranges.Add((startNumber, endNumber));
	}

	private void ProcessFinalRanges(int indexOfRange)
	{
		if (indexOfRange == 0)
		{
			_finalRanges.Add(_ranges[indexOfRange]);
			return;
		}

		var start = _ranges[indexOfRange].Item1;
		var end = _ranges[indexOfRange].Item2;
		var startInRange = -1;
		var endInRange = -1;

		for (int i = 0; i < _finalRanges.Count; i++)
		{
			if (start >= _finalRanges[i].Item1 && start <= _finalRanges[i].Item2)
				startInRange = i;

			if (end >= _finalRanges[i].Item1 && end <= _finalRanges[i].Item2)
				endInRange = i;
		}

		if (startInRange == endInRange && startInRange != -1)
			return;

		if (startInRange != -1 && endInRange == -1)
		{
			_finalRanges[startInRange] = (_finalRanges[startInRange].Item1, end);
			return;
		}

		if (startInRange == -1 && endInRange != -1)
		{
			_finalRanges[endInRange] = (start, _finalRanges[endInRange].Item2);
			return;
		}

		if (startInRange != -1 && endInRange != -1)
		{
			var startOfNewRange = _finalRanges[startInRange].Item1;
			_finalRanges[endInRange] = (startOfNewRange, _finalRanges[endInRange].Item2);
			_finalRanges.RemoveAt(startInRange);
			return;
		}

		if (startInRange == -1 && endInRange == -1)
		{
			var otherRangeInsideThis = CheckIsOtherRangeInsideThis(start, end);
			if (otherRangeInsideThis)
				return;

			_finalRanges.Add((start, end));
		}
	}

	private bool CheckIsOtherRangeInsideThis(long start, long end)
	{
		for (int i = 0; i < _finalRanges.Count; ++i)
		{
			if (start <= _finalRanges[i].Item1 && end >= _finalRanges[i].Item2)
			{
				_finalRanges[i] = (start, end);
				return true;
			}
		}
		return false;
	}
}