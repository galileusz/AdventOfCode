namespace AdventOfCodeManager.ConsoleCommons;

public sealed class NavAction
{
	public NavActionKind Kind { get; }
	public IMenuState? NextState { get; }

	private NavAction(NavActionKind kind, IMenuState? nextState = null)
	{
		Kind = kind;
		NextState = nextState;
	}

	public static NavAction Stay() => new(NavActionKind.Stay);
	public static NavAction Push(IMenuState next) => new(NavActionKind.Push, next);
	public static NavAction Pop() => new(NavActionKind.Pop);
	public static NavAction Quit() => new(NavActionKind.Quit);
}