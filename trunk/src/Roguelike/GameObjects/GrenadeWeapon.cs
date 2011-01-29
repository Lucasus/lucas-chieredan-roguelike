﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class GrenadeWeapon : RangedWeapon
	{
		public int Spread { get; set; }
		public int Count { get; set; }
	
		public GrenadeWeapon()
		{
			Spread = 2;
			Count = 1;
		}

		public override void objectPickedBy(Creature creature)
		{
			creature.GrenadeWeapon = this;
			AbstractLogger.Current.Log(creature.MianownikName + " zebrał granat zadający " + this.Damage + " obrażeń, o obszarze o średnicy " + this.Spread + "pól i zasięgu " + this.Range);
		}
	}
}