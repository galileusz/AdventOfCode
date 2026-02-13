namespace AdventOfCode.Year2025.Day12.Commons;

internal class PackageProblem
{
    public int AreaWidth { get; set; }
    public int AreaHeight { get; set; }
    public int[] Packages { get; set; } = new int[0];

    public bool IsTrivial { get; }

    public PackageProblem(string rawProblem)
    {
        var items = rawProblem.Split(' ');
        var rawArea = items[0];
        var indexOfX = rawArea.IndexOf('x');
        var indexOfColon = rawArea.IndexOf(':');
        AreaWidth = Convert.ToInt32(rawArea.Substring(0, indexOfX));
        AreaHeight = Convert.ToInt32(rawArea.Substring(indexOfX + 1, indexOfColon - indexOfX - 1));

        var packagesList = new List<int>();
        foreach (var item in items.Skip(1))
        {
            packagesList.Add(Convert.ToInt32(item));
        }
        Packages = packagesList.ToArray();

        IsTrivial = Packages.Sum() * 9 <= AreaWidth * AreaHeight;
    }

    public bool IsPossible(Dictionary<int, PresentBox> boxes)
    {
        var totalArea = 0;
        for (int i = 0; i < Packages.Length; i++)
        {
            totalArea += boxes[i].Area * Packages[i];
        }
        if (totalArea > AreaWidth * AreaHeight)
        {
            return false;
        }
        return true;
    }
}
