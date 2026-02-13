namespace AdventOfCode.Year2025.Day10.Commons;

internal class XFunction
{
    public Dictionary<int, Func<int[], int>?> FunctionDictionary = new Dictionary<int, Func<int[], int>?>();

    public bool[] Pattern { get; }

    public XFunction(bool[] pattern, int value)
    {
        Pattern = pattern;

        for (int i = 0; i < pattern.Length; ++i)
        {
            if (false == pattern[i])
            {
                FunctionDictionary[i] = null;
                continue;
            }

            var index = i;

            FunctionDictionary[index] = (clicks) =>
            {
                var sum = value;
                for (int j = 0; j < pattern.Length; ++j)
                {
                    if (j != index && pattern[j])
                        sum -= clicks[j];
                }
                return sum;
            };
        }
    }

    public bool[] WhatCanCalculate(bool[] knownClicks)
    {
        var canCalculate = new bool[Pattern.Length];

        for (int i = 0; i < Pattern.Length; ++i)
        {
            if (false == Pattern[i])
            {
                canCalculate[i] = false;
                continue;
            }

            if (Pattern[i] && knownClicks[i])
            {
                canCalculate[i] = false;
                continue;
            }

            var isPossible = true;
            for (int j = 0; j < knownClicks.Length; ++j)
            {
                if (j == i)
                    continue;

                if (Pattern[j] && false == knownClicks[j])
                {
                    isPossible = false;
                    break;
                }
            }
            canCalculate[i] = isPossible;
        }

        return canCalculate;
    }

    public int CalculateClick(int index, int[] clicks)
    {
        var func = FunctionDictionary[index];
        return func == null ? throw new InvalidOperationException($"Cannot calculate click for index {index}.") : func(clicks);
    }

}
