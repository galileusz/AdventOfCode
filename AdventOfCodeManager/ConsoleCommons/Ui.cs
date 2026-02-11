namespace AdventOfCodeManager.ConsoleCommons;

public static class Ui
{
    public static void Header(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('=', Math.Max(10, title.Length)));
        Console.WriteLine();
    }

    public static void Footer()
    {
        Console.WriteLine();
        Console.WriteLine("[B] Wstecz   [Q] Wyjście");
        Console.Write("> ");
    }

    public static bool IsBack(string input) =>
        input.Equals("b", StringComparison.OrdinalIgnoreCase);

    public static bool IsQuit(string input) =>
        input.Equals("q", StringComparison.OrdinalIgnoreCase);

    public static void Invalid()
    {
        Console.WriteLine();
        Console.WriteLine("Nieprawidłowy wybór. Naciśnij Enter...");
        Console.ReadLine();
    }
}