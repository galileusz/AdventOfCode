namespace AdventOfCodeManager.ConsoleCommons;

public interface IMenuState
{
	void Render();
	NavAction Handle(ConsoleKeyInfo key);
}