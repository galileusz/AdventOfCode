using AdventOfCodeManager.ConsoleCommons;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var nav = new NavigationStack();
nav.Push(new YearMenuState());

while (nav.HasState)
{
    Console.Clear();

    var state = nav.Peek();
    state.Render();

    var input = Console.ReadLine()?.Trim() ?? "";

    var action = state.Handle(input);

    switch (action.Kind)
    {
        case NavActionKind.Push:
            nav.Push(action.NextState!);
            break;

        case NavActionKind.Pop:
            nav.Pop();
            break;

        case NavActionKind.Quit:
            return;

        case NavActionKind.Stay:
        default:
            // nic - ekran odświeży się w następnej iteracji
            break;
    }
}
