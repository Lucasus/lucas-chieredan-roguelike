using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public abstract class Weapon : IGameObject
	{
		public int Damage { get; set; }
		

		public Weapon()
		{
			Damage = 1;
		}

		public abstract void objectPickedBy(Creature creature);
	}
}
