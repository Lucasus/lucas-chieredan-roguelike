﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class LootGenerator
	{
		static private Random randomGenerator = new Random();

		public void generateLoot(Creature player, Creature creature)
		{
			Random r = new Random();
			int generatedValue = randomGenerator.Next(5);
			if (generatedValue == 0)
			{
				creature.Field.placeObject(new Money() { Worth = randomGenerator.Next(5) + 1 });
			}
			else if (generatedValue == 1)
			{
				creature.Field.placeObject(new MedKit() { Health = randomGenerator.Next(8) + 3 });
			}
			else if (generatedValue == 2)
			{
				creature.Field.placeObject(new Weapon() { Damage = randomGenerator.Next(10) + 1 });
			}
			else if (generatedValue == 3)
			{
				creature.Field.placeObject(new RangedWeapon() { Damage = randomGenerator.Next(10) + 1, Range = randomGenerator.Next(10) + 1, Chance = randomGenerator.NextDouble() });
			}
			else
			{
				creature.Field.placeObject(new GrenadeWeapon() { Damage = randomGenerator.Next(10) + 1, Range = randomGenerator.Next(10) + 1, Spread = randomGenerator.Next(4), Count = 1 + randomGenerator.Next(3) });
			}
		}
		int getRandomizedDamage(Random r, int baseDamage)
		{
			return (int)((baseDamage + 1) / 1.5 + r.Next(baseDamage + 2) / 1.5);
		}
	}
}
