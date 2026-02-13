namespace AdventOfCode.Year2025.Day09.Commons;
internal class Rectangle(Tile first, Tile second)
{
    public Tile First { get; } = first;
    public Tile Second { get; } = second;
    public long Area { get; } = (Math.Abs(first.X - second.X) + 1) * (Math.Abs(first.Y - second.Y) + 1);
}
