using AdventOfCode.Commons;
using AdventOfCodeManager.Benchmark;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace AdventOfCodeManager.ConsoleCommons;

public sealed class ResultState : IMenuState
{
	private BaseResolver _baseResolver;
	private bool _isBanchmark = false;

	public ResultState(Type solverType)
	{
		_baseResolver = solverType.GetConstructor(Type.EmptyTypes)?.Invoke(null) as BaseResolver
			?? throw new InvalidOperationException($"Cannot create instance of {solverType.FullName}");
	}

	public void Render()
	{
		Ui.Header("✅ Wybrano");

		if (!_isBanchmark)
			_baseResolver.Resolve();

		if (_isBanchmark)
		{
			var selectedType = _baseResolver.GetType();

			// ważne: ustaw string tak, żeby Type.GetType go znalazł:
			var typeId = $"{selectedType.FullName}, {selectedType.Assembly.GetName().Name}";
			Environment.SetEnvironmentVariable("AOC_SOLVER_TYPE", typeId);

			var job = Job.Default.WithWarmupCount(10).WithIterationCount(20);
			var config = DefaultConfig.Instance.AddJob(job);

			var summary = BenchmarkRunner.Run<ResolverBenchmark>(config); string benchInfo;

			if (summary.Reports.Count() == 0 || summary.Reports[0].ResultStatistics == null)
			{
				benchInfo = "Benchmark: FAILED (no reports)";
			}
			else
			{
				var report = summary.Reports[0];
				var stats = report.ResultStatistics;

				benchInfo =
		$"""Mean: {stats.Mean / 1_000_000.0:F3} ms Allocated: {GetAllocatedMemory(report)}""";
			}

			Console.Clear();
			Console.WriteLine(benchInfo);
			Console.WriteLine();
		}

		Ui.Footer("B wstecz | T benchmark | Esc wyjście");

		_isBanchmark = false;
	}

	public NavAction Handle(ConsoleKeyInfo key)
	{
		if (key.Key == ConsoleKey.Escape) return NavAction.Quit();
		if (key.Key == ConsoleKey.B) return NavAction.Pop();
		if (key.Key == ConsoleKey.T)
			_isBanchmark = true;

		return NavAction.Stay();
	}

	string GetAllocatedMemory(BenchmarkReport report)
	{
		var bytes = report.GcStats.GetBytesAllocatedPerOperation(report.BenchmarkCase);

		if (bytes < 1024)
			return $"{bytes} B";
		else if (bytes < 1024 * 1024)
			return $"{bytes / 1024.0:F2} KB";
		else if (bytes < 1024 * 1024 * 1024)
			return $"{bytes / (1024.0 * 1024.0):F2} MB";
		else
			return $"{bytes / (1024.0 * 1024.0 * 1024.0):F2} GB";
	}
}