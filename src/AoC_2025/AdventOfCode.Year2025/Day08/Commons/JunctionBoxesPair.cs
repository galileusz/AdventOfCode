namespace AdventOfCode.Year2025.Day08.Commons;

internal class JunctionBoxesPair(JunctionBox firstBox, JunctionBox secondBox)
{
    public JunctionBox FirstBox { get; } = firstBox;
    public JunctionBox SecondBox { get; } = secondBox;
    public double Distance { get; } = DistanceHelper.CalculateDistance(firstBox, secondBox);
}
