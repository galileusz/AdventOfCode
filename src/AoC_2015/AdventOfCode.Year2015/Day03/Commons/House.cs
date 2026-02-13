namespace AdventOfCode.Year2015.Day03.Commons;

readonly struct House : IEquatable<House>
{
	public readonly int X, Y;
	public House(int x, int y) { X = x; Y = y; }
	public bool Equals(House other) => X == other.X && Y == other.Y;
	public override int GetHashCode() => HashCode.Combine(X, Y);
}