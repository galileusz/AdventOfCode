using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2025.Day11.Part2;

public class Solver : BaseResolver, IAdventOfCode
{
	private long _result = 0;
	private Dictionary<string, List<string>> _gatewaysDictionary = new Dictionary<string, List<string>>();

	public override string Solve(string input)
	{
		var lines = input.Split("\n");

		Task.Run(async () => await Process(lines)).Wait();

		return _result.ToString();
	}

	private async Task Process(string[] lines)
	{
		_gatewaysDictionary = new Dictionary<string, List<string>>();

		foreach (var line in lines)
			AddDictionaryItem(line);

		var keysFFT_Forward = new List<string>();

		FindForward(keysFFT_Forward, ["fft"]);

		var keysFFT = new List<string>();

		FindReverse(keysFFT, ["fft"]);

		var keysDac = new List<string>();

		FindReverse(keysDac, ["dac"]);


		keysFFT_Forward = keysFFT_Forward.Order().ToList();
		keysDac = keysDac.Order().ToList();
		var intersect_FFT_DAC = keysFFT_Forward.Intersect(keysDac).ToList();
		intersect_FFT_DAC.AddRange(["fft", "dac"]);

		var gatewaysFFT = _gatewaysDictionary.Where(x => keysFFT.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
		var gatewaysDac = _gatewaysDictionary.Where(x => intersect_FFT_DAC.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);

		_result = 0;
		await FindPaths("svr", "fft", gatewaysFFT);
		var result1 = _result;
		_result = 0;
		await FindPaths("fft", "dac", gatewaysDac);
		var result2 = _result;
		_result = 0;
		await FindPaths("dac", "out", _gatewaysDictionary);
		var result3 = _result;

		_result = result1 * result2 * result3;
	}

	private void FindForward(List<string> keysFFT_Forward, List<string> actualFinding)
	{
		if (actualFinding.Count == 0)
			return;

		var newFinding = new List<string>();
		foreach (var key in actualFinding)
		{
			if (_gatewaysDictionary.TryGetValue(key, out List<string> paths))
			{
				foreach (var path in paths)
				{
					if (!keysFFT_Forward.Contains(path))
					{
						keysFFT_Forward.Add(path);
						newFinding.Add(path);
					}
				}
			}
		}
		FindForward(keysFFT_Forward, newFinding);
	}

	private async Task FindPaths(string gateway, string end, Dictionary<string, List<string>> gateways)
	{
		if (gateway == end)
		{
			_result++;
			return;
		}

		if (gateways.TryGetValue(gateway, out List<string> paths))
		{
			var tasks = paths.Select(p => FindPaths(p, end, gateways));
			await Task.WhenAll(tasks);
		}
	}

	private void FindReverse(List<string> keys, List<string> actualFinding)
	{
		if (actualFinding.Count == 0)
			return;

		var newFinding = new List<string>();
		foreach (var path in actualFinding)
		{
			foreach (var item in _gatewaysDictionary)
			{
				if (item.Value.Contains(path) && !keys.Contains(item.Key))
				{
					keys.Add(item.Key);
					newFinding.Add(item.Key);
				}
			}
		}
		FindReverse(keys, newFinding);
	}

	private void AddDictionaryItem(string line)
	{
		var parts = line.Split(' ');
		_gatewaysDictionary.Add(parts[0][..^1], [.. parts.Skip(1)]);
	}
}
