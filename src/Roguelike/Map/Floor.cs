using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class Floor : Field
    {
		private Creature creature;
		private List<GameObject> objects;

		public Creature Creature
		{
			get { return creature; }
			set { creature = value; }
		}

		public Floor(int x, int y) : base(x, y)
		{
			objects = new List<GameObject>();
		}

		public override bool putCreature(Creature thing)
		{
			if(this.Creature == null)
			{
				this.Creature = thing;
				this.Creature.field = this;
				foreach(GameObject gameObject in objects)
				{
					gameObject.objectPickedBy(creature);
				}
				objects.Clear();
				return true;
			}
			else
				return false;
		}

		public override void removeCreature()
		{
			this.Creature.field = null;
			this.Creature = null;
		}

		public override bool placeObject(GameObject gameObject)
		{
			objects.Add(gameObject);
			return true;
		}

		public override void accept(IFieldVisitor visitor)
		{
			visitor.visit(this);
		}
    }
}
