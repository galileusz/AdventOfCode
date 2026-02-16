using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day04.Commons;
using AdventOfCodeGate.Interfaces;
using System.Text;
namespace AdventOfCode.Year2015.Day04.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		ReadOnlySpan<char> hash;
		byte[] prefixBytes = Encoding.UTF8.GetBytes(input);

		var i = 0;
		string number;
		do
		{
			number = i.ToString();
			//Console.Write(number);
			int totalLength = prefixBytes.Length + number.Length;

			byte[] buffer = new byte[totalLength];

			Buffer.BlockCopy(prefixBytes, 0, buffer, 0, prefixBytes.Length);

			for (int j = 0; j < number.Length; j++)
				buffer[prefixBytes.Length + j] = (byte)number[j];

			i++;
			hash = MD5Coder.Code(buffer);
			//Console.Write($"\t->{hash}\n");
		
		} while (!hash.StartsWith("00000"));

		return number; 
	}
}
