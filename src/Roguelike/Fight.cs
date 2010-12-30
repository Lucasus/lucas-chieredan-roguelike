using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class Fight : ICreatureInteraction
	{
		IRandomNumberGenerator randomNumberGenerator;

		public Fight(IRandomNumberGenerator randomNumberGen)
		{
			this.randomNumberGenerator = randomNumberGen;
		}

		public void commenceInteraction(Creature attacker, Creature deffender)
		{
			if(randomNumberGenerator.generateNumber() <= attacker.Weapon.Chance)
			{
				deffender.Health -= attacker.Weapon.Damage;
				if(deffender.isDead)
				{
					LootGenerator lootGen = new LootGenerator();
					lootGen.generateLoot(deffender);
					deffender.Field.removeCreature();
				}
			}
		}
	}
}
