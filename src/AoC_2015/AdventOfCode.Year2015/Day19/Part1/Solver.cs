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

		var amount = 0;

		for (int i = 0; i < _replacements.Count; i += 2)
		{
			amount += CheckAmountOfChanges(span, i);
		}

		return amount.ToString();
	}

	private int CheckAmountOfChanges(ReadOnlySpan<char> span, int i)
	{
		var lookingFor = span[_replacements[i]];

		var amount = CalculateAppearancesOfString(span, lookingFor);

		if (amount > 0)
		{
			var minusAmount = CheckAmountOfAgregateChanges(span, i, lookingFor, span[_replacements[i + 1]]);
			amount -= minusAmount;
		}

		return amount;
	}

	private int CalculateAppearancesOfString(ReadOnlySpan<char> span, ReadOnlySpan<char> lookingFor)
	{
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

		return amount;
	}

	private int CheckAmountOfAgregateChanges(ReadOnlySpan<char> span, int i, ReadOnlySpan<char> lookingFor, ReadOnlySpan<char> replacement)
	{
		var amount = 0;
		var indexInReplacment = replacement.IndexOf(lookingFor);
		if (indexInReplacment == -1)
			return 0;

		var startsWith = replacement.StartsWith(lookingFor);
		var endsWith = replacement.EndsWith(lookingFor);
		var isMultiplied = CheckIsMultiplied(lookingFor, replacement);

		if (startsWith)
			amount += GetAmountOfStartAgregation(span, lookingFor, replacement, i);
		if (endsWith)
			amount += GetAmountOfEndAgregation(span, lookingFor, replacement, i);
		if (isMultiplied)
			amount += CalculateAppearancesOfString(span, replacement);

		return amount;
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

	private int GetAmountOfStartAgregation(ReadOnlySpan<char> span, ReadOnlySpan<char> lookingFor, ReadOnlySpan<char> replacement, int i)
	{
		var amount = 0;
		var tail = replacement[lookingFor.Length..];
		for (var j = i + 2; j < _replacements.Count; j += 2)
		{
			var item = span[_replacements[j]];
			var replacementItem = span[_replacements[j + 1]];
			if (replacementItem.StartsWith(tail) && replacementItem.EndsWith(item) && replacementItem.Length == tail.Length + item.Length)
			{
				Span<char> alloc = stackalloc char[lookingFor.Length + item.Length];

				lookingFor.CopyTo(alloc);
				item.CopyTo(alloc[lookingFor.Length..]);
				amount += CalculateAppearancesOfString(span, alloc);
			}
		}
		return amount;
	}

	private int GetAmountOfEndAgregation(ReadOnlySpan<char> span, ReadOnlySpan<char> lookingFor, ReadOnlySpan<char> replacement, int i)
	{
		var amount = 0;
		var head = replacement[..^lookingFor.Length];
		for (var j = i + 2; j < _replacements.Count; j += 2)
		{
			var item = span[_replacements[j]];
			var replacementItem = span[_replacements[j + 1]];
			if (replacementItem.EndsWith(head) && replacementItem.StartsWith(item) && replacementItem.Length == head.Length + item.Length)
			{
				Span<char> alloc = stackalloc char[lookingFor.Length + item.Length];

				item.CopyTo(alloc);
				lookingFor.CopyTo(alloc[item.Length..]);
				amount += CalculateAppearancesOfString(span, alloc);
			}
		}
		return amount;
	}
}
