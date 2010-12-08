using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class CreatureVisitor : IFieldVisitor
	{
		Creature creature;
		public VisitResult visitResult;

		public CreatureVisitor(Creature creature)
		{
			this.creature = creature;
		}

		public void visit(Wall wall)
		{
			visitResult = new VisitResult { moved = false };
		}

		public void visit(Floor floor)
		{
			if(floor.Creature == null)
			{
				floor.putCreature(creature);
				visitResult = new VisitResult { moved = true };
			}
			else
			{
				creature.attack(floor.Creature);
				visitResult = new VisitResult { moved = false };
			}
		}
	}
}
