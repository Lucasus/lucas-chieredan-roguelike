using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class ShootCommand : IPlayerCommand
	{
		Creature target;
		public ShootCommand(Creature target)
		{
			this.target = target;
		}

		public void execute(Player player)
		{
			player.Creature.shoot(target);
		}
	}
}
