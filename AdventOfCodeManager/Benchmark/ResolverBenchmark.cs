using AdventOfCode.Commons;
using BenchmarkDotNet.Attributes;

namespace AdventOfCodeManager.Benchmark;

[MemoryDiagnoser]
public class ResolverBenchmark
{
	private BaseResolver _solver = null!;
	private string _input;

	[IterationSetup]
	public void Setup()
	{
		var typeName = Environment.GetEnvironmentVariable("AOC_SOLVER_TYPE")
				?? throw new InvalidOperationException("AOC_SOLVER_TYPE is not set.");

		// Najlepiej: pełna nazwa typu + assembly, np. "X.Y.Day01Resolver, AdventOfCode.Solvers"
		var type = Type.GetType(typeName, throwOnError: false);

		if (type is null)
			throw new InvalidOperationException($"Cannot resolve type from AOC_SOLVER_TYPE: '{typeName}'");

		_solver = (BaseResolver)(Activator.CreateInstance(type)
				?? throw new InvalidOperationException($"Cannot create {type.FullName}"));

		_input = File.ReadAllText(_solver.DataInputPath ?? throw new ArgumentNullException("input path is null"));
	}

	[Benchmark]
	public void Solve() => _solver.Solve(_input);
}