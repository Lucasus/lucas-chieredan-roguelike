using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class Weapon : IGameObject
	{
		public int Damage { get; set; }

		public Weapon()
		{
			Damage = 1;
		}

		public virtual void objectPickedBy(Creature creature)
		{
			creature.MeleeWeapon = this;
			AbstractLogger.Current.Log(creature.MianownikName + " zebrał nóż zadający " + this.Damage + " obrażeń.");
		}
	}
}
