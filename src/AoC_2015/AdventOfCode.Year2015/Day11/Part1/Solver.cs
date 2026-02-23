using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day10.Commons;
using AdventOfCode.Year2015.Day11.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day11.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	private const int _maxLetter = 122;
	private const int _minLetter = 97;
	private bool _firstRun = true;

	public override string Solve(string input)
	{
		var span = input.AsSpan().Trim();
		var maxLevel = span.Length - 1;
		var password = new int[span.Length];

		var context = new PasswordContext();

		ProcessRecurency(span, password, 0, context, maxLevel);

		return span.Length.ToString();
	}

	private void ProcessRecurency(ReadOnlySpan<char> span, int[] password, int level, PasswordContext context, int maxLevel)
	{
		if (level > maxLevel)
		{
			Console.WriteLine($"{new String(password.Select(x => (char)x).ToArray())}");
			_firstRun = false;
			return;
		}

		if (context.AreAllRulesRealized())
		{
			return;
		}

		context.ResetRulesRealization(level);

		var minLetter = _firstRun ? span[level] : _minLetter;

		for (var i = minLetter; i <= _maxLetter; i++)
		{
			if (IsLetterToSkip(i))
				continue;

			password[level] = i;

			if (!context.IsFirstPairRuleRealized && level > 0)
				CheckFirstPair(password, context, level);

			if (context.IsFirstPairRuleRealized && !context.IsSecondPairRuleRealized)
				CheckSecondPair(password, context, level);

			if (context.IsStraightRuleRealized && level > 1)
				CheckStraightRule(password, context, level);

			ProcessRecurency(span, password, level + 1, context, maxLevel);
		}
	}

	private void CheckStraightRule(int[] password, PasswordContext context, int level)
	{
		var straightCount = 1;
		var previous = password[0];

		for (var i = 1; i <= level; i++)
		{
			if (password[i] - previous == 1)
			{
				straightCount++;
				if (straightCount == 3)
				{
					context.SetStraightRuleRealized(i);
					return;
				}
			}
			else
			{
				straightCount = 1;
			}

			previous = i;
		}
	}

	private void CheckFirstPair(int[] password, PasswordContext context, int level)
	{
		for (int i = 0; i < level; i++)
		{
			if (password[i] == password[i+1])
			{
				context.SetFirstPairRuleRealized(i+1);
				return;
			}
		}
	}

	private void CheckSecondPair(int[] password, PasswordContext context, int level)
	{
		for (int i = context.FirstPairRuleLevel + 2; i < level; i++)
		{
			if (password[i] == password[i + 1])
			{
				context.SetSecondPairRuleRealized(i + 1);
				return;
			}
		}
	}

	private bool IsLetterToSkip(int letter) =>
		letter == 105 || letter == 108 || letter == 111;
}
