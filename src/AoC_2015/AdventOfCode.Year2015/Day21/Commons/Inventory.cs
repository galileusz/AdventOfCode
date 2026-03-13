namespace AdventOfCode.Year2015.Day21.Commons;

internal static class Inventory
{
	public static readonly Item[] Weapons =
	[
			new Item(8, 4, 0),
			new Item(10, 5, 0),
			new Item(25, 6, 0),
			new Item(40, 7, 0),
			new Item(74, 8, 0),
	];

	public static readonly Item[] Armors =
	[	
			new Item(13, 0, 1),
			new Item(31, 0, 2),
			new Item(53, 0, 3),
			new Item(75, 0, 4),
			new Item(102, 0, 5),
	];

	public static readonly Item[] Rings =
	[
			new Item(25, 1, 0),
			new Item(50, 2, 0),
			new Item(100, 3, 0),
			new Item(20, 0, 1),
			new Item(40, 0, 2),
			new Item(80, 0, 3),
	];
}
