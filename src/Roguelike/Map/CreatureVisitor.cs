using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class CreatureVisitor : IFieldVisitor
	{
		Creature creature;

		public CreatureVisitor(Creature creature)
		{
			this.creature = creature;
		}

		public void visit(Wall wall)
		{

		}

		public void visit(Floor floor)
		{
			if(floor.Creature == null)
			{
				floor.putCreature(creature);
			}
			else
			{
				new AttackCommand(creature, floor.Creature).execute();
			}
		}
	}
}
