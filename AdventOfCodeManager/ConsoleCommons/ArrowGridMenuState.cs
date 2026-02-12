namespace AdventOfCodeManager.ConsoleCommons;

public abstract class ArrowGridMenuState : IMenuState
{
	private int _selectedIndex;

	protected abstract string Title { get; }
	protected abstract IReadOnlyList<string> Items { get; }
	protected abstract int MaxRowsPerColumn { get; } // np. 10
	protected abstract NavAction OnEnter(int selectedIndex);
	protected virtual NavAction OnBack() => NavAction.Pop();

	public void Render()
	{
		Ui.Header(Title);
		Ui.DrawGrid(Items, _selectedIndex, MaxRowsPerColumn);
		Ui.Footer("↑/↓ wiersz   ←/→ kolumna   Enter zatwierdź   B wstecz   Esc wyjście");
	}

	public NavAction Handle(ConsoleKeyInfo key)
	{
		if (key.Key == ConsoleKey.Escape) return NavAction.Quit();
		if (key.Key == ConsoleKey.B) return OnBack();

		if (key.Key is ConsoleKey.UpArrow or ConsoleKey.DownArrow or ConsoleKey.LeftArrow or ConsoleKey.RightArrow)
		{
			_selectedIndex = Ui.MoveGridIndex(_selectedIndex, key.Key, Items.Count, MaxRowsPerColumn);
			return NavAction.Stay();
		}

		if (key.Key == ConsoleKey.Enter)
			return OnEnter(_selectedIndex);

		return NavAction.Stay();
	}
}