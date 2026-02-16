namespace AdventOfCode.Year2015.Day06.Commons;

internal class Light(int x, int y)
{
	public Position Position { get; } = new Position(x, y);
	public bool IsOn { get; private set; } = false;

	public void TurnOn()
	{
		IsOn = true;
	}

	public void TurnOff()
	{
		IsOn = false;
	}

	public void Toggle()
	{
		IsOn = !IsOn;
	}
}
