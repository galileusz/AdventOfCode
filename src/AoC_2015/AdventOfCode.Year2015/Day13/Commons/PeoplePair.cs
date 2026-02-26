namespace AdventOfCode.Year2015.Day13.Commons;

internal class PeoplePair(int firstPerson, int secondPerson, int happiness)
{
	public int FirstPerson { get; set; } = firstPerson;
	public int SecondPerson { get; set; } = secondPerson;
	public int Happiness { get; set; } = happiness;
}