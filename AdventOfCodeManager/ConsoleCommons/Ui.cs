namespace AdventOfCodeManager.ConsoleCommons;

public static class Ui
{
	public static void Header(string title)
	{
		Console.WriteLine(title);
		Console.WriteLine(new string('=', Math.Max(10, title.Length)));
		Console.WriteLine();
	}

	public static void Footer(string hint)
	{
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.WriteLine(hint);
		Console.ResetColor();
	}

	// Proste, jednowymiarowe menu (jak wcześniej)
	public static void DrawList(IReadOnlyList<string> items, int selectedIndex)
	{
		for (int i = 0; i < items.Count; i++)
		{
			bool selected = i == selectedIndex;

			if (selected)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.Cyan;
				Console.Write("▶ ");
			}
			else
			{
				Console.ResetColor();
				Console.Write("  ");
			}

			Console.WriteLine(items[i]);
			Console.ResetColor();
		}
	}

	// GRID: maxRowsPerCol wierszy, potem nowa kolumna
	public static void DrawGrid(IReadOnlyList<string> items, int selectedIndex, int maxRowsPerCol, int colSpacing = 6)
	{
		if (items.Count == 0) return;

		int rows = Math.Max(1, maxRowsPerCol);
		int cols = (int)Math.Ceiling(items.Count / (double)rows);

		int itemWidth = items.Max(s => s.Length) + 2; // +2 na "▶ " / "  "
		int colWidth = itemWidth + colSpacing;

		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				int idx = c * rows + r;
				if (idx >= items.Count) continue;

				bool selected = idx == selectedIndex;

				// ustawiamy kursor na początek "komórki"
				Console.SetCursorPosition(c * colWidth, Console.CursorTop);

				if (selected)
				{
					Console.ForegroundColor = ConsoleColor.Black;
					Console.BackgroundColor = ConsoleColor.Cyan;
					Console.Write("▶ ");
				}
				else
				{
					Console.ResetColor();
					Console.Write("  ");
				}

				// dopisujemy treść i dopełniamy do szerokości
				string text = items[idx];
				Console.Write(text.PadRight(itemWidth - 2));
				Console.ResetColor();
			}

			Console.WriteLine();
		}
	}

	// Pomocnicze: ruch w gridzie (rows = maxRowsPerCol)
	public static int MoveGridIndex(int current, ConsoleKey key, int count, int rows)
	{
		if (count <= 0) return 0;

		rows = Math.Max(1, rows);

		int col = current / rows;
		int row = current % rows;

		int cols = (int)Math.Ceiling(count / (double)rows);

		int newCol = col;
		int newRow = row;

		switch (key)
		{
			case ConsoleKey.UpArrow: newRow = row - 1; break;
			case ConsoleKey.DownArrow: newRow = row + 1; break;
			case ConsoleKey.LeftArrow: newCol = col - 1; break;
			case ConsoleKey.RightArrow: newCol = col + 1; break;
			default: return current;
		}

		// wrap kolumn
		if (newCol < 0) newCol = cols - 1;
		if (newCol >= cols) newCol = 0;

		// wrap wierszy
		if (newRow < 0) newRow = rows - 1;
		if (newRow >= rows) newRow = 0;

		// próbujemy trafić w newCol/newRow
		int candidate = newCol * rows + newRow;

		// jeśli w tej kolumnie nie ma takiego wiersza (bo ostatnia kolumna jest "krótsza"),
		// cofamy wiersz w górę aż trafimy na istniejący element
		while (candidate >= count && newRow > 0)
		{
			newRow--;
			candidate = newCol * rows + newRow;
		}

		// jeśli dalej nie pasuje (np. kolumna pusta – rzadkie przy obliczaniu cols), wróć na current
		if (candidate >= count) return current;

		return candidate;
	}
}