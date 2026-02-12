using AdventOfCode.Commons.Helpers;

namespace AdventOfCodeManager.ConsoleCommons;

public sealed class DayMenuState : ArrowGridMenuState
{
	private readonly int _year;
	private readonly int[] _days = Enumerable.Range(1, 31).ToArray();
	private Dictionary<int, List<Type>> _solversByDay;

	protected override string Title => $"Menu 2/3: Choose Day (Year: {_year})";
	protected override IReadOnlyList<string> Items => _days.Select(d => d.ToString("00")).ToArray();
	protected override int MaxRowsPerColumn => 10;

	protected override NavAction OnEnter(int selectedIndex)
	{ 
		var day = _days[selectedIndex];
		var solvers = _solversByDay[day];
		return NavAction.Push(new PartOfDayMenuState(_year, day, solvers));
	}

	public DayMenuState(int year, List<Type> solvers)
	{
		_year = year;

		_days = InitializeDays(solvers);

		_solversByDay = InitializeSolversByDay(solvers);
	}

	private int[] InitializeDays(List<Type> solvers)
	{
		return solvers
			.Select(AssemblySearcher.GetDayFromNamespace)
			.Where(y => y.HasValue)
			.Select(y => y!.Value)
			.Distinct()
			.OrderBy(y => y)
			.ToArray();
	}

	private Dictionary<int, List<Type>> InitializeSolversByDay(List<Type> solvers)
	{
		var solversByDay = new Dictionary<int, List<Type>>();

		foreach (var solver in solvers)
		{
			var day = AssemblySearcher.GetDayFromNamespace(solver);
			if (day.HasValue)
			{
				if (!solversByDay.ContainsKey(day.Value))
				{
					solversByDay[day.Value] = new List<Type>();
				}
				solversByDay[day.Value].Add(solver);
			}
		}

		return solversByDay;
	}
}