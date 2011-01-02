using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class PickupCommand : ICreatureCommand
	{
		private Creature creature;
		public PickupCommand(Creature creature)
		{
			this.creature = creature;
		}

		public void execute()
		{
			foreach (IGameObject gameObject in creature.Field.Objects)
			{
				gameObject.objectPickedBy(creature);
			}
			creature.Field.Objects.Clear();
		}
	}
}
