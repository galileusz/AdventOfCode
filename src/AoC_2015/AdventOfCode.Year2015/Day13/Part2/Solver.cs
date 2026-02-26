using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day13.Commons;
using AdventOfCodeGate.Interfaces;
using System.Collections.Immutable;

namespace AdventOfCode.Year2015.Day13.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	private List<PeoplePair> _happinessList;
	private int[] _people;
	private List<int[]> _posibilities;

	public override string Solve(string input)
	{
		_happinessList = new List<PeoplePair>();
		_posibilities = new List<int[]>();

		var span = input.AsSpan().Trim();
		var lines = span.Split('\n');
		var people = new List<int>() { 0 };

		foreach (var line in lines)
			AddPairs(span[line], _happinessList, people);

		_people = people.ToArray();

		CreatePosibilities(_people.Length);

		return GetCombinedHappiness();
	}

	private void AddPairs(ReadOnlySpan<char> span, List<PeoplePair> happinessList, List<int> people)
	{
		var indexWould = span.IndexOf(" would ");
		var indexGain = span.IndexOf(" gain ");
		var indexLose = span.IndexOf(" lose ");
		var indexHappiness = span.IndexOf(" happiness ");

		var firstPerson = GetHash(span[..indexWould]);
		var secondPerson = GetHash(span[(indexHappiness + 36)..^1]);

		if (!people.Contains(firstPerson))
			people.Add(firstPerson);

		var sign = indexGain != -1 ? 1 : -1;

		var value = sign == 1 ? GetHappinessValue(indexGain, indexHappiness, span) : GetHappinessValue(indexLose, indexHappiness, span);

		var pair = happinessList.FirstOrDefault(x => (x.FirstPerson == firstPerson && x.SecondPerson == secondPerson) || 
																								 (x.FirstPerson == secondPerson && x.SecondPerson == firstPerson));
		if (pair == null)
			happinessList.Add(new PeoplePair(firstPerson, secondPerson, sign * value));
		else
			pair.Happiness += sign * value;
	}

	private int GetHappinessValue(int indexStart, int indexEnd, ReadOnlySpan<char> span)
	{
		return int.Parse(span[(indexStart + 6)..indexEnd]);
	}

	private int GetHash(ReadOnlySpan<char> span)
	{
		var hash = 0;
		foreach (var c in span)
			hash += 17 + c;
		return hash;
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

	private string GetCombinedHappiness()
	{
		var max = 0;
		foreach (var item in _posibilities)
		{
			var happiness = GetTableHappiness(item);
			if (happiness > max)
				max = happiness;
		}

		return max.ToString();
	}

	private int GetTableHappiness(int[] item)
	{
		var happiness = 0;
		for (int i = 0; i < item.Length; i++)
		{
			var firstPerson = _people[item[i]];
			var secondPerson = _people[item[(i + 1) % item.Length]];

			if (firstPerson == 0 || secondPerson == 0)
				continue;

			var pair = _happinessList.FirstOrDefault(x => (x.FirstPerson == firstPerson && x.SecondPerson == secondPerson) || 
																										(x.FirstPerson == secondPerson && x.SecondPerson == firstPerson));

			happiness += pair.Happiness;
		}

		return happiness;
	}
}
