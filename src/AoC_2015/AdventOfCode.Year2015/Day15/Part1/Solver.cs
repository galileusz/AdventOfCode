using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;
using System.Collections.Immutable;

namespace AdventOfCode.Year2015.Day15.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	private int[] _ingredients;

	public override string Solve(string input)
	{
		_ingredients = new int[30];
		var span = input.AsSpan().Trim();
		var lines = span.Split('\n');

		var index = 0;
		foreach (var line in lines)
		{
			FillIngredient(span[line], index, ref _ingredients);
			index++;
		}

		return string.Empty;
	}

	private void FillIngredient(ReadOnlySpan<char> span, int index, ref int[] ingredients)
	{
		var indexCapacity = span.IndexOf(" capacity ");
		var indexDurability = span.IndexOf(" durability ");
		var indexFlavor = span.IndexOf(" flavor ");
		var indexTexture = span.IndexOf(" texture ");
		var indexCalories = span.IndexOf(" calories ");

		var i = index * 5;
		ingredients[i] = GetIntValue(span, indexCapacity + 10, indexDurability - 1);
		ingredients[i + 1] = GetIntValue(span, indexDurability + 12, indexFlavor - 1);
		ingredients[i + 2] = GetIntValue(span, indexFlavor + 8, indexTexture - 1);
		ingredients[i + 3] = GetIntValue(span, indexTexture + 9, indexCalories - 1);
	}

	private int GetIntValue(ReadOnlySpan<char> span, int startIndex, int endIndex)
	{
		var sign = 1;
		var value = 0;
		for (var i = startIndex; i < endIndex; i++)
		{
			if (span[i] == '-')
			{
				sign = -1;
				continue;
			}
			value = value * 10 + (span[i] - '0');
		}
		return sign * value;
	}
}
