using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class MedKit : GameObject
	{
		public int Health { get; set; }
		
		public void objectPickedBy(Creature creature)
		{
			creature.Health += this.Health;
		}
	}
}
