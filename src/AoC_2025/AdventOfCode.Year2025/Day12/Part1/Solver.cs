using AdventOfCode.Commons;
using AdventOfCode.Year2025.Day12.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day12.Part1;

public class Solver : BaseResolver, IAdventOfCode
{
	private Dictionary<int, PresentBox> _presentsDictionary = new Dictionary<int, PresentBox>();
	private List<PackageProblem> _problems;

	private int _result = 0;

	public override string Solve(string input)
	{
		var lines = input.Split("\n");

		Process(lines);

		return _result.ToString();
	}

	private void Process(string[] lines)
	{
		_result = 0;
		_presentsDictionary = new Dictionary<int, PresentBox>();
		_problems = new List<PackageProblem>();

		bool startShape = false;
		int currentShapeId = 0;
		var currentShape = new List<string>();
		foreach (var line in lines)
		{
			if (startShape)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					startShape = false;
					_presentsDictionary.Add(currentShapeId, GeneratePresentBox(currentShapeId, currentShape));
					currentShape = new List<string>();
					continue;
				}
				currentShape.Add(line);
				continue;
			}
			if (line.Contains(':') && false == line.Contains('x'))
			{
				startShape = true;
				currentShapeId = Convert.ToInt32(line.Substring(0, line.IndexOf(':')));
				continue;
			}
			if (line.Contains('x'))
			{
				_problems.Add(new PackageProblem(line));
			}
		}

		var trivials = _problems.Where(x => x.IsTrivial).ToList();
		var impossibles = _problems.Where(x => false == x.IsPossible(_presentsDictionary)).ToList();

		if (trivials.Count() + impossibles.Count() == _problems.Count())
			_result = trivials.Count();
		else
			Console.WriteLine("\nTo nie jest trywialny problem,\nNie mam rozwiązania\n");
	}

	private PresentBox GeneratePresentBox(int currentShapeId, List<string> currentShape)
	{
		var width = currentShape[0].Length;
		var height = currentShape.Count();

		var shape = new bool[height, width];

		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				shape[i, j] = currentShape[i][j] == '#';
			}
		}

		return new PresentBox(currentShapeId, shape);
	}
}
