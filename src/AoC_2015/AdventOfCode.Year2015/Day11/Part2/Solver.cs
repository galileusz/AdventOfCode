using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day11.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day11.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	private const int _maxLetter = 122;
	private const int _minLetter = 97;
	private bool _firstRun;
	private bool _isFirstPasswordFound;

	public override string Solve(string input)
	{
		_firstRun = true;
		_isFirstPasswordFound = false;
		var span = input.AsSpan().Trim();
		var maxLevel = span.Length - 1;
		var password = new byte[span.Length];

		var context = new PasswordContext(maxLevel);

		ProcessRecurency(span, password, 0, context, maxLevel);

		return new string(password.Select(x => (char)x).ToArray());
	}

	private void ProcessRecurency(ReadOnlySpan<char> span, byte[] password, int level, PasswordContext context, int maxLevel)
	{
		if (level > maxLevel)
		{
			_firstRun = false;
			return;
		}

		var minLetter = _firstRun ? span[level] : _minLetter;

		for (var i = minLetter; i <= _maxLetter; i++)
		{
			if (_isFirstPasswordFound && context.AreAllRulesRealized)
				return;

			if (!_isFirstPasswordFound && context.AreAllRulesRealized)
				_isFirstPasswordFound = true;

			context.ResetRulesRealization(level);

			if (IsLetterToSkip(i))
				continue;

			password[level] = (byte)i;

			if (!context.IsFirstPairRuleRealized && level > 0)
				CheckFirstPair(password, context, level);

			if (!context.IsFirstPairRuleRealized && level >= context.MaxLevelForFirstPairRule)
				continue;

			if (context.IsFirstPairRuleRealized && !context.IsSecondPairRuleRealized)
				CheckSecondPair(password, context, level);

			if (!context.IsStraightRuleRealized && level > 1)
				CheckStraightRule(password, context, level);

			ProcessRecurency(span, password, level + 1, context, maxLevel);
		}
	}

	private void CheckStraightRule(byte[] password, PasswordContext context, int level)
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

			previous = password[i];
		}
	}

	private void CheckFirstPair(byte[] password, PasswordContext context, int level)
	{
		for (int i = 0; i < level; i++)
		{
			if (password[i] == password[i + 1])
			{
				context.SetFirstPairRuleRealized(i + 1);
				return;
			}
		}
	}

	private void CheckSecondPair(byte[] password, PasswordContext context, int level)
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
