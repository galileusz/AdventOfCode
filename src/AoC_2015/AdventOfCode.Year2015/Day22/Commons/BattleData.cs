namespace AdventOfCode.Year2015.Day22.Commons;

internal class BattleData
{
	public bool PlayerWins { get; set; }
	public bool IsCompleted { get; set; }
	public bool IsPlayerTurn { get; set; } = true;
	public int TotalManaSpent { get; private set; } = 0;

	public int PlayerHealth { get; set; }
	public int PlayerMana { get; set; }
	public int BossHealth { get; set; }
	public int BossDamage { get; set; }
	public int ShieldTimer { get; set; }
	public int PoisonTimer { get; set; }
	public int RechargeTimer { get; set; }

	public bool CanCastMissile() => PlayerMana >= SpellInfo.MissileMana;
	public bool CanCastDrain() => PlayerMana >= SpellInfo.DrainMana;
	public bool CanCastShield() => PlayerMana >= SpellInfo.ShieldMana;
	public bool CanCastPoison() => PlayerMana >= SpellInfo.PoisonMana;
	public bool CanCastRecharge() => PlayerMana >= SpellInfo.RechargeMana;
	public bool CanCastAny() => PlayerMana >= SpellInfo.MissileMana;

	public void CastMissile()
	{
		PlayerMana -= SpellInfo.MissileMana;
		BossHealth -= 4;
		TotalManaSpent += SpellInfo.MissileMana;
	}

	public void CastDrain()
	{
		PlayerMana -= SpellInfo.DrainMana;
		PlayerHealth += 2;
		BossHealth -= 2;
		TotalManaSpent += SpellInfo.DrainMana;
	}

	public void CastShield()
	{
		PlayerMana -= SpellInfo.ShieldMana;
		ShieldTimer = 6;
		TotalManaSpent += SpellInfo.ShieldMana;
	}

	public void CastPoison()
	{
		PlayerMana -= SpellInfo.PoisonMana;
		PoisonTimer = 6;
		TotalManaSpent += SpellInfo.PoisonMana;
	}

	public void CastRecharge()
	{
		PlayerMana -= SpellInfo.RechargeMana;
		RechargeTimer = 5;
		TotalManaSpent += SpellInfo.RechargeMana;
	}

	private void BeforeRound()
	{
		if (RechargeTimer > 0)
		{
			PlayerMana += 101;
			RechargeTimer--;
		}
		if (PoisonTimer > 0)
		{
			BossHealth -= 3;
			PoisonTimer--;
		}
	}

	private void PostRound()
	{
		if (ShieldTimer > 0)
			ShieldTimer--;
		CheckIsEnd();
	}

	private void CheckIsEnd()
	{
		if (BossHealth <= 0)
		{
			PlayerWins = true;
			IsCompleted = true;
		}
		else if (PlayerHealth <= 0)
		{
			PlayerWins = false;
			IsCompleted = true;
		}
		else if (IsPlayerTurn == false && RechargeTimer == 0 && false == CanCastAny())
		{
			PlayerWins = false;
			IsCompleted = true;
		}
	}

	public void BossAttack()
	{
		BeforeRound();
		var damage = BossDamage - (ShieldTimer > 0 ? 7 : 0);
		PlayerHealth -= damage > 0 ? damage : 1;
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

	public BattleData Clone()
	{
		return new BattleData
		{
			PlayerWins = PlayerWins,
			IsCompleted = IsCompleted,
			IsPlayerTurn = IsPlayerTurn,
			TotalManaSpent = TotalManaSpent,
			PlayerHealth = PlayerHealth,
			PlayerMana = PlayerMana,
			BossHealth = BossHealth,
			BossDamage = BossDamage,
			ShieldTimer = ShieldTimer,
			PoisonTimer = PoisonTimer,
			RechargeTimer = RechargeTimer
		};
	}
}
