namespace AdventOfCodeManager.ConsoleCommons;

public sealed class NavigationStack
{
    private readonly Stack<IMenuState> _stack = new();

    public bool HasState => _stack.Count > 0;

    public void Push(IMenuState state) => _stack.Push(state);

    public void Pop()
    {
        if (_stack.Count > 0) _stack.Pop();
    }

    public IMenuState Peek() => _stack.Peek();
}