using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2015.Day09.Common;

internal struct CitiesPair(string firstCity, string secondCity)
{
	public string FirstCity { get; set; } = firstCity;
	public string SecondCity { get; set; } = secondCity;

	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		if (obj == null)
			return false;

		if (obj is CitiesPair cp)
		{
			return (this.FirstCity == cp.FirstCity && this.SecondCity == cp.SecondCity) ||
						 (this.FirstCity == cp.SecondCity && this.SecondCity == cp.FirstCity);
		}

		return false;
	}

	public override int GetHashCode()
	{
		return 0;
	}
}
