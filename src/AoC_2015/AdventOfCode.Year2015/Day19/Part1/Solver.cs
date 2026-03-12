using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day19.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	private List<Range> _replacements;
	private Range _checkString;

	public override string Solve(string input)
	{
		_replacements = new List<Range>();
		var span = input.AsSpan().Trim();
		var lines = span.Split('\n');

		foreach (var line in lines)
		{
			var indexArrow = span[line].IndexOf(" => ");

			if (indexArrow != -1)
			{
				_replacements.Add(new Range(line.Start, line.Start.Value + indexArrow));
				_replacements.Add(new Range(line.Start.Value + indexArrow + 4, line.End));
				continue;
			}

			if (line.End.Value - line.Start.Value > 1)
				_checkString = line;
		}

		for (int i = 0; i < _replacements.Count; i += 2)
		{
			var amount = CheckAmountOfChanges(span, i);
		}


		return string.Empty;
	}

	private int CheckAmountOfChanges(ReadOnlySpan<char> span, int i)
	{
		var lookingFor = span[_replacements[i]];

		var amount = 0;
		var index = -1;
		do
		{
			var nextIndex = index + 1;
			index = span[(_checkString.Start.Value + nextIndex)..(_checkString.End.Value)].IndexOf(lookingFor);
			if (index == -1)
				break;

			amount++;
			index += nextIndex;

		} while (index != -1);

		if (amount > 0)
		{
			var minusAmount = CheckAmountOfAgregateChanges(span, i, lookingFor, span[_replacements[i + 1]]);
			amount -= minusAmount;
		}

		return amount;
	}

	private int CheckAmountOfAgregateChanges(ReadOnlySpan<char> span, int i, ReadOnlySpan<char> lookingFor, ReadOnlySpan<char> replacement)
	{
		var indexInReplacment = replacement.IndexOf(lookingFor);
		if (indexInReplacment == -1)
			return 0;

		var startsWith = replacement.StartsWith(lookingFor);
		var endsWith = replacement.EndsWith(lookingFor);
		var isMultiplied = CheckIsMultiplied(lookingFor, replacement);

		return 0;
	}

	private bool CheckIsMultiplied(ReadOnlySpan<char> lookingFor, ReadOnlySpan<char> replacement)
	{
		var index = -lookingFor.Length;
		var lastIndex = 0;
		do
		{
			index = replacement[(lastIndex)..replacement.Length].IndexOf(lookingFor);
			if (index == 0)
			{
				lastIndex += lookingFor.Length;
				if (lastIndex == replacement.Length)
					return true;

				continue;
			}
			return false;

		} while (true);
	}
}
