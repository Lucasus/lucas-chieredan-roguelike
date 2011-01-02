using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class Wall : Field
    {
		public Wall(int x, int y) : base(x,y){}

		public override bool blocksSight()
		{
			return true;
		}

		public override bool putCreature(Creature thing)
		{
			return false;
		}
		public override void removeCreature() { }
		public override bool placeObject(IGameObject field)
		{
			return false;
		}

		public override void accept(IFieldVisitor visitor)
		{
			visitor.visit(this);
		}
    }
}
