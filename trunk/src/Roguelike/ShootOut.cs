using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class ShootOut : ICreatureInteraction
	{
		IRandomNumberGenerator randomNumberGenerator;

		public ShootOut(IRandomNumberGenerator randomNumberGenerator)
		{
			this.randomNumberGenerator = randomNumberGenerator;
		}

		public void commenceInteraction(Creature attacker, Creature deffender)
		{
			if(randomNumberGenerator.generateNumber() <= attacker.RangedWeapon.Chance)
			{
				deffender.Health -= attacker.RangedWeapon.Damage;
				if (deffender.isDead)
				{
					LootGenerator lootGen = new LootGenerator();
					lootGen.generateLoot(deffender);
					deffender.Field.removeCreature();
				}
			}
		}
	}
}
