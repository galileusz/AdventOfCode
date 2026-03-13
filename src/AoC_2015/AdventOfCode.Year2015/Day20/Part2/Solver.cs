using AdventOfCode.Commons;
using AdventOfCodeGate.Interfaces;
using System.Globalization;

namespace AdventOfCode.Year2015.Day20.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	private int _min;
	public override string Solve(string input)
	{
		_min = int.MaxValue;
		int[] primeNumbers = { 2, 3, 5, 7, 11, 13 };
		var checkNumber = int.Parse(input);

		for (int i = 0; i <= 10; i++)
		{
			var number1 = (int)Math.Pow(primeNumbers[5], i);
			if (CheckIsAnswer(number1, checkNumber))
			{
				// Console.WriteLine($"{13}^{i}");
				break;
			}
			for (int j = 0; j <= 10; j++)
			{
				var number2 = number1 * (int)Math.Pow(primeNumbers[4], j);
				if (CheckIsAnswer(number2, checkNumber))
				{
					// Console.WriteLine($"{13}^{i} * {11}^{j}");
					break;
				}
				for (int k = 0; k <= 10; k++)
				{
					var number3 = number2 * (int)Math.Pow(primeNumbers[3], k);
					if (CheckIsAnswer(number3, checkNumber))
					{
						// Console.WriteLine($"{13}^{i} * {11}^{j} * {7}^{k}");
						break;
					}
					for (int l = 0; l <= 10; l++)
					{
						var number4 = number3 * (int)Math.Pow(primeNumbers[2], l);
						if (CheckIsAnswer(number4, checkNumber))
						{
							// Console.WriteLine($"{13}^{i} * {11}^{j} * {7}^{k} * {5}^{l}");
							break;
						}
						for (int m = 0; m <= 10; m++)
						{
							var number5 = number4 * (int)Math.Pow(primeNumbers[1], m);
							if (CheckIsAnswer(number5, checkNumber))
							{
								// Console.WriteLine($"{13}^{i} * {11}^{j} * {7}^{k} * {5}^{l} * {3}^{m}");
								break;
							}
							for (int n = 0; n <= 10; n++)
							{
								var number6 = number5 * (int)Math.Pow(primeNumbers[0], n);
								if (CheckIsAnswer(number6, checkNumber))
								{
									// Console.WriteLine($"{13}^{i} * {11}^{j} * {7}^{k} * {5}^{l} * {3}^{m} * {2}^{n}");
									break;
								}
							}

						}
					}
				}
			}
		}


		return _min.ToString();
	}

	public bool CheckIsAnswer(int number, int checkNumber)
	{
		if (number > checkNumber / 20 || number < 0)
			return false;

		var valueX = (1 + number) * 11;

		for (var j = 2; j < number; j++)
		{
			if (number % j == 0 && number / j <= 50)
				valueX += j * 11;
		}

		var result = valueX >= checkNumber;
		if (result)
		{
			if (number < _min)
				_min = number;
			// Console.WriteLine($"{number} : {valueX}");
		}

		return result;
	}
}
