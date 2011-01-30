using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class ShootCommand : ICreatureCommand
	{
		public static bool CanShoot(Map map, Creature attacker, Creature target)
		{
			if (attacker.RangedWeapon != null
				&& map.getDistanceBetweenFields(attacker.Field, target.Field) <= attacker.RangedWeapon.Range
				&& map.isSightBetweenFields(attacker.Field, target.Field))
				return true;
			return false;
		}

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
			if (attacker.RangedWeapon != null && attacker.RangedWeapon.Ammo > 0 && map.getDistanceBetweenFields(attacker.Field, deffender.Field) <= attacker.RangedWeapon.Range && map.isSightBetweenFields(attacker.Field, deffender.Field))
				return true;
			else
				return false;
		}

		public void execute()
		{
			if (CanShoot(map, attacker, deffender))
			{
				attacker.RangedWeapon.Ammo -= 1;
				if (randomNumberGenerator.generateNumber() <= attacker.RangedWeapon.Chance)
				{
					int damage = attacker.RangedWeapon.Damage;
					deffender.Health -= damage;
					if (deffender.isDead)
					{
						LootGenerator lootGen = new LootGenerator();
						lootGen.generateLoot(attacker, deffender);
						deffender.Field.removeCreature();
						AbstractLogger.Current.Log(attacker.MianownikName + " zabił " + deffender.BiernikName);
					}
					else
					{
						if (deffender.Health * 2 < deffender.MaxHealth)
						{
							deffender.PanicModeCounter = 20;
							AbstractLogger.Current.Log(attacker.MianownikName + " strzelił " + deffender.BiernikName + " i zadał mu " + damage + " obrażeń. " + deffender.MianownikName + " ucieka w popłochu");
						}
						else
						{
							AbstractLogger.Current.Log(attacker.MianownikName + " strzelił do " + deffender.BiernikName + " i zadał mu " + damage + " obrażeń");
						}
					}
				}
				else
				{
					AbstractLogger.Current.Log(attacker.MianownikName + " strzelił do " + deffender.BiernikName + " ale nie trafił.");
				}
			}
		}
	}
}
