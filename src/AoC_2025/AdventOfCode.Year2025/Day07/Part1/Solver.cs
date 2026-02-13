using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day07.Part1;

public class Solver : BaseResolver, IAdventOfCode
{
	private int _result = 0;

	public override string Solve(string input)
	{
		var lines = input.Split("\n");

		Process(lines);

		return _result.ToString();
	}

	private void Process(string[] lines)
	{
		var beamIndexes = new List<int>
				{
						lines[0].IndexOf('S')
				};

		for (int i = 1; i < lines.Length; ++i)
		{
			var newBeamIndexes = new List<int>();
			for (int j = 0; j < beamIndexes.Count(); ++j)
			{
				var beamIndex = beamIndexes[j];
				if (lines[i][beamIndex] == '^')
				{
					_result++;
					newBeamIndexes.Add(beamIndex - 1);
					newBeamIndexes.Add(beamIndex + 1);
				}
				else
				{
					newBeamIndexes.Add(beamIndex);
				}
			}
			beamIndexes = newBeamIndexes.Distinct().ToList();
		}
	}
}
