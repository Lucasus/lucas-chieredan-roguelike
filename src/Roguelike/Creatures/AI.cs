using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class AI
	{
		Map map;
		Player player;

		public AI(Map map, Player player)
		{
			this.map = map;
			this.player = player;
		}

		public void move(Creature creature)
		{
			if(creature.field != null)
			{
				if(creature.canAttack(player.Creature))
				{
					creature.attack(player.Creature);
				}
			}
			else
				throw new CreatureException();
		}
	}
}
