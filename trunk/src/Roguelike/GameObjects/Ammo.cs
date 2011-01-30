using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class Ammo : IGameObject
	{
		public int Bullets { get; set; }
		public int Grenades { get; set; }

		public void objectPickedBy(Creature creature)
		{
			creature.RangedWeapon.Ammo += Bullets;
			creature.GrenadeWeapon.Ammo += Grenades; 
			AbstractLogger.Current.Log(creature.MianownikName + " zebrał skrzynkę z amonicją.");
		}
	}
}
