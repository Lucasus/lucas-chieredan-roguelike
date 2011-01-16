using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class AttackCommand : ICreatureCommand
	{
		Map map;
		Creature deffender;
		Creature attacker;
		public AttackCommand(Creature attacker, Creature target, Map map)
		{
			this.deffender = target;
			this.attacker = attacker;
			this.map = map;
		}

		public bool isExecutable()
		{
			if(attacker.MeleeWeapon != null && map.getDistanceBetweenFields(deffender.Field, attacker.Field) == 1)
				return true;
			else
				return false;
		}

		public void execute()
		{
			if(this.isExecutable())
			{
				deffender.Health -= attacker.MeleeWeapon.Damage;
				if (deffender.isDead)
				{
					LootGenerator lootGen = new LootGenerator();
					lootGen.generateLoot(attacker, deffender);
					deffender.Field.removeCreature();
				}
			}
		}
	}
}
