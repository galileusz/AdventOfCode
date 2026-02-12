namespace AdventOfCodeManager.ConsoleCommons;

public abstract class ArrowMenuState : IMenuState
{
	private int _selectedIndex;

	protected abstract string Title { get; }
	protected abstract IReadOnlyList<string> Items { get; }
	protected abstract NavAction OnEnter(int selectedIndex);

	protected virtual NavAction OnBack() => NavAction.Pop();

	public void Render()
	{
		Ui.Header(Title);
		Ui.DrawList(Items, _selectedIndex);
		Ui.Footer("↑/↓ wybór   Enter zatwierdź   B wstecz   Esc wyjście");
	}

	public NavAction Handle(ConsoleKeyInfo key)
	{
		if (key.Key == ConsoleKey.Escape) return NavAction.Quit();
		if (key.Key == ConsoleKey.B) return OnBack();

		switch (key.Key)
		{
			case ConsoleKey.UpArrow:
				_selectedIndex = (_selectedIndex - 1 + Items.Count) % Items.Count;
				return NavAction.Stay();
			case ConsoleKey.DownArrow:
				_selectedIndex = (_selectedIndex + 1) % Items.Count;
				return NavAction.Stay();
			case ConsoleKey.Enter:
				return OnEnter(_selectedIndex);
			default:
				return NavAction.Stay();
		}
	}
}
