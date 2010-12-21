using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class AttackCommand : IPlayerCommand
	{
		Creature target;
		public AttackCommand(Creature target)
		{
			this.target = target;
		}

		public void execute(Player player)
		{
			player.Creature.attack(target);
		}
	}
}
