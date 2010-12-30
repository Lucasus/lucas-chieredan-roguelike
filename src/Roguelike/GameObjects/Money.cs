using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class Money : IGameObject
	{
		public int Worth { get; set; }

		public void objectPickedBy(Creature creature)
		{
			creature.Money += this.Worth;
		}
	}
}
