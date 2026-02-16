namespace AdventOfCode.Year2015.Day06.Commons;

internal class Part2Light(int x, int y)
{
	public Position Position { get; } = new Position(x, y);
	public int Brightness { get; private set; } = 0;

	public void TurnOn()
	{
		Brightness++;
	}

	public void TurnOff()
	{
		if (Brightness > 0)
			Brightness--;
	}

	public void Toggle()
	{
		Brightness += 2;
	}

	public void Reset()
	{
		Brightness = 0;
	}
}
