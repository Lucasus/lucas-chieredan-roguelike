﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class RangedWeapon : Weapon
	{
		public double Chance { get; set; }
		public int Range { get; set; }

		public RangedWeapon()
		{
			Chance = 1;
			Range = 1;
		}

		public override void objectPickedBy(Creature creature)
		{
			creature.RangedWeapon = this;
		}
	}
}
