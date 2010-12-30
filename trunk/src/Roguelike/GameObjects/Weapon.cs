using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class Weapon : GameObject
	{
		public int Range { get; set; }
		public int Damage { get; set; }

		public double Chance { get; set; }

		public Weapon()
		{
			Range = 1;
			Damage = 1;
			Chance = 1;
		}

		public void objectPickedBy(Creature creature)
		{
			creature.Weapon = this;
		}
	}
}
