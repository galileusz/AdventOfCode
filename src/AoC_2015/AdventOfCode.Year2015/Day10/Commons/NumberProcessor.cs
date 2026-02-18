using System.Text;

namespace AdventOfCode.Year2015.Day10.Commons;

internal static class NumberProcessor
{
	public static ReadOnlySpan<char> Process(ReadOnlySpan<char> span)
	{
		var sb = new StringBuilder();

		var counter = 0;
		var previousChar = '\0';
		foreach (var c in span)
		{
			if (previousChar == c)
			{
				counter++;
			}
			else if (previousChar == '\0')
			{
				// Do Nothing
			}
			else
			{
				sb.Append($"{++counter}{previousChar}");
				counter = 0;
			}
			previousChar = c;
		}
		sb.Append($"{++counter}{previousChar}");

		return sb.ToString();
	}
}
