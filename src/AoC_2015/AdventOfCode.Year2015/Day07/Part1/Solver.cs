using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day07.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	private static readonly Dictionary<string, Func<uint>> _wiresLogicDictionary = new();
	public override string Solve(string input)
	{
		_wiresLogicDictionary.Clear();
		var span = input.AsSpan().Trim();
		var instructions = span.Split('\n');

		foreach (var range in instructions)
		{
			var instruction = span[range];
			CreateWireLogic(instruction);
		}

		return _wiresLogicDictionary.TryGetValue("a", out var func) ? func().ToString() : "0";
	}

	private void CreateWireLogic(ReadOnlySpan<char> instruction)
	{
		var resultIndex = instruction.IndexOf("->");
		var resultName = instruction[(resultIndex + 2)..].Trim().ToString();

		var indexOfAnd = instruction.IndexOf("AND");
		if (indexOfAnd != -1)
		{
			AddAND(resultName, instruction, indexOfAnd, resultIndex);
			return;
		}

		var indexOfOr = instruction.IndexOf("OR");
		if (indexOfOr != -1)
		{
			AddOR(resultName, instruction, indexOfOr, resultIndex);
			return;
		}

		var indexOfLShift = instruction.IndexOf("LSHIFT");
		if (indexOfLShift != -1)
		{
			AddLShift(resultName, instruction, indexOfLShift, resultIndex);
			return;
		}

		var indexOfRShift = instruction.IndexOf("RSHIFT");
		if (indexOfRShift != -1)
		{
			AddRShift(resultName, instruction, indexOfRShift, resultIndex);
			return;
		}

		if (instruction.StartsWith("NOT"))
		{
			AddNOT(resultName, instruction, resultIndex);
			return;
		}

		AddDirect(resultName, instruction, resultIndex);

	}

	private void AddDirect(string resultName, ReadOnlySpan<char> instruction, int resultIndex)
	{
		var wire = instruction[..(resultIndex - 1)].ToString();
		if (uint.TryParse(wire, out var value))
		{
			_wiresLogicDictionary[resultName] = () => 
			{
				Console.WriteLine($"{value} -> {resultName}");
				return value;
			};
		}
		else
		{
			_wiresLogicDictionary[resultName] = () =>
			{
				Console.WriteLine($"{wire} -> {resultName}");
				var wireResult = _wiresLogicDictionary.TryGetValue(wire, out var func) ? func : () => 0;
				_wiresLogicDictionary[resultName] = () => wireResult();
				return wireResult();
			};
		}
	}

	private void AddNOT(string resultName, ReadOnlySpan<char> instruction, int resultIndex)
	{
		var wire = instruction[4..(resultIndex - 1)].ToString();

		if (uint.TryParse(wire, out var value))
		{
			Console.WriteLine($"NOT {value} -> {resultName}");

			_wiresLogicDictionary[resultName] = () => ~value;
			return;
		}

		_wiresLogicDictionary[resultName] = () =>
		{
			Console.WriteLine($"NOT {wire} -> {resultName}");

			var wireResult = _wiresLogicDictionary.TryGetValue(wire, out var func) ? func : () => 0;
			_wiresLogicDictionary[resultName] = () => ~wireResult();
			return ~wireResult();
		};
	}

	private void AddLShift(string resultName, ReadOnlySpan<char> instruction, int indexOfLShift, int resultIndex)
	{
		var wire = instruction[..(indexOfLShift - 1)].ToString();
		var shiftValue = int.Parse(instruction[(indexOfLShift + 7)..(resultIndex - 1)]);

		if (uint.TryParse(wire, out var value))
		{
			_wiresLogicDictionary[resultName] = () =>
			{
				Console.WriteLine($"{value} LSHIFT {shiftValue} -> {resultName}");
				_wiresLogicDictionary[resultName] = () => value << shiftValue;
				return value << shiftValue;
			};
			return;
		}

		_wiresLogicDictionary[resultName] = () =>
		{
			Console.WriteLine($"{wire} LSHIFT {shiftValue} -> {resultName}");
			var wireResult = _wiresLogicDictionary.TryGetValue(wire, out var func) ? func : () => 0;
			_wiresLogicDictionary[resultName] = () => wireResult() << shiftValue;
			return wireResult() << shiftValue;
		};
	}

	private void AddRShift(string resultName, ReadOnlySpan<char> instruction, int indexOfRShift, int resultIndex)
	{
		var wire = instruction[..(indexOfRShift - 1)].ToString();
		var shiftValue = int.Parse(instruction[(indexOfRShift + 7)..(resultIndex - 1)]);

		if (uint.TryParse(wire, out var value))
		{
			_wiresLogicDictionary[resultName] = () =>
			{
				Console.WriteLine($"{value} RSHIFT {shiftValue} -> {resultName}");
				_wiresLogicDictionary[resultName] = () => value >> shiftValue;
				return value >> shiftValue;
			};
			return;
		}

		_wiresLogicDictionary[resultName] = () =>
		{
			Console.WriteLine($"{wire} RSHIFT {shiftValue} -> {resultName}");
			var wireResult = _wiresLogicDictionary.TryGetValue(wire, out var func) ? func : () => 0;
			_wiresLogicDictionary[resultName] = () => wireResult() >> shiftValue;
			return wireResult() >> shiftValue;
		};
	}

	private void AddAND(string resultName, ReadOnlySpan<char> instruction, int indexOfAnd, int resultIndex)
	{
		var wire1 = instruction[..(indexOfAnd - 1)].ToString();
		var wire2 = instruction[(indexOfAnd + 4)..(resultIndex - 1)].ToString();

		if (uint.TryParse(wire1, out var value1))
		{
			_wiresLogicDictionary[resultName] = () =>
			{
				Console.WriteLine($"{value1} AND {wire2} -> {resultName}");
				var wire2Result = _wiresLogicDictionary.TryGetValue(wire2, out var func2) ? func2 : () => 0;
				_wiresLogicDictionary[resultName] = () => value1 & wire2Result();
				return value1 & wire2Result();
			};
			return;
		}

		if (uint.TryParse(wire2, out var value2))
		{
			_wiresLogicDictionary[resultName] = () =>
			{
				Console.WriteLine($"{wire1} AND {value2} -> {resultName}");
				var wire1Result = _wiresLogicDictionary.TryGetValue(wire1, out var func1) ? func1 : () => 0;
				_wiresLogicDictionary[resultName] = () => wire1Result() & value2;
				return wire1Result() & value2;
			};
			return;
		}

		_wiresLogicDictionary[resultName] = () =>
		{
			Console.WriteLine($"{wire1} AND {wire2} -> {resultName}");
			var wire1Result = _wiresLogicDictionary.TryGetValue(wire1, out var func1) ? func1 : () => 0;
			var wire2Result = _wiresLogicDictionary.TryGetValue(wire2, out var func2) ? func2 : () => 0;
			_wiresLogicDictionary[resultName] = () => wire1Result() & wire2Result();
			return wire1Result() & wire2Result();
		};
	}

	private void AddOR(string resultName, ReadOnlySpan<char> instruction, int indexOfOr, int resultIndex)
	{
		var wire1 = instruction[..(indexOfOr - 1)].ToString();
		var wire2 = instruction[(indexOfOr + 3)..(resultIndex - 1)].ToString();

		if (uint.TryParse(wire1, out var value1))
		{
			_wiresLogicDictionary[resultName] = () =>
			{
				Console.WriteLine($"{value1} OR {wire2} -> {resultName}");

				var wire2Result = _wiresLogicDictionary.TryGetValue(wire2, out var func2) ? func2 : () => 0;
				_wiresLogicDictionary[resultName] = () => value1 | wire2Result();

				return value1 | wire2Result();
			};
			return;
		}

		if (uint.TryParse(wire2, out var value2))
		{
			_wiresLogicDictionary[resultName] = () =>
			{
				Console.WriteLine($"{wire1} OR {value2} -> {resultName}");

				var wire1Result = _wiresLogicDictionary.TryGetValue(wire1, out var func1) ? func1 : () => 0;
				_wiresLogicDictionary[resultName] = () => wire1Result() | value2;

				return wire1Result() | value2;
			};
			return;
		}

		_wiresLogicDictionary[resultName] = () =>
		{
			Console.WriteLine($"{wire1} AND {wire2} -> {resultName}");

			var wire1Result = _wiresLogicDictionary.TryGetValue(wire1, out var func) ? func : () => 0;
			var wire2Result = _wiresLogicDictionary.TryGetValue(wire2, out var func2) ? func2 : () => 0;

			_wiresLogicDictionary[resultName] = () => wire1Result() | wire2Result();
			return wire1Result() | wire2Result();
		};
	}
}
