using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day22.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day22.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	private int _minManaSpent;
	public override string Solve(string input)
	{
		_minManaSpent = int.MaxValue;
		var battleData = new BattleData
		{
			PlayerHealth = 50,
			PlayerMana = 500,
		};

		var span = input.AsSpan().Trim();
		var enterIndex = span.IndexOf('\n');
		battleData.BossHealth = int.Parse(span[12..enterIndex]);
		battleData.BossDamage = int.Parse(span[(enterIndex + 9)..]);

		SimulateBattle(battleData);

		return _minManaSpent.ToString();
	}

	public void SimulateBattle(BattleData battleData)
	{
		if (battleData.TotalManaSpent >= _minManaSpent)
			return;
		if (battleData.PlayerWins && battleData.TotalManaSpent < _minManaSpent)
		{
			_minManaSpent = battleData.TotalManaSpent;
		}
		if (battleData.IsCompleted)	
			return;

		if (battleData.IsPlayerTurn)
		{
			SimulatePlayerTurn(battleData);
		}
		else
			SimulateBossTurn(battleData);
	}

	private void SimulateBossTurn(BattleData battleData)
	{
		battleData.BossAttack();
		SimulateBattle(battleData);
	}

	private void SimulatePlayerTurn(BattleData battleData)
	{
		if (battleData.CanCastDrain())
		{
			var clone = battleData.Clone();
			clone.PlayerAttack(ESpell.Drain);
			SimulateBattle(clone);
		}
		if (battleData.CanCastMissile())
		{
			var clone = battleData.Clone();
			clone.PlayerAttack(ESpell.Missile);
			SimulateBattle(clone);
		}
		if (battleData.CanCastShield())
		{
			var clone = battleData.Clone();
			clone.PlayerAttack(ESpell.Shield);
			SimulateBattle(clone);
		}
		if (battleData.CanCastPoison())
		{
			var clone = battleData.Clone();
			clone.PlayerAttack(ESpell.Poison);
			SimulateBattle(clone);
		}
		if (battleData.CanCastRecharge())
		{
			var clone = battleData.Clone();
			clone.PlayerAttack(ESpell.Recharge);
			SimulateBattle(clone);
		}
	}
}