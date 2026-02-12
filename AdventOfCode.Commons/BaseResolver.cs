using AdventOfCode.Commons.Helpers;
using AdventOfCodeGate.Interfaces;
using System.Diagnostics;

namespace AdventOfCode.Commons;

public abstract class BaseResolver : IResolver, IAdventOfCode
{
	private const string _testDataPath = @"..\..\..\..\testData";

	private int _year;
	private int _day;
	private int _part;
	private string? _getDataInputPath;
	private string? _getDataResultPath;
	public abstract string Solve(string input);

	protected BaseResolver()
	{
		var type = GetType();

		_year = AssemblySearcher.GetYearFromNamespace(type) ?? throw new InvalidOperationException("Year not found in namespace");
		_day = AssemblySearcher.GetDayFromNamespace(type) ?? throw new InvalidOperationException("Day not found in namespace");
		_part = AssemblySearcher.GetPartFromNamespace(type) ?? throw new InvalidOperationException("Part not found in namespace");

		InitializeTestData();
	}

	public void Resolve()
	{
		Console.WriteLine();
		Console.WriteLine($"--------------------{NameOfResolver}--------------------");

		if (_getDataInputPath == null)
		{
			Console.WriteLine($"No test data found. Expected at: {_testDataPath}");
			Console.WriteLine($"-----------------------------------------------------");
			return;
		}

		Console.WriteLine($"{NameOfResolver} - Process Started");
		var input = File.ReadAllText(_getDataInputPath);

		var timer = Stopwatch.StartNew();
		var result = Solve(input);
		timer.Stop();

		Console.WriteLine($"{NameOfResolver} - Result: {result}");
		Console.WriteLine($"{NameOfResolver} - Process Finished in {timer.Elapsed.TotalSeconds} seconds");

		if (_getDataResultPath != null)
		{
			var expectedResult = File.ReadAllText(_getDataResultPath).Trim();
			if (result.Trim() == expectedResult)
				Console.WriteLine($"{NameOfResolver} - ✅ Result is correct");
			else
				Console.WriteLine($"{NameOfResolver} - ❌ Result is incorrect. Expected: {expectedResult}");
		}

		Console.WriteLine($"-----------------------------------------------------");
	}

	private string NameOfResolver =>
			$"YEAR_{_year}_DAY_{_day:00}_PART_{_part}";

	private void InitializeTestData()
	{
		var directoryPath = Path.Combine(AppContext.BaseDirectory, _testDataPath);
		var yearDirectory = Directory.GetDirectories(directoryPath).FirstOrDefault(d => d.Contains($"Year{_year}"));

		if (yearDirectory == null)
			return;

		var inputDataPath = Path.Combine(directoryPath, yearDirectory, $"Day{_day:00}_DataInput.txt");
		if (File.Exists(inputDataPath))
			_getDataInputPath = inputDataPath;

		var resultDataPath = Path.Combine(directoryPath, yearDirectory, $"Day{_day:00}_Part{_part}_Result.txt");
		if (File.Exists(resultDataPath))
			_getDataResultPath = resultDataPath;
	}
}