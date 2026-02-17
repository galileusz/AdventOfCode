using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day07.Common;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day07.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	private readonly Dictionary<string, WireInstruction> _wiresLogicDictionary = new();
	private uint? _resultPart1;
	public override string Solve(string input)
	{
		if (_resultPart1 == null)
		{
			var solver1 = new Part1.Solver();
			var result1 = solver1.Solve(input);
			_resultPart1 = uint.Parse(result1);
		}

		_wiresLogicDictionary.Clear();
		_wiresLogicDictionary["b"] = new WireInstruction(() => _resultPart1.Value);

		var span = input.AsSpan().Trim();
		var instructions = span.Split('\n');

		foreach (var range in instructions)
		{
			var instruction = span[range];
			CreateWireLogic(instruction);
		}

		return _wiresLogicDictionary.TryGetValue("a", out var inst) ? inst.GetValue().ToString() : "0";
	}

	private void CreateWireLogic(ReadOnlySpan<char> instruction)
	{
		var resultIndex = instruction.IndexOf("->");
		var resultName = instruction[(resultIndex + 2)..].Trim().ToString();
		if (resultName == "b")
			return;

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
			_wiresLogicDictionary[resultName] = new WireInstruction(() => 
			{
				
				return value;
			});
		}
		else
		{
			_wiresLogicDictionary[resultName] = new WireInstruction(() =>
			{
				
				return _wiresLogicDictionary[wire].GetValue();
			});
		}
	}

	private void AddNOT(string resultName, ReadOnlySpan<char> instruction, int resultIndex)
	{
		var wire = instruction[4..(resultIndex - 1)].ToString();

		if (uint.TryParse(wire, out var value))
		{
			

			_wiresLogicDictionary[resultName] = new WireInstruction(() => ~value);
			return;
		}

		_wiresLogicDictionary[resultName] = new WireInstruction(() =>
		{
			

			var wireResult = _wiresLogicDictionary[wire].GetValue();
			return ~wireResult;
		});
	}

	private void AddLShift(string resultName, ReadOnlySpan<char> instruction, int indexOfLShift, int resultIndex)
	{
		var wire = instruction[..(indexOfLShift - 1)].ToString();
		var shiftValue = int.Parse(instruction[(indexOfLShift + 7)..(resultIndex - 1)]);

		if (uint.TryParse(wire, out var value))
		{
			_wiresLogicDictionary[resultName] = new WireInstruction(() =>
			{
				
				return value << shiftValue;
			});
			return;
		}

		_wiresLogicDictionary[resultName] = new WireInstruction(() =>
		{
			
			var wireResult = _wiresLogicDictionary[wire].GetValue();
			return wireResult << shiftValue;
		});
	}

	private void AddRShift(string resultName, ReadOnlySpan<char> instruction, int indexOfRShift, int resultIndex)
	{
		var wire = instruction[..(indexOfRShift - 1)].ToString();
		var shiftValue = int.Parse(instruction[(indexOfRShift + 7)..(resultIndex - 1)]);

		if (uint.TryParse(wire, out var value))
		{
			_wiresLogicDictionary[resultName] = new WireInstruction(() =>
			{
				
				return value >> shiftValue;
			});
			return;
		}

		_wiresLogicDictionary[resultName] = new WireInstruction(() =>
		{
			
			var wireResult = _wiresLogicDictionary[wire].GetValue();
			return wireResult >> shiftValue;
		});
	}

	private void AddAND(string resultName, ReadOnlySpan<char> instruction, int indexOfAnd, int resultIndex)
	{
		var wire1 = instruction[..(indexOfAnd - 1)].ToString();
		var wire2 = instruction[(indexOfAnd + 4)..(resultIndex - 1)].ToString();

		if (uint.TryParse(wire1, out var value1))
		{
			_wiresLogicDictionary[resultName] = new WireInstruction(() =>
			{
				
				var wire2Result = _wiresLogicDictionary[wire2].GetValue();
				return value1 & wire2Result;
			});
			return;
		}

		if (uint.TryParse(wire2, out var value2))
		{
			_wiresLogicDictionary[resultName] = new WireInstruction(() =>
			{	
				var wire1Result = _wiresLogicDictionary[wire1].GetValue();
				return wire1Result & value2;
			});
			return;
		}

		_wiresLogicDictionary[resultName] = new WireInstruction(() =>
		{
			
			var wire1Result = _wiresLogicDictionary[wire1].GetValue();
			var wire2Result = _wiresLogicDictionary[wire2].GetValue();
			return wire1Result & wire2Result;
		});
	}

	private void AddOR(string resultName, ReadOnlySpan<char> instruction, int indexOfOr, int resultIndex)
	{
		var wire1 = instruction[..(indexOfOr - 1)].ToString();
		var wire2 = instruction[(indexOfOr + 3)..(resultIndex - 1)].ToString();

		if (uint.TryParse(wire1, out var value1))
		{
			_wiresLogicDictionary[resultName] = new WireInstruction(() =>
			{
				var wire2Result = _wiresLogicDictionary[wire2].GetValue();

				return value1 | wire2Result;
			});
			return;
		}

		if (uint.TryParse(wire2, out var value2))
		{
			_wiresLogicDictionary[resultName] = new WireInstruction(() =>
			{
				var wire1Result = _wiresLogicDictionary[wire1].GetValue();

				return wire1Result | value2;
			});
			return;
		}

		_wiresLogicDictionary[resultName] = new WireInstruction(() =>
		{
			var wire1Result = _wiresLogicDictionary[wire1].GetValue();
			var wire2Result = _wiresLogicDictionary[wire2].GetValue();

			return wire1Result | wire2Result;
		});
	}
}
