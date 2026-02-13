using AdventOfCode.Commons;
using AdventOfCode.Year2025.Day09.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day09.Part2;

public class Solver : BaseResolver, IAdventOfCode
{
	private long _result = 0;
	private Dictionary<long, long> _xMinDictionary = new Dictionary<long, long>();
	private Dictionary<long, long> _xMaxDictionary = new Dictionary<long, long>();
	private Dictionary<long, long> _yMinDictionary = new Dictionary<long, long>();
	private Dictionary<long, long> _yMaxDictionary = new Dictionary<long, long>();

	public override string Solve(string input)
	{
		Dictionary<long, long> _xMinDictionary = new Dictionary<long, long>();
		Dictionary<long, long> _xMaxDictionary = new Dictionary<long, long>();
		Dictionary<long, long> _yMinDictionary = new Dictionary<long, long>();
		Dictionary<long, long> _yMaxDictionary = new Dictionary<long, long>();

		var lines = input.Split("\n");

		Process(lines);

		return _result.ToString();
	}

	private void Process(string[] lines)
	{
		var redTiles = lines.Select(line =>
		{
			var coordinates = line.Split(',').Select(long.Parse).ToArray();
			return new Tile(coordinates[0], coordinates[1]);
		}).ToList();

		var greenTiles = new List<Tile>();
		for (int i = 0; i < redTiles.Count - 1; i++)
		{
			CreateGreenTiles(greenTiles, redTiles[i], redTiles[i + 1]);
		}
		CreateGreenTiles(greenTiles, redTiles.Last(), redTiles.First());

		var rectangles = new List<Rectangle>();
		for (int i = 0; i < redTiles.Count - 1; i++)
		{
			for (int j = i + 1; j < redTiles.Count; j++)
			{
				rectangles.Add(new Rectangle(redTiles[i], redTiles[j]));
			}
		}

		rectangles = rectangles.OrderByDescending(r => r.Area).ToList();

		FillDictionaries(redTiles, greenTiles);

		foreach (var rec in rectangles)
		{
			if (TryCreateRectangle(rec.First, rec.Second))
			{
				_result = rec.Area;
				return;
			}
		}
	}

	private void FillDictionaries(List<Tile> redTiles, List<Tile> greenTiles)
	{
		var allTiles = redTiles.Concat(greenTiles);

		foreach (var tile in allTiles)
		{
			if (false == _xMinDictionary.TryGetValue(tile.X, out long yMin) || yMin > tile.Y)
				_xMinDictionary[tile.X] = tile.Y;

			if (false == _xMaxDictionary.TryGetValue(tile.X, out long yMax) || yMax < tile.Y)
				_xMaxDictionary[tile.X] = tile.Y;

			if (false == _yMinDictionary.TryGetValue(tile.Y, out long xMin) || xMin > tile.X)
				_yMinDictionary[tile.Y] = tile.X;

			if (false == _yMaxDictionary.TryGetValue(tile.Y, out long xMax) || xMax < tile.X)
				_yMaxDictionary[tile.Y] = tile.X;
		}
	}

	private void CreateGreenTiles(List<Tile> greenTiles, Tile tile1, Tile tile2)
	{
		if (tile1.X == tile2.X)
		{
			CreateYGreenTiles(greenTiles, tile1, tile2);
		}

		if (tile1.Y == tile2.Y)
		{
			CreateXGreenTiles(greenTiles, tile1, tile2);
		}
	}

	private void CreateXGreenTiles(List<Tile> greenTiles, Tile tile1, Tile tile2)
	{
		var startX = Math.Min(tile1.X, tile2.X) + 1;
		var endX = Math.Max(tile1.X, tile2.X) - 1;
		for (long i = startX; i <= endX; i++)
			greenTiles.Add(new Tile(i, tile1.Y));
	}

	private void CreateYGreenTiles(List<Tile> greenTiles, Tile tile1, Tile tile2)
	{
		var startY = Math.Min(tile1.Y, tile2.Y) + 1;
		var endY = Math.Max(tile1.Y, tile2.Y) - 1;
		for (long i = startY; i <= endY; i++)
			greenTiles.Add(new Tile(tile1.X, i));
	}

	private bool TryCreateRectangle(Tile tile1, Tile tile2)
	{
		var minX = Math.Min(tile1.X, tile2.X);
		var maxX = Math.Max(tile1.X, tile2.X);
		var minY = Math.Min(tile1.Y, tile2.Y);
		var maxY = Math.Max(tile1.Y, tile2.Y);

		var areaMax_xMin = _yMinDictionary.Where(item => item.Key > minY && item.Key < maxY).Select(item => item.Value).Max();
		if (areaMax_xMin > minX)
			return false;

		var areaMin_xMax = _yMaxDictionary.Where(item => item.Key > minY && item.Key < maxY).Select(item => item.Value).Min();
		if (areaMin_xMax < maxX)
			return false;

		var areaMax_yMin = _xMinDictionary.Where(item => item.Key > minX && item.Key < maxX).Select(item => item.Value).Max();
		if (areaMax_yMin > minY)
			return false;

		var areaMin_yMax = _xMaxDictionary.Where(item => item.Key > minX && item.Key < maxX).Select(item => item.Value).Min();
		if (areaMin_yMax < maxY)
			return false;

		return true;
	}

}