namespace AdventOfCode.Year2015.Day11.Commons;

internal class PasswordContext
{
	public bool IsFirstPairRuleRealized { get; private set; } = false;
	public bool IsSecondPairRuleRealized { get; private set; } = false;
	public bool IsStraightRuleRealized { get; private set; } = false;
	public int FirstPairRuleLevel { get; private set; } = 0;
	public int SecondPairRuleLevel { get; private set; } = 0;
	public int StraightRuleLevel { get; private set; } = 0;

	public void SetFirstPairRuleRealized(int level)
	{
				IsFirstPairRuleRealized = true;
				FirstPairRuleLevel = level;
	}

	public void SetSecondPairRuleRealized(int level)
	{
				IsSecondPairRuleRealized = true;
				SecondPairRuleLevel = level;
	}

	public void SetStraightRuleRealized(int level)
	{
				IsStraightRuleRealized = true;
				StraightRuleLevel = level;
	}

	public void ResetRulesRealization(int level)
	{
		if (IsFirstPairRuleRealized && FirstPairRuleLevel >= level)
		{
			IsFirstPairRuleRealized = false;
			FirstPairRuleLevel = 0;
		}
		if (IsSecondPairRuleRealized && SecondPairRuleLevel >= level)
		{
			IsSecondPairRuleRealized = false;
			SecondPairRuleLevel = 0;
		}
		if (IsStraightRuleRealized && StraightRuleLevel >= level)
		{
			IsStraightRuleRealized = false;
			StraightRuleLevel = 0;
		}
	}

	public bool AreAllRulesRealized() =>
		IsFirstPairRuleRealized && IsSecondPairRuleRealized && IsStraightRuleRealized;
}
