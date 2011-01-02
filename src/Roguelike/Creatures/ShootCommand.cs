using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class ShootCommand : ICreatureCommand
	{
		private IRandomNumberGenerator randomNumberGenerator;
		private Creature deffender;
		private Creature attacker;
		private Map map;
		public ShootCommand(Creature attacker, Creature target, Map map, IRandomNumberGenerator randomNumberGenerator)
		{
			this.randomNumberGenerator = randomNumberGenerator;
			this.deffender = target;
			this.attacker = attacker;
			this.map = map;
		}

		public bool isExecutable()
		{
			if (attacker.RangedWeapon != null && map.getDistanceBetweenFields(attacker.Field, deffender.Field) <= attacker.RangedWeapon.Range && map.isSightBetweenFields(attacker.Field, deffender.Field))
				return true;
			else
				return false;
		}

		public void execute()
		{
			if (attacker.RangedWeapon != null && map.getDistanceBetweenFields(attacker.Field, deffender.Field) <= attacker.RangedWeapon.Range && map.isSightBetweenFields(attacker.Field, deffender.Field))
			{
				if (randomNumberGenerator.generateNumber() <= attacker.RangedWeapon.Chance)
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
}
