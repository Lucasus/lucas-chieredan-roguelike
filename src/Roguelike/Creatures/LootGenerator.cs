using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class LootGenerator
	{
		static private Random randomGenerator = new Random();

		public void generateLoot(Creature creature)
		{
			int generatedValue = randomGenerator.Next(4);
			if (generatedValue == 0)
				creature.Field.placeObject(new Money() { Worth = randomGenerator.Next(5) + 1 });
			else if(generatedValue == 1)
				creature.Field.placeObject(new MedKit() { Health = randomGenerator.Next(5) + 1 });
			else if(generatedValue == 2)
				creature.Field.placeObject(new Weapon() { Damage = randomGenerator.Next(10) + 1});
			else
				creature.Field.placeObject(new RangedWeapon() { Damage = randomGenerator.Next(10) + 1, Range = randomGenerator.Next(10) + 1, Chance = randomGenerator.NextDouble()});
		}
	}
}
