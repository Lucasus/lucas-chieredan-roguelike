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
				f.placeObject(new Ammo() { Bullets = randomGenerator.Next(5) + 1, Grenades = 1 });
			}
			else if (generatedValue == 1)
			{
				f.placeObject(new MedKit() { Health = randomGenerator.Next(8) + 3 });
			}
			else if (generatedValue == 2)
			{
				f.placeObject(new Weapon() { Damage = randomGenerator.Next(10) + 1 });
			}
			else if (generatedValue == 3)
			{
				f.placeObject(new RangedWeapon() { Damage = randomGenerator.Next(10) + 1, Range = randomGenerator.Next(10) + 1, Chance = randomGenerator.NextDouble() });
			}
			else
			{
				f.placeObject(new GrenadeWeapon() { Damage = randomGenerator.Next(10) + 5, Range = randomGenerator.Next(10) + 1, Spread = randomGenerator.Next(3)+1, Ammo = 1 + randomGenerator.Next(3) });
			}
		}
	}
}
