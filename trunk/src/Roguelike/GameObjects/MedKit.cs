using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class MedKit : IGameObject
	{
		public int Health { get; set; }
		
		public void objectPickedBy(Creature creature)
		{
			int oldHealth = creature.Health;

			if(creature.Health + this.Health <= creature.MaxHealth)
				creature.Health += this.Health;
			else
				creature.Health = creature.MaxHealth;

			AbstractLogger.Current.Log(creature.MianownikName + " wyleczył " + (creature.Health - oldHealth) + " zdrowia.");
		}
	}
}
