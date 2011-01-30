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
			int generatedValue = randomGenerator.Next(23);
			if (0 <= generatedValue && generatedValue <= 3)
			{
				int spread = randomGenerator.Next(4) + 1;
				f.placeObject(new GrenadeWeapon() { Damage = randomGenerator.Next(10) + 5, Range = randomGenerator.Next(10) + spread, Spread = spread, Ammo = 1 + randomGenerator.Next(3) });
			}
			else if (4 <= generatedValue && generatedValue <= 7)
			{
				Weapon w = new Weapon() { Damage = randomGenerator.Next(10) + 1 };
				w.BrokeChance = (double)(1 + w.Damage/5 + r.Next(4)) / 100;
				f.placeObject(w);
			}
			else if (8 <= generatedValue && generatedValue <= 11)
			{
				f.placeObject(new RangedWeapon() { Damage = randomGenerator.Next(10) + 1, Range = randomGenerator.Next(10) + 1, Chance = 0.1 + 0.9*randomGenerator.NextDouble(), Ammo = 10 + r.Next(15) });
			}
			else if (12 <= generatedValue && generatedValue <= 16)
			{
				f.placeObject(new Ammo() { Bullets = randomGenerator.Next(8) + 4, Grenades = randomGenerator.Next(3) });
			}
			else if (17 <= generatedValue && generatedValue <= 22)
			{
				f.placeObject(new MedKit() { Health = randomGenerator.Next(8) + 3 });
			}
		}

		int getRandomizedDamage(Random r, int baseDamage)
		{
			return (int)((baseDamage + 1) / 1.5 + r.Next(baseDamage + 2) / 1.5);
		}
	}
}
