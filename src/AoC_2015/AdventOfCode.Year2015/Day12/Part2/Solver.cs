using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;
using System.Text;

namespace AdventOfCode.Year2015.Day12.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var sum = 0;
		var span = input.AsSpan().Trim();
		var valueStarted = false;
		var stringStarted = false;
		var numberStarted = false;
		var numberBuilder = new StringBuilder();
		var startCheckRed = false;
		var previousIsR = false;
		var previousIsE = false;
		var previousIsD = false;
		var isRedObject = false;

		var tempSums = new int[30];


		foreach (var c in span)
		{
			if (c == ':')
				valueStarted = true;

			if (c == '{' || c == '}' || c == '[' || c == ']' || c == ',')
				valueStarted = false;

			if (c == '\"')
				stringStarted = !stringStarted;

			if (stringStarted && !valueStarted)
				continue;

			if (stringStarted && valueStarted)
			{
				if (c == '\"')
					startCheckRed = true;
				else if (c == 'r' && startCheckRed)
				{
					startCheckRed = false;
					previousIsR = true;
				}
				else if (c == 'e' && previousIsR)
				{
					previousIsR = false;
					previousIsE = true;
				}
				else if (c == 'd' && previousIsE)
				{
					previousIsE = false;
					previousIsD = true;
				}
				else if (c == '\"' && previousIsD)
				{
					previousIsD = false;
					numberStarted = false;
					isRedObject = true;
				}
				else
				{
					startCheckRed = false;
					previousIsR = false;
					previousIsE = false;
					previousIsD = false;
				}
			}

			if (numberStarted && char.IsAsciiDigit(c))
			{
				numberBuilder.Append(c);
				continue;
			}

			if (numberStarted && !char.IsDigit(c))
			{
				if (int.TryParse(numberBuilder.ToString(), out var number))
					sum += number;

				numberBuilder.Clear();
				numberStarted = false;
				continue;
			}

			if (c == '-' || char.IsDigit(c))
			{
				numberStarted = true;
				numberBuilder.Append(c);
			}
		}
		
		return sum.ToString();
	}
}
