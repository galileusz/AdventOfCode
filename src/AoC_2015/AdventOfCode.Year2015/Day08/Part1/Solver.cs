using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day08.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var span = input.AsSpan().Trim();
		var lineRanges = span.Split('\n');
		var result = 0;
		foreach ( var range in lineRanges )
		{
			var line = span[range];

			result += ProcessLine(line);
		}

		return result.ToString();
	}

	private int ProcessLine(ReadOnlySpan<char> line)
	{
		var length = line.Length;
		var result = -2;
		bool specialChar = false;
		int specialX = 0;
		foreach (var c in line)
		{
			if (specialChar && specialX > 0)
			{
				specialX++;
				if (specialX > 2)
				{
					specialChar = false;
					specialX = 0;
					result++;
				}
			}
			else if (c == '\\')
			{
				if (!specialChar)
					specialChar = true;
				else
				{
					specialChar = false;
					result++;
				}
			}
			else if (specialChar && c == 'x')
			{
				specialX++;
			}
			else if (specialChar && c == '\"')
			{
				specialChar = false;
				result++;
			}
			else
			{ 
				result++;
			}
		}

		return length - result;
	}
}
