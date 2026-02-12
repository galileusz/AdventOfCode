using AdventOfCode.Commons;
using AdventOfCode.Commons.Helpers;
using System.Reflection;

namespace AdventOfCodeManager.ConsoleCommons;

public sealed class YearMenuState : ArrowGridMenuState
{
	private readonly int[] _years;
	private Dictionary<int, List<Type>> _solversByYear = [];

	protected override string Title => "Menu 1/3: Choose Year";
	protected override IReadOnlyList<string> Items => _years.Select(y => y.ToString()).ToArray();

	protected override int MaxRowsPerColumn => 10;

	protected override NavAction OnEnter(int selectedIndex)
	{
		var year = _years[selectedIndex];
		var solvers = _solversByYear[year];
		return NavAction.Push(new DayMenuState(year, solvers));
	}

	protected override NavAction OnBack() => NavAction.Quit();

	public YearMenuState() => _years = InitializeYears();

	private int[] InitializeYears()
	{
		var solvers = AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(a =>
			{
				try { return a.GetTypes(); }
				catch (ReflectionTypeLoadException ex) { return ex.Types.Where(x => x != null)!; }
			})
			.Where(t => t.IsClass && !t.IsAbstract && typeof(BaseResolver).IsAssignableFrom(t))
			.ToList();

		_solversByYear = InitializeSolversByYear(solvers);

		return solvers
			.Select(AssemblySearcher.GetYearFromNamespace)
			.Where(y => y.HasValue)
			.Select(y => y!.Value)
			.Distinct()
			.OrderBy(y => y)
			.ToArray();
	}

	private Dictionary<int, List<Type>> InitializeSolversByYear(List<Type> solvers)
	{
		var solversByYear = new Dictionary<int, List<Type>>();

		foreach (var solver in solvers)
		{
			var year = AssemblySearcher.GetYearFromNamespace(solver);
			if (year.HasValue)
			{
				if (!solversByYear.ContainsKey(year.Value))
				{
					solversByYear[year.Value] = new List<Type>();
				}
				solversByYear[year.Value].Add(solver);
			}
		}

		return solversByYear;
	}
}