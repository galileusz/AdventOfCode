using AdventOfCodeManager.ConsoleCommons;
using AdventOfCodeManager.Helpers;

AssemblyLoader.LoadAll();

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.CursorVisible = false;

var nav = new NavigationStack();
nav.Push(new YearMenuState());

while (nav.HasState)
{
	Console.Clear();

	var state = nav.Peek();
	state.Render();

	var key = Console.ReadKey(intercept: true);
	var action = state.Handle(key);

	switch (action.Kind)
	{
		case NavActionKind.Push: nav.Push(action.NextState!); break;
		case NavActionKind.Pop: nav.Pop(); break;
		case NavActionKind.Quit: return;
		case NavActionKind.Stay: default: break;
	}
}