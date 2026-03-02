using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day13.Commons;
using AdventOfCodeGate.Interfaces;
using System.Collections.Immutable;

namespace AdventOfCode.Year2015.Day13.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	private List<PeoplePair> _happinessList;
	private int[] _people;

	public override string Solve(string input)
	{
		_happinessList = new List<PeoplePair>();

		var span = input.AsSpan().Trim();
		var lines = span.Split('\n');
		var people = new List<int>() { 0 };

		foreach (var line in lines)
			AddPairs(span[line], _happinessList, people);

		_people = people.ToArray();

		var max = int.MinValue;
		CheckPosibilities(_people.Length, ref max);

		return max.ToString();
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
	private void CheckPosibilities(int length, ref int max)
	{
		var referenceList = new List<int>();
		for (int i = 0; i < length; i++)
			referenceList.Add(i + 1);

		var tempPossibility = 0;
		Recurency(referenceList, ref tempPossibility, ref max);
	}

	private void Recurency(List<int> referenceList, ref int tempPossibility, ref int max)
	{
		if (referenceList.Count == 0)
		{
			var item = tempPossibility;
			var happiness = GetTableHappiness(item);
			if (happiness > max)
				max = happiness;
			return;
		}

		foreach (int i in referenceList)
		{
			var newReference = referenceList.Where(x => x != i).ToList();
			var newTemp = tempPossibility * 10;
			newTemp += i;
			Recurency(newReference, ref newTemp, ref max);
		}
	}

	private int GetTableHappiness(int item)
	{
		if (item == 123746589)
		{
			var test0 = 0;
		}
		var happiness = 0;
		var firstElement = -1;
		var previousElement = -1;
		for (int i = 0; i <= _people.Length; i++)
		{
			var element = item % 10 - 1;
			item /= 10;
			if (previousElement != -1)
			{
				if (element == -1)
					element = firstElement;

				var firstPerson = _people[previousElement];
				var secondPerson = _people[element];

				if (firstPerson == 0 || secondPerson == 0)
				{
					previousElement = element;
					continue;
				}

				var pair = _happinessList.First(x => (x.FirstPerson == firstPerson && x.SecondPerson == secondPerson) ||
																						 (x.FirstPerson == secondPerson && x.SecondPerson == firstPerson));
				happiness += pair.Happiness;
			}

			if (i == 0)
				firstElement = element;

			previousElement = element;
		}

		return happiness;
	}

}
