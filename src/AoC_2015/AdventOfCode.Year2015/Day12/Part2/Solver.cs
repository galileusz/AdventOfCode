using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day12.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var span = input.AsSpan().Trim();
		var valueStarted = false;
		var stringStarted = false;
		var numberStarted = false;
		var startCheckRed = false;
		var previousIsR = false;
		var previousIsE = false;
		var previousIsD = false;
		var isRedObject = false;
		var redObjectLevel = -1;
		var level = -1;
		var currentNumber = 0;
		var currentSign = 1;

		var tempSums = new int[20];

		foreach (var c in span)
		{
			if (c == '{')
				level++;

			if (c == '}')
			{
				if (level == 0)
					break;

				if (isRedObject && level == redObjectLevel)
				{
					isRedObject = false;
					redObjectLevel = -1;

					for (var i = level; i < tempSums.Length; i++)
						tempSums[i] = 0;
				}
				else if (!isRedObject)
				{
					tempSums[level-1] += tempSums[level];
					tempSums[level] = 0;
				}
				level--;
			}

			if (isRedObject && level >= redObjectLevel)
				continue;


			if (c == ':')
				valueStarted = true;

			if (c == '{' || c == '}' || c == '[' || c == ']' || c == ',')
				valueStarted = false;

			if (c == '\"' && !previousIsD)
				stringStarted = !stringStarted;

			if (stringStarted && !valueStarted)
				continue;

			if (stringStarted && valueStarted)
			{
				if (c == '\"' && !previousIsD)
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
					redObjectLevel = level;
					stringStarted = false;
				}
				else
				{
					startCheckRed = false;
					previousIsR = false;
					previousIsE = false;
					previousIsD = false;
				}

				continue;
			}

			if (numberStarted && char.IsAsciiDigit(c))
			{
				currentNumber = currentNumber * 10 + (c - '0');
				continue;
			}

			if (numberStarted && !char.IsDigit(c))
			{
				tempSums[level] += currentSign * currentNumber;

				numberStarted = false;
				continue;
			}

			if (!numberStarted)
			{
				if (c == '-')
				{
					currentSign = -1;
					numberStarted = true;
					currentNumber = 0;
				}
				if (char.IsAsciiDigit(c))
				{
					currentSign = 1;
					numberStarted = true;
					currentNumber = c - '0';
				}
			}
		}

		return tempSums[0].ToString();
	}
}
