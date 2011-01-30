using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class MeleeWeapon : Weapon
	{
		public double BrokeChance { get; set; }

		public MeleeWeapon()
		{
			BrokeChance = 0;
		}

		public override void objectPickedBy(Creature creature)
		{
			creature.MeleeWeapon = this;
			AbstractLogger.Current.Log(creature.MianownikName + " zebrał nóż zadający " + this.Damage + " obrażeń.");
		}
	}
}
