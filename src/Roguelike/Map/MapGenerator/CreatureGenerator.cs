using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class CreatureGenerator
	{
		public Creature GetRandomCreature()
		{
			Random r = RandomNumberGenerator.GlobalRandom;

			Creature enemy = new Creature(7 + r.Next(13))
			{
				CreatureType = "Enemy",
				MianownikName = "Gangster",
				BiernikName = "gangstera",
				MeleeWeapon = new Weapon() { Damage = 1 + r.Next(2), BrokeChance = 0 },
				RangedWeapon = new RangedWeapon() { Chance = (double)(10 + r.Next(80)) / 100, Damage = 1, Range = 2 + r.Next(7), Ammo = 5 + r.Next(15) }
			};

			return enemy;

		}
	}
}
