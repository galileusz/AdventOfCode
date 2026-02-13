namespace AdventOfCode.Year2025.Day08.Commons;

internal static class DistanceHelper
{
    public static double CalculateDistance(JunctionBox box1, JunctionBox box2)
    {
        return Math.Sqrt(Math.Pow(box1.X - box2.X, 2) + Math.Pow(box1.Y - box2.Y, 2) + Math.Pow(box1.Z - box2.Z, 2));
    }
}
