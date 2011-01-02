using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class AttackCommand : ICreatureCommand
	{
		Creature deffender;
		Creature attacker;
		public AttackCommand(Creature attacker, Creature target)
		{
			this.deffender = target;
			this.attacker = attacker;
		}

		public void execute()
		{
			if(attacker.MeleeWeapon != null)
			{
				deffender.Health -= attacker.MeleeWeapon.Damage;
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
