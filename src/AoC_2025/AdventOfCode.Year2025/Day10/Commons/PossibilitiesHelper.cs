namespace AdventOfCode.Year2025.Day10.Commons;

internal static class PossibilitiesHelper
{
    private const int ASCII_OFFSET = 48;
    public static List<int[]> CreatePossibilitiesArray(int numberOfItems)
    {
        var result = new List<int[]>();
        for (int i = 1; i < Math.Pow(2, numberOfItems); i++)
            result.Add(Convert.ToString(i, 2).PadLeft(numberOfItems,'0').Select(x => Convert.ToInt32(x) - ASCII_OFFSET).ToArray());

        result = result.OrderBy(x => x.Sum()).ToList();

        return result;
    }

    public static List<int[]> CreatePossibilitiesArray(LampSet set)
    {
        var buttonMaxClicks = new int[set.Buttons.Count];
        var buttonsCount = set.Buttons.Count;

        for (int i = 0; i < buttonsCount; ++i)
        {
            var max = 99999999;
            for (int j = 0; j < set.Joltage.Length; ++j)
            {
                if (set.Buttons[i].Contains(j) && max > set.Joltage[j])
                    max = set.Joltage[j];
            }
            buttonMaxClicks[i] = max;
        }

        var allPossibilities = CreatePossibilitiesByMaxClicks(buttonMaxClicks, set);

        return GetPossibilitiesAfterLimitation(allPossibilities, set.Joltage, set.Buttons);
    }

    private static List<int[]> CreatePossibilitiesByMaxClicks(int[] buttonMaxClicks, LampSet lampSet)
    {
        var allPossibilities = new List<int[]>();
        var tempPossibilities = new int[buttonMaxClicks.Length];

        var buttonsIndexesForJoltageInfluence = GetButtonsIndexesForJoltageInfluence(lampSet);

        ProcessRecurrency(allPossibilities, tempPossibilities, buttonMaxClicks, 0, lampSet.Joltage, buttonsIndexesForJoltageInfluence);

        return allPossibilities;
    }

    private static List<List<int>> GetButtonsIndexesForJoltageInfluence(LampSet set)
    {
        var result = new List<List<int>>();
        for (int i = 0; i < set.Joltage.Length; ++i)
        {
            var buttonIndexes = new List<int>();
            for (int j = 0; j < set.Buttons.Count; ++j)
            {
                if (set.Buttons[j].Contains(i))
                    buttonIndexes.Add(j);
            }
            result.Add(buttonIndexes);
        }
        return result;
    }

    private static void ProcessRecurrency(List<int[]> allPossibilities,
        int[] tempPossibilities,
        int[] buttonMaxClicks,
        int level,
        int[] joltage,
        List<List<int>> buttonsIndexesForJoltageInfluence)
    {
        if (level == buttonMaxClicks.Length)
            return;

        for (int i = 0; i <= buttonMaxClicks[level]; ++i)
        {
            tempPossibilities[level] = i;
            allPossibilities.Add(tempPossibilities.ToArray());

            if (IsTopLimit(tempPossibilities, joltage, buttonsIndexesForJoltageInfluence, level))
            {
                ClearRecurrencyPath(tempPossibilities, level);
                break;
            }
            if (IsBottomLimit(tempPossibilities, joltage, buttonsIndexesForJoltageInfluence, level, buttonMaxClicks))
                continue;

            ProcessRecurrency(allPossibilities, tempPossibilities, buttonMaxClicks, level + 1, joltage, buttonsIndexesForJoltageInfluence);
        }
    }

    private static void ClearRecurrencyPath(int[] tempPossibilities, int level)
    {
        for (int i = level + 1; i < tempPossibilities.Length; ++i)
            tempPossibilities[i] = 0;
    }

    private static bool IsBottomLimit(int[] tempPossibilities, int[] joltage,
        List<List<int>> buttonsIndexesForJoltageInfluence, int level, int[] buttonMaxClicks)
    {
        for (int i = 0; i < joltage.Length; ++i)
        {
            if (false == buttonsIndexesForJoltageInfluence[i].Contains(level))
                continue;

            var sum = 0;
            foreach (var index in buttonsIndexesForJoltageInfluence[i])
            {
                if (index <= level)
                    sum += tempPossibilities[index];
                else
                    sum += buttonMaxClicks[index];
            }
            if (sum < joltage[i])
                return true;
        }
        return false;
    }

    private static bool IsTopLimit(int[] tempPossibilities, int[] joltage, List<List<int>> buttonsIndexesForJoltageInfluence, int level)
    {
        for (int i = 0; i < joltage.Length; ++i)
        {
            if (level < buttonsIndexesForJoltageInfluence[i].Max())
                continue;

            var sum = 0;
            foreach (var index in buttonsIndexesForJoltageInfluence[i])
            {
                sum += tempPossibilities[index];
            }
            if (sum > joltage[i])
                return true;
        }
        return false;
    }

    private static List<int[]> GetPossibilitiesAfterLimitation(List<int[]> allPossibilities, int[] joltage, List<List<int>> buttons)
    {
        for (int i = 0; i < joltage.Length; ++i)
        {
            var buttonIndexes = new List<int>();
            for (int j = 0; j < buttons.Count; ++j)
            {
                if (buttons[j].Contains(i))
                    buttonIndexes.Add(j);
            }
            allPossibilities = allPossibilities.Where(possibility => IsRealyPossible(possibility, buttonIndexes, joltage[i])).ToList();
        }
        return allPossibilities;
    }

    private static bool IsRealyPossible(int[] possibility, List<int> buttonIndexes, int numberOfClicks)
    {
        var sum = 0;
        foreach (var index in buttonIndexes)
        {
            sum += possibility[index];
        }
        return sum == numberOfClicks;
    }
}
