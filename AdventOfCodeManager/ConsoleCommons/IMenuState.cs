namespace AdventOfCodeManager.ConsoleCommons;

public interface IMenuState
{
    void Render();
    NavAction Handle(string input);
}