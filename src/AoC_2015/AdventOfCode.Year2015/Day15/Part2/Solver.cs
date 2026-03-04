using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;
using System.Collections.Immutable;

namespace AdventOfCode.Year2015.Day15.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	private int[] _ingredients;

	public override string Solve(string input)
	{
		_ingredients = new int[20];
		var span = input.AsSpan().Trim();
		var lines = span.Split('\n');

		var index = 0;
		foreach (var line in lines)
		{
			FillIngredient(span[line], index, ref _ingredients);
			index++;
		}

		var max = 0;
		CheckRecipes(_ingredients, ref max);

		return max.ToString();
	}

	private void CheckRecipes(int[] ingredients, ref int max)
	{
		for (int i1 = 0; i1 <= 100; i1++)
		{
			for (int i2 = 0; i2 <= 100 - i1; i2++)
			{
				for (int i3 = 0; i3 <= 100 - i1 - i2; i3++)
				{
					if (i1 + i2 + i3 > 100)
						continue;
					if (ingredients[4] * i1 + ingredients[9] * i2 + ingredients[14] * i3 + ingredients[19] * (100 - i1 - i2 - i3) != 500)
						continue;

					var i4 = 100 - i1 - i2 - i3;
					var capacity = ingredients[0] * i1 + ingredients[5] * i2 + ingredients[10] * i3 + ingredients[15] * i4;
					var durability = ingredients[1] * i1 + ingredients[6] * i2 + ingredients[11] * i3 + ingredients[16] * i4;
					var flavor = ingredients[2] * i1 + ingredients[7] * i2 + ingredients[12] * i3 + ingredients[17] * i4;
					var texture = ingredients[3] * i1 + ingredients[8] * i2 + ingredients[13] * i3 + ingredients[18] * i4;
					if (capacity < 0)
						capacity = 0;
					if (durability < 0)
						durability = 0;
					if (flavor < 0)
						flavor = 0;
					if (texture < 0)
						texture = 0;
					var score = capacity * durability * flavor * texture;
					if (score > max)
						max = score;
				}
			}
		}
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
		ingredients[i + 4] = GetIntValue(span, indexCalories + 10, span.Length);
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
