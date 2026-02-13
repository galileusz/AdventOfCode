using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day07.Part2;

public class Solver : BaseResolver, IAdventOfCode
{
	private long _result = 0;

	public override string Solve(string input)
	{
		var lines = input.Split("\n");

		Process(lines);

		return _result.ToString();
	}

	private void Process(string[] lines)
	{
		var beamIndexes = new List<(int, long)>
				{
						(lines[0].IndexOf('S'), 1)
				};

		for (int i = 1; i < lines.Length; ++i)
		{
			var newBeamIndexes = new List<(int, long)>();
			for (int j = 0; j < beamIndexes.Count(); ++j)
			{
				var beamIndex = beamIndexes[j];
				if (lines[i][beamIndex.Item1] == '^')
				{
					newBeamIndexes.Add((beamIndex.Item1 - 1, beamIndex.Item2));
					newBeamIndexes.Add((beamIndex.Item1 + 1, beamIndex.Item2));
				}
				else
				{
					newBeamIndexes.Add(beamIndex);
				}
			}
			beamIndexes = GetCalculatedBeams(newBeamIndexes);
		}

		_result = beamIndexes.Select(x => x.Item2).Sum();
	}

	private List<(int, long)> GetCalculatedBeams(List<(int, long)> beamIndexes)
	{
		var uniqueIndexes = beamIndexes.Select(i => i.Item1).Distinct();
		var result = new List<(int, long)>();

		foreach (var index in uniqueIndexes)
		{
			var posibilities = beamIndexes.Where(x => index == x.Item1).Sum(x => x.Item2);
			result.Add((index, posibilities));
		}

		return result;
	}
}