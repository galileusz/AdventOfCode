using AdventOfCode.Commons;
using AdventOfCode.Year2015.Day21.Commons;
using AdventOfCodeGate.Interfaces;

namespace AdventOfCode.Year2015.Day21.Part2;

internal class Solver : BaseResolver, IAdventOfCode
{
	public override string Solve(string input)
	{
		var span = input.AsSpan().Trim();
		var lines = span.Split('\n');

		lines.MoveNext();
		var bossVital = int.Parse(span[lines.Current][12..]);
		lines.MoveNext();
		var bossDamage = int.Parse(span[lines.Current][8..]);
		lines.MoveNext();
		var bossArmor = int.Parse(span[lines.Current][7..]);

		var playerVital = 100;

		var max = 0;

		for (int wi = 0; wi < Inventory.Weapons.Length; wi++)
		{
			for (int ai = -1; ai < Inventory.Armors.Length; ai++)
			{
				for (int r1i = -1; r1i < Inventory.Rings.Length; r1i++)
				{
					for (int r2i = -1; r2i < Inventory.Rings.Length; r2i++)
					{
						if (r1i == r2i && r1i != -1)
							continue;

						(var playerDamage, var playerArmor, var playerCost) = GetPlayerStats(wi, ai, r1i, r2i);
						if (playerCost > max)
						{
							if (!DoesPlayerWin(playerVital, playerDamage, playerArmor, bossVital, bossDamage, bossArmor))
								max = playerCost;
						}
					}
				}
			}
		}

		return max.ToString();
	}

	private bool DoesPlayerWin(int playerVital, int playerDamage, int playerArmor, int bossVital, int bossDamage, int bossArmor)
	{
		return playerVital / Math.Max(1, bossDamage - playerArmor) >= bossVital / Math.Max(1, playerDamage - bossArmor);
	}

	private (int playerDamage, int playerArmor, int playerCost) GetPlayerStats(int wi, int ai, int r1i, int r2i)
	{
		var cost = 0;
		var armor = 0;
		var damage = 0;
		cost += Inventory.Weapons[wi].Cost;
		damage += Inventory.Weapons[wi].Damage;
		armor += Inventory.Weapons[wi].Armor;

		if (ai != -1)
		{
			cost += Inventory.Armors[ai].Cost;
			damage += Inventory.Armors[ai].Damage;
			armor += Inventory.Armors[ai].Armor;
		}

		if (r1i != -1)
		{
			cost += Inventory.Rings[r1i].Cost;
			damage += Inventory.Rings[r1i].Damage;
			armor += Inventory.Rings[r1i].Armor;
		}

		if (r2i != -1)
		{
			cost += Inventory.Rings[r2i].Cost;
			damage += Inventory.Rings[r2i].Damage;
			armor += Inventory.Rings[r2i].Armor;
		}

		return (damage, armor, cost);
	}
}
