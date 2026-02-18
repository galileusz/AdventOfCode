using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day09.Common;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day09.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	private Dictionary<CitiesPair, int> _distancesDictionary = new Dictionary<CitiesPair, int>();
	private string[] _cities;
	private List<int[]> _posibilities;
	public override string Solve(string input)
	{
		_distancesDictionary.Clear();
		_cities = [];
		_posibilities = new List<int[]>();

		var span = input.AsSpan().Trim();
		var distancesRanges = span.Split('\n');
		var cities = new List<string>();

		foreach (var range in distancesRanges)
			AddDistance(span[range], cities);

		_cities = cities.ToArray();

		CreatePosibilities(_cities.Length);

		var minDistance = GetMinimumDistance();

		return minDistance.ToString();
	}

	private int GetMinimumDistance()
	{
		var min = int.MaxValue;

		foreach (var item in _posibilities)
		{
			var distance = GetFullDistance(item);
			if (distance < min)
				min = distance;
		}

		return min;
	}

	private void AddDistance(ReadOnlySpan<char> line, List<string> cities)
	{
		var toIndex = line.IndexOf(" to ");
		var equalIndex = line.IndexOf('=');

		var firstCity = line[..toIndex].ToString();
		var secondCity = line[(toIndex + 4)..(equalIndex - 1)].ToString();

		if (!cities.Contains(secondCity))
			cities.Add(secondCity);
		if (!cities.Contains(firstCity))
			cities.Add(firstCity);

		var citiesPair = new CitiesPair(firstCity, secondCity);
		var distance = int.Parse(line[(equalIndex + 2)..].ToString());

		_distancesDictionary[citiesPair] = distance;
	}

	private void CreatePosibilities(int length)
	{
		var referenceList = new List<int>();
		for (int i = 0; i < length; i++)
			referenceList.Add(i);

		var tempList = new List<int>();

		Recurency(referenceList, tempList, _posibilities);
	}

	private void Recurency(List<int> referenceList, List<int> tempList, List<int[]> posibilities)
	{
		if (referenceList.Count == 0)
		{
			posibilities.Add(tempList.ToArray());
			return;
		}

		foreach (int i in referenceList)
		{
			var newReference = referenceList.Where(x => x != i).ToList();
			var newTemp = tempList.ToList();
			newTemp.Add(i);
			Recurency(newReference, newTemp, posibilities);
		}
	}

	private int GetFullDistance(int[] item)
	{
		var span = _cities.AsSpan();
		var distance = 0;
		for (int i = 0; i < item.Length - 1; i++)
		{
			var pair = new CitiesPair(span[item[i]], span[item[i+1]]);
			distance += _distancesDictionary[pair];
		}
		return distance;
	}
}
