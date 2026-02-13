namespace AdventOfCode.Year2025.Day10.Commons;

internal class LampSet
{
    public bool[] LampConfig { get; }
    public List<List<int>> Buttons { get; }
    public int[] Joltage { get; }
    public int[] CalcJoltage { get; private set; }

    public LampSet(string inputSchema)
    {
        Buttons = new List<List<int>>();

        var lampConfigStart = inputSchema.IndexOf('[') + 1;
        var lampConfigEnd = inputSchema.IndexOf(']') - 1;
        var lampSchema = inputSchema.AsSpan().Slice(lampConfigStart, lampConfigEnd - lampConfigStart + 1).ToString();
        LampConfig = lampSchema.Select(c =>
        {
            if (c == '#')
                return true;
            else
                return false;

        }).ToArray();

        var buttonConfigStart = inputSchema.IndexOf('(', 0);
        while (buttonConfigStart != -1)
        {
            var buttonConfigEnd = inputSchema.IndexOf(')', buttonConfigStart);
            var buttonSchema = inputSchema.AsSpan().Slice(buttonConfigStart + 1, buttonConfigEnd - buttonConfigStart - 1).ToString();
            Buttons.Add(buttonSchema.Split(',').Select(int.Parse).ToList());
            buttonConfigStart = inputSchema.IndexOf('(', buttonConfigStart + 1);
        }

        var joltageConfigStart = inputSchema.IndexOf('{') + 1;
        var joltageConfigEnd = inputSchema.IndexOf('}') - 1;
        var joltageSchema = inputSchema.AsSpan().Slice(joltageConfigStart, joltageConfigEnd - joltageConfigStart + 1).ToString();
        Joltage = joltageSchema.Split(',').Select(int.Parse).ToArray();
    }

    public void ResetCalcJoltage()
    {
        CalcJoltage = Joltage.ToArray();
    }

    public bool TryButtonsClicks(int[] clicks)
    {
        ResetCalcJoltage();
        for (int i = 0; i < clicks.Length; ++i)
        {
            var button = Buttons[i];
            for (int j = 0; j < button.Count; ++j)
            {
                CalcJoltage[button[j]] -= clicks[i];
            }
        }
        return CalcJoltage.All(x => x == 0);
    }
}
