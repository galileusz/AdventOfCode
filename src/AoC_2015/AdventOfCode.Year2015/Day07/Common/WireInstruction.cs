namespace AdventOfCode.Year2015.Day07.Common;

internal class WireInstruction(Func<uint> wireFunction)
{
	public Func<uint> WireFunction { get; set; } = wireFunction;
	public uint? Result { get; set; } = null;
	public uint GetValue()
	{
		if (Result != null)
			return Result.Value;

		var result = WireFunction.Invoke();
		Result = result;
		return result;
	}
}
