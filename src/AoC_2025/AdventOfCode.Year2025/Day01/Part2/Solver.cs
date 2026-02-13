using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day01.Part2;

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
	}

	private void RotatorMoveLeft(string move)
	{
		var value = Convert.ToInt32(move[1..]);

		while (value > 100)
		{
			value -= 100;
			_zeroPositionCounter++;
		}

		if (_safePosition == 0)
			_zeroPositionCounter--;

		_safePosition -= value;
		if (_safePosition < 0)
		{
			_safePosition += 100;
			_zeroPositionCounter++;
		}

		if (_safePosition == 0)
			_zeroPositionCounter++;
	}

	private void RotatorMoveRight(string move)
	{
		var value = Convert.ToInt32(move[1..]);

		while (value > 100)
		{
			value -= 100;
			_zeroPositionCounter++;
		}

		_safePosition += value;
		if (_safePosition > 99)
		{
			_safePosition %= 100;
			_zeroPositionCounter++;
		}
	}
}
