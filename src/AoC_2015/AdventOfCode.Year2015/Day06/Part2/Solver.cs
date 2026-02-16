using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day06.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day06.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	private readonly Dictionary<Position, Part2Light> _lightsDictionary;

	public Solver()
	{
		_lightsDictionary = new Dictionary<Position, Part2Light>();

		for (int x = 0; x < 1000; x++)
		{
			for (int y = 0; y < 1000; y++)
			{
				var position = new Position(x, y);
				_lightsDictionary[position] = new Part2Light(x, y);
			}
		}
	}

	public override string Solve(string input)
	{
		foreach (var light in _lightsDictionary.Values)
			light.Reset();

		var span = input.AsSpan().Trim();

		foreach (var range in span.Split('\n'))
		{
			ProcessInstruction(span[range]);
		}

		return _lightsDictionary.Sum(x => x.Value.Brightness).ToString();
	}

	private void ProcessInstruction(ReadOnlySpan<char> instruction)
	{
		var indexThrough = instruction.IndexOf("th");

		if (instruction.StartsWith("to"))
			ProcessToggle(instruction, indexThrough);
		if (instruction.StartsWith("turn on"))
			ProcessTurnOn(instruction, indexThrough);
		if (instruction.StartsWith("turn of"))
			ProcessTurnOff(instruction, indexThrough);
	}

	private void ProcessToggle(ReadOnlySpan<char> instruction, int indexThrough)
	{
		var position1 = instruction[7..(indexThrough - 1)];
		var position2 = GetPosition2(instruction, indexThrough);

		(int x1, int x2, int y1, int y2) = GetPositions(position1, position2);

		Parallel.For(x1, x2 + 1, x =>
		{
			for (int y = y1; y <= y2; y++)
			{
				var position = new Position(x, y);
				_lightsDictionary[position].Toggle();
			}
		});
	}

	private void ProcessTurnOn(ReadOnlySpan<char> instruction, int indexThrough)
	{
		var position1 = instruction[8..(indexThrough - 1)];
		var position2 = GetPosition2(instruction, indexThrough);

		(int x1, int x2, int y1, int y2) = GetPositions(position1, position2);

		Parallel.For(x1, x2 + 1, x =>
		{
			for (int y = y1; y <= y2; y++)
			{
				var position = new Position(x, y);
				_lightsDictionary[position].TurnOn();
			}
		});
	}

	private void ProcessTurnOff(ReadOnlySpan<char> instruction, int indexThrough)
	{
		var position1 = instruction[9..(indexThrough - 1)];
		var position2 = GetPosition2(instruction, indexThrough);

		(int x1, int x2, int y1, int y2) = GetPositions(position1, position2);

		Parallel.For(x1, x2 + 1, x =>
		{
			for (int y = y1; y <= y2; y++)
			{
				var position = new Position(x, y);
				_lightsDictionary[position].TurnOff();
			}
		});
	}

	private ReadOnlySpan<char> GetPosition2(ReadOnlySpan<char> instruction, int indexThrough)
	{
		return instruction[(indexThrough + 8)..];
	}

	private (int x1, int x2, int y1, int y2) GetPositions(
		ReadOnlySpan<char> position1,
		ReadOnlySpan<char> position2)
	{
		var position1Parts = position1.Split(',');
		var position2Parts = position2.Split(',');

		position1Parts.MoveNext();
		position2Parts.MoveNext();
		var x1 = int.Parse(position1[position1Parts.Current]);
		var x2 = int.Parse(position2[position2Parts.Current]);
		position1Parts.MoveNext();
		position2Parts.MoveNext();
		var y1 = int.Parse(position1[position1Parts.Current]);
		var y2 = int.Parse(position2[position2Parts.Current]);

		return (x1, x2, y1, y2);
	}
}
