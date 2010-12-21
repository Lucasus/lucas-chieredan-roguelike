using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class Fight : ICreatureInteraction
	{
		public void commenceInteraction(Creature attacker, Creature deffender)
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
