using AdventOfCode.Commons.Helpers;

namespace AdventOfCodeManager.ConsoleCommons;

public sealed class PartOfDayMenuState : ArrowMenuState
{
	private readonly int _year;
	private readonly int _day;

	private readonly string[] _parts;
	private Dictionary<string, Type> _partToSolverMap;

	protected override string Title => $"Menu 3/3: Choose Part (Year: {_year}, Day: {_day})";
	protected override IReadOnlyList<string> Items => _parts;

	protected override NavAction OnEnter(int selectedIndex)
	{
		var selected = _partToSolverMap[_parts[selectedIndex]];
		return NavAction.Push(new ResultState(selected));
	}

	public PartOfDayMenuState(int year, int day, List<Type> solvers)
	{
		_year = year;
		_day = day;

		_parts = InitializeParts(solvers);
		_partToSolverMap = InitializePartToSolverMap(solvers);
	}

	private Dictionary<string, Type> InitializePartToSolverMap(List<Type> solvers)
	{
		var solversByPart = new Dictionary<string, Type>();

		foreach (var solver in solvers)
		{
			var day = AssemblySearcher.GetPartFromNamespace(solver);
			if (day.HasValue)
			{
				solversByPart[$"Part_{day}"] = solver;
			}
		}

		return solversByPart;
	}

	private string[] InitializeParts(List<Type> solvers)
	{
		return solvers
			.Select(AssemblySearcher.GetDayFromNamespace)
			.Where(y => y.HasValue)
			.Select(y => y!.Value)
			.Distinct()
			.OrderBy(y => y).Select(p => $"Part_{p}")
			.ToArray();
	}
}
