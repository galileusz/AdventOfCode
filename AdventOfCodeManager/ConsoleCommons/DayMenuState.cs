namespace AdventOfCodeManager.ConsoleCommons;

public sealed class DayMenuState : IMenuState
{
    private readonly int _year;
    private readonly int[] _days = Enumerable.Range(1, 31).ToArray();

    public DayMenuState(int year) => _year = year;

    public void Render()
    {
        Ui.Header($"Menu 2/3: Wybierz dzień (Rok: {_year})");

        // proste wypisanie w 2 kolumnach
        for (int i = 0; i < _days.Length; i += 2)
        {
            string left = $"{i + 1}. {_days[i],2}";
            string right = (i + 1 < _days.Length) ? $"{i + 2}. {_days[i + 1],2}" : "";
            Console.WriteLine($"{left}    {right}");
        }

        Ui.Footer();
    }

    public NavAction Handle(string input)
    {
        if (Ui.IsQuit(input)) return NavAction.Quit();
        if (Ui.IsBack(input)) return NavAction.Pop();

        if (!int.TryParse(input, out var choice) || choice < 1 || choice > _days.Length)
        {
            Ui.Invalid();
            return NavAction.Stay();
        }

        int selectedDay = _days[choice - 1];
        return NavAction.Push(new PartOfDayMenuState(_year, selectedDay));
    }
}