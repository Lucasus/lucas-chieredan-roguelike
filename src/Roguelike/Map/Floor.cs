using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class Floor : Field
	{
		public Floor(int x, int y) : base(x, y)
		{
		}

		public override bool blocksSight()
		{
			return false;
		}

		public override bool putCreature(Creature thing)
		{
			if (this.Creature == null)
			{
				this.Creature = thing;
				this.Creature.Field = this;
				return true;
			}
			else
			{
				return false;
			}
		}

		public override void removeCreature()
		{
			this.Creature.Field = null;
			this.Creature = null;
		}

		public override bool placeObject(IGameObject gameObject)
		{
			Objects.Add(gameObject);
			return true;
		}

		public override void accept(IFieldVisitor visitor)
		{
			visitor.visit(this);
		}
	}
}
