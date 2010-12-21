using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class PickupCommand : IPlayerCommand
	{
		public void execute(Player player)
		{
			player.Creature.pickupItems();
		}
	}
}
