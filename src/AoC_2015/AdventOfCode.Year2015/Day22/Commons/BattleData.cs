using System.Buffers;

namespace AdventOfCode.Year2015.Day22.Commons;

internal class BattleData
{
	public BattleData(ArrayPool<int> pool, int playerHealth, int playerMana, int bossHealth)
	{
		IsPlayerTurn = true;
		Data = pool.Rent(7);
		Data[0] = playerHealth;
		Data[1] = playerMana;
		Data[2] = bossHealth;
	}

	public bool IsPlayerTurn { get; set; }
	public int[] Data { get; }

	public bool CanCastMissile() => Data[1] >= SpellInfo.MissileMana;
	public bool CanCastDrain() => Data[1] >= SpellInfo.DrainMana;
	public bool CanCastShield() => Data[1] >= SpellInfo.ShieldMana;
	public bool CanCastPoison() => Data[1] >= SpellInfo.PoisonMana;
	public bool CanCastRecharge() => Data[1] >= SpellInfo.RechargeMana;
	public bool CanCastAny() => Data[1] >= SpellInfo.MissileMana;

	public bool IsPlayerWin() => Data[2] <= 0;
	public bool IsComplete() => Data[0] <= 0 || IsPlayerWin();
	public int GetTotalManaSpent() => Data[6];

	public void CastMissile()
	{
		Data[1] -= SpellInfo.MissileMana;
		Data[2] -= 4;
		Data[6] += SpellInfo.MissileMana;
	}

	public void CastDrain()
	{
		Data[1] -= SpellInfo.DrainMana;
		Data[0] += 2;
		Data[2] -= 2;
		Data[6] += SpellInfo.DrainMana;
	}

	public void CastShield()
	{
		Data[1] -= SpellInfo.ShieldMana;
		Data[3] = 6;
		Data[6] += SpellInfo.ShieldMana;
	}

	public void CastPoison()
	{
		Data[1] -= SpellInfo.PoisonMana;
		Data[4] = 6;
		Data[6] += SpellInfo.PoisonMana;
	}

	public void CastRecharge()
	{
		Data[1] -= SpellInfo.RechargeMana;
		Data[5] = 5;
		Data[6] += SpellInfo.RechargeMana;
	}

	private void BeforeRound()
	{
		if (Data[5] > 0)
		{
			Data[1] += 101;
			Data[5]--;
		}
		if (Data[4] > 0)
		{
			Data[2] -= 3;
			Data[4]--;
		}
	}

	private void PostRound()
	{
		if (Data[3] > 0)
			Data[3]--;
	}

	public void BossAttack(int bossDamage)
	{
		BeforeRound();
		var damage = bossDamage - (Data[3] > 0 ? 7 : 0);
		Data[0] -= damage > 0 ? damage : 1;
		PostRound();
		IsPlayerTurn = true;
	}

	public void PlayerAttack(ESpell spell)
	{
		BeforeRound();
		switch (spell)
		{
			case ESpell.Missile:
				CastMissile();
				break;
			case ESpell.Drain:
				CastDrain();
				break;
			case ESpell.Shield:
				CastShield();
				break;
			case ESpell.Poison:
				CastPoison();
				break;
			case ESpell.Recharge:
				CastRecharge();
				break;
		}
		PostRound();
		IsPlayerTurn = false;
	}

	public BattleData Clone(ArrayPool<int> pool)
	{
		var clone = new BattleData(pool, Data[0], Data[1], Data[2]);
		clone.Data[3] = Data[3];
		clone.Data[4] = Data[4];
		clone.Data[5] = Data[5];
		clone.Data[6] = Data[6];
		clone.IsPlayerTurn = IsPlayerTurn;

		return clone;
	}
}
