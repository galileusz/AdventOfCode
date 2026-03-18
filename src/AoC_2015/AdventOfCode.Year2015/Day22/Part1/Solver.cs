using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day22.Commons;
using AdventOfCodeGate.Interfaces;
using System.Buffers;

namespace AdventOfCode.Year2015.Day22.Part1;

internal class Solver : BaseResolver, IAdventOfCode
{
	private int _minManaSpent;
	private ArrayPool<int> _pool;
	public override string Solve(string input)
	{
		_pool = ArrayPool<int>.Shared;
		_minManaSpent = int.MaxValue;

		var span = input.AsSpan().Trim();
		var enterIndex = span.IndexOf('\n');
		var bossHealth = int.Parse(span[12..enterIndex]);
		var bossDamage = int.Parse(span[(enterIndex + 9)..]);

		var battleData = new BattleData(_pool, 50, 500, bossHealth);

		SimulateBattle(battleData, bossDamage);

		return _minManaSpent.ToString();
	}

	public void SimulateBattle(BattleData battleData, int bossDamage)
	{
		if (battleData.GetTotalManaSpent() >= _minManaSpent)
		{
			_pool.Return(battleData.Data);
			return;
		}
		if (battleData.IsPlayerWin() && battleData.GetTotalManaSpent() < _minManaSpent)
		{
			_minManaSpent = battleData.GetTotalManaSpent();
		}
		if (battleData.IsComplete())
		{
			_pool.Return(battleData.Data);
			return;
		}

		if (battleData.IsPlayerTurn)
		{
			SimulatePlayerTurn(battleData, bossDamage);
		}
		else
			SimulateBossTurn(battleData, bossDamage);
	}

	private void SimulateBossTurn(BattleData battleData, int bossDamage)
	{
		battleData.BossAttack(bossDamage);
		SimulateBattle(battleData, bossDamage);
	}

	private void SimulatePlayerTurn(BattleData battleData, int bossDamage)
	{
		if (battleData.CanCastDrain())
		{
			var clone = battleData.Clone(_pool);
			clone.PlayerAttack(ESpell.Drain);
			SimulateBattle(clone, bossDamage);
		}
		if (battleData.CanCastMissile())
		{
			var clone = battleData.Clone(_pool);
			clone.PlayerAttack(ESpell.Missile);
			SimulateBattle(clone, bossDamage);
		}
		if (battleData.CanCastShield())
		{
			var clone = battleData.Clone(_pool);
			clone.PlayerAttack(ESpell.Shield);
			SimulateBattle(clone, bossDamage);
		}
		if (battleData.CanCastPoison())
		{
			var clone = battleData.Clone(_pool);
			clone.PlayerAttack(ESpell.Poison);
			SimulateBattle(clone, bossDamage);
		}
		if (battleData.CanCastRecharge())
		{
			var clone = battleData.Clone(_pool);
			clone.PlayerAttack(ESpell.Recharge);
			SimulateBattle(clone, bossDamage);
		}
		_pool.Return(battleData.Data);
	}
}