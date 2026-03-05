using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day16.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var span = input.AsSpan().Trim();
		var lines = span.Split('\n');

		var resultIndex = 0;
		foreach (var line in lines)
		{
			resultIndex++;
			if (IsCorrectSue(span[line]))
				break;
		}

		return resultIndex.ToString();
	}

	private bool IsCorrectSue(ReadOnlySpan<char> span)
	{
		if (!ChildrenOk(span))
			return false;

		if (!CatsOk(span))
			return false;

		if (!SamoyedsOk(span))
			return false;

		if (!PomeraniansOk(span))
			return false;

		if (!AkitasOk(span))
			return false;

		if (!VizslasOk(span))
			return false;

		if (!GoldfishOk(span))
			return false;

		if (!TreesOk(span))
			return false;

		if (!CarsOk(span))
			return false;

		if (!PerfumesOk(span))
			return false;

		return true;
	}

	private bool ChildrenOk(ReadOnlySpan<char> span) => ValueOk(span, "children: ", '3');

	private bool CatsOk(ReadOnlySpan<char> span) => ValueOk(span, "cats: ", '7');

	private bool SamoyedsOk(ReadOnlySpan<char> span) => ValueOk(span, "samoyeds: ", '2');

	private bool PomeraniansOk(ReadOnlySpan<char> span) => ValueOk(span, "pomeranians: ", '3');

	private bool AkitasOk(ReadOnlySpan<char> span) => ValueOk(span, "akitas: ", '0');

	private bool VizslasOk(ReadOnlySpan<char> span) => ValueOk(span, "vizslas: ", '0');

	private bool GoldfishOk(ReadOnlySpan<char> span) => ValueOk(span, "goldfish: ", '5');

	private bool TreesOk(ReadOnlySpan<char> span) => ValueOk(span, "trees: ", '3');

	private bool CarsOk(ReadOnlySpan<char> span) => ValueOk(span, "cars: ", '2');

	private bool PerfumesOk(ReadOnlySpan<char> span) => ValueOk(span, "perfumes: ", '1');

	private bool ValueOk(ReadOnlySpan<char> span, ReadOnlySpan<char> name, char expectedValue)
	{
		var index = span.IndexOf(name);
		if (index == -1)
			return true;

		return span[index + name.Length] == expectedValue;
	}
}
