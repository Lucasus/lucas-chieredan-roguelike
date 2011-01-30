using System;
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
			Field f = creature.Field;
			generateLoot(f);
		}

		public void generateLoot(Field f)
		{
			Random r = new Random();
			int generatedValue = randomGenerator.Next(5);
			if (generatedValue == 0)
			{
				f.placeObject(new Ammo() { Bullets = randomGenerator.Next(10) + 5, Grenades = randomGenerator.Next(3) });
			}
			else if (generatedValue == 1)
			{
				f.placeObject(new MedKit() { Health = randomGenerator.Next(8) + 3 });
			}
			else if (generatedValue == 2)
			{
				Weapon w = new Weapon() { Damage = randomGenerator.Next(10) + 1 };
				w.BrokeChance = (double)(3 + w.Damage/5 + r.Next(4)) / 100;
				f.placeObject(w);
			}
			else if (generatedValue == 3)
			{
				f.placeObject(new RangedWeapon() { Damage = randomGenerator.Next(10) + 1, Range = randomGenerator.Next(10) + 1, Chance = 0.1 + 0.9*randomGenerator.NextDouble(), Ammo = 10 + r.Next(15) });
			}
			else
			{
				f.placeObject(new GrenadeWeapon() { Damage = randomGenerator.Next(10) + 1, Range = randomGenerator.Next(10) + 1, Spread = randomGenerator.Next(4), Ammo = 1 + randomGenerator.Next(3) });
			}
		}

		int getRandomizedDamage(Random r, int baseDamage)
		{
			return (int)((baseDamage + 1) / 1.5 + r.Next(baseDamage + 2) / 1.5);
		}
	}
}
