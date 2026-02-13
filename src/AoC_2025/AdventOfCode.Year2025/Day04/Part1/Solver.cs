using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day04.Part1;

public class Solver : BaseResolver, IAdventOfCode
{
	private int _result = 0;

	public override string Solve(string input)
	{
		var rows = input.Split("\n");

		var rowsNumber = rows.Length;
		var rowLength = rows[0].Length;
		for (int i = 0; i < rowsNumber; i++)
		{
			for (int j = 0; j < rowLength; j++)
			{
				if (rows[i][j] == '@')
				{
					var neighbours = GetNeighbours(rows, i, j, rowsNumber, rowLength);
					if (neighbours < 4)
						_result++;
				}
			}
		}

		return _result.ToString();
	}

	private int GetNeighbours(string[] rows, int i, int j, int rowsNumber, int rowLength)
	{
		var result = -1;
		for (int ii = -1; ii <= 1; ii++)
		{
			for (int jj = -1; jj <= 1; jj++)
			{
				if (i + ii < 0 ||
								j + jj < 0 ||
								i + ii >= rowsNumber ||
								j + jj >= rowLength)
					continue;

				if (rows[i + ii][j + jj] == '@')
					result++;
			}
		}
		return result;
	}
}
