namespace AdventOfCode.Year2025.Day10.Commons;

internal class FunctionsSet
{
    private const int AsciiOffset = 48;

    public List<XFunction> Functions { get; } = new List<XFunction>();
    public List<(int, XFunction)> CalculationPattern { get; private set; }
    public FunctionsSet(LampSet lampSet)
    {
        for (int i = 0; i < lampSet.Joltage.Length; ++i)
        {
            var pattern = new bool[lampSet.Buttons.Count];
            for (int j = 0; j < lampSet.Buttons.Count; ++j)
            {
                if (lampSet.Buttons[j].Contains(i))
                    pattern[j] = true;
                else
                    pattern[j] = false;
            }
            Functions.Add(new XFunction(pattern, lampSet.Joltage[i]));
        }

        Functions = Functions.OrderBy(x => x.Pattern.Count(y => y)).ToList();
    }

    public bool[] WhatHaveToIterate(int[] maxOfButtonClicks)
    {
        var iterationPatterns = GetIterationPatterns();
        var bestPattern = iterationPatterns.Last();
        var bestNumberOfIterations = long.MaxValue;
        var bestNumberOfClickedButtons = GetNumberOfClickedButtons(bestPattern);

        if (maxOfButtonClicks.Length < 4)
            return bestPattern;

        foreach (var pattern in iterationPatterns)
        {
            CalculationPattern = new List<(int, XFunction)>();
            if (IsPossibleToCalculate(pattern))
            {
                var currentNumberOfClickedButtons = GetNumberOfClickedButtons(pattern);
                var numberOfIterations = GetNumberOfIterations(pattern, maxOfButtonClicks);
                if (numberOfIterations < bestNumberOfIterations && currentNumberOfClickedButtons <= bestNumberOfClickedButtons)
                {
                    bestNumberOfClickedButtons = currentNumberOfClickedButtons;
                    bestPattern = pattern;
                    bestNumberOfIterations = numberOfIterations;
                }
            }
        }

        if (IsPossibleToCalculate(bestPattern))
            return bestPattern;

        throw new Exception("No possible iteration pattern found!");
    }

    private int GetNumberOfClickedButtons(bool[] bestPattern)
    {
        return bestPattern.Sum(x => x ? 1 : 0);
    }

    private long GetNumberOfIterations(bool[] pattern, int[] maxOfButtonClicks)
    {
        var maxOfClicks = maxOfButtonClicks.Select(x => (long)x).ToArray();
        long result = 1;
        for (int i = 0;  i < pattern.Length;  ++i)
        {
            if (pattern[i])
                result *= maxOfClicks[i];
        }
        return result;
    }

    private List<bool[]> GetIterationPatterns()
    {
        var numberOfItems = Functions.First().Pattern.Length;

        var result = new List<int[]>();
        for (int i = 1; i < Math.Pow(2, numberOfItems); i++)
            result.Add(Convert.ToString(i, 2).PadLeft(numberOfItems, '0').Select(x => Convert.ToInt32(x) - AsciiOffset).ToArray());

        result = result.OrderBy(x => x.Sum()).ToList();

        return result.Select(x => x.Select(y => y == 1).ToArray()).ToList();
    }

    private bool IsPossibleToCalculate(bool[] pattern)
    {
        var tempPattern = pattern.ToArray();
        var knownClicks = tempPattern.Sum(x => x ? 1 : 0);

        int startingKnownClicks;
        do
        {
            startingKnownClicks = knownClicks;
            foreach (var func in Functions)
            {
                var canCalculate = func.WhatCanCalculate(tempPattern);
                if (canCalculate.Any(x => x))
                {
                    var canCalculateIndex = Array.IndexOf(canCalculate, true);
                    tempPattern[canCalculateIndex] = true;

                    knownClicks++;  
                    CalculationPattern.Add((canCalculateIndex, func));
                }
            }
        } while (knownClicks < pattern.Length && knownClicks > startingKnownClicks);

        return knownClicks == pattern.Length;
    }

    public bool IsClicksValid(int[] clicks)
    {
        for (int i = 0; i < clicks.Length; ++i)
        {
            foreach (var func in Functions)
            {
                if (func.FunctionDictionary[i] == null)
                    continue;

                if (func.CalculateClick(i, clicks) != clicks[i])
                    return false;
            }
        }

        return true;
    }

}
