using AdventOfCode.Commons;

namespace AdventOfCodeManager.ConsoleCommons;

public sealed class ResultState : IMenuState
{
	private BaseResolver _baseResolver;

	public ResultState(Type solverType)
	{
		_baseResolver = solverType.GetConstructor(Type.EmptyTypes)?.Invoke(null) as BaseResolver
			?? throw new InvalidOperationException($"Cannot create instance of {solverType.FullName}");
	}

	public void Render()
	{
		Ui.Header("✅ Wybrano");

		_baseResolver.Resolve();

		Ui.Footer("B wstecz   Esc wyjście");
	}

	public NavAction Handle(ConsoleKeyInfo key)
	{
		if (key.Key == ConsoleKey.Escape) return NavAction.Quit();
		if (key.Key == ConsoleKey.B) return NavAction.Pop();
		return NavAction.Stay();
	}
}