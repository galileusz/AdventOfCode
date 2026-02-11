namespace AdventOfCodeManager.ConsoleCommons;

public sealed class YearMenuState : IMenuState
{
    private readonly int[] _years = { 2024, 2025, 2026 };

    public void Render()
    {
        Ui.Header("Menu 1/3: Wybierz rok");

        for (int i = 0; i < _years.Length; i++)
            Console.WriteLine($"{i + 1}. {_years[i]}");

        Ui.Footer();
    }

    public NavAction Handle(string input)
    {
        if (Ui.IsQuit(input)) return NavAction.Quit();
        if (Ui.IsBack(input)) return NavAction.Pop(); // jeśli jesteś na topie, to wyjdzie z appki (stack się opróżni)

        if (!int.TryParse(input, out var choice) || choice < 1 || choice > _years.Length)
        {
            Ui.Invalid();
            return NavAction.Stay();
        }

        int selectedYear = _years[choice - 1];
        return NavAction.Push(new DayMenuState(selectedYear));
    }
}