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
			if (randomGenerator.Next(2) == 0)
				creature.Field.placeObject(new Money() { Worth = 5 });
			else
				creature.Field.placeObject(new MedKit() { Health = 5 });
		}
	}
}
