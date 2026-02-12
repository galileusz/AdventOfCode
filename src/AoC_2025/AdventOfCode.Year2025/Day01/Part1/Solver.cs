using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day01.Part1;

public class Solver : BaseResolver, IAdventOfCode
{
	private int _safePosition = 50;
	private int _zeroPositionCounter = 0;

	public override string Solve(string input)
	{
		var movesString = input.Split('\n');
		foreach (var move in movesString)
			RotatorMove(move);

		return _zeroPositionCounter.ToString();
	}

	private void RotatorMove(string move)
	{
		if (move.First() == 'L')
			RotatorMoveLeft(move);
		else
			RotatorMoveRight(move);

		Console.WriteLine($"Move: {move}, Position: {_safePosition}, {(_safePosition == 0 ? "TO ZERO" : "")}");

		if (_safePosition == 0)
			_zeroPositionCounter++;
	}

	private void RotatorMoveLeft(string move)
	{
		var value = Convert.ToInt32(move[1..]);
		_safePosition -= value % 100;
		if (_safePosition < 0)
			_safePosition += 100;
	}

	private void RotatorMoveRight(string move)
	{
		var value = Convert.ToInt32(move[1..]);
		_safePosition += value;
		if (_safePosition > 99)
			_safePosition %= 100;
	}
}
