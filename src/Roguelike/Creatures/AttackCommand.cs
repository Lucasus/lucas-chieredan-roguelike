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
				int damage = attacker.MeleeWeapon.Damage;
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
						AbstractLogger.Current.Log(attacker.MianownikName + " zaatakował nożem " + deffender.BiernikName + " i zadał mu " + damage + " obrażeń. " + deffender.MianownikName + " ucieka w popłochu");
					}
					else
					{
						AbstractLogger.Current.Log(attacker.MianownikName + " zaatakował nożem " + deffender.BiernikName + " i zadał mu " + damage + " obrażeń");
					}
				}
				Random r = RandomNumberGenerator.GlobalRandom;
				if (r.Next(100) < attacker.MeleeWeapon.BrokeChance * 100)
				{
					attacker.MeleeWeapon.Damage = (attacker.MeleeWeapon.Damage + 1) / 2;
					AbstractLogger.Current.Log("Twój nóż uległ uszkodzeniu. Daje teraz tylko " + attacker.MeleeWeapon.Damage + " obrażeń");
				}
			}
		}
	}
}
