namespace AdventOfCodeManager.ConsoleCommons;

public sealed class PartOfDayMenuState : IMenuState
{
    private readonly int _year;
    private readonly int _day;

    private readonly string[] _parts =
    {
        "Rano",
        "Popołudnie",
        "Wieczór",
        "Noc"
    };

    public PartOfDayMenuState(int year, int day)
    {
        _year = year;
        _day = day;
    }

    public void Render()
    {
        Ui.Header($"Menu 3/3: Wybierz część dnia (Rok: {_year}, Dzień: {_day})");

        for (int i = 0; i < _parts.Length; i++)
            Console.WriteLine($"{i + 1}. {_parts[i]}");

        Ui.Footer();
    }

    public NavAction Handle(string input)
    {
        if (Ui.IsQuit(input)) return NavAction.Quit();
        if (Ui.IsBack(input)) return NavAction.Pop();

        if (!int.TryParse(input, out var choice) || choice < 1 || choice > _parts.Length)
        {
            Ui.Invalid();
            return NavAction.Stay();
        }

        string selected = _parts[choice - 1];

        Console.Clear();
        Ui.Header("✅ Wybrano");
        Console.WriteLine($"Rok:   {_year}");
        Console.WriteLine($"Dzień: {_day}");
        Console.WriteLine($"Część: {selected}");
        Console.WriteLine();
        Console.WriteLine("Enter = wróć do wyboru części dnia, B = wstecz, Q = wyjście");
        Console.Write("> ");

        var next = Console.ReadLine()?.Trim() ?? "";
        if (Ui.IsQuit(next)) return NavAction.Quit();
        if (Ui.IsBack(next)) return NavAction.Pop();
        return NavAction.Stay();
    }
}