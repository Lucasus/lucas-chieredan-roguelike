using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public abstract class Field
    {
		private int x;
		private int y;

		public int X
		{
			get{ return x ;}
			set { x = value; }
		}
		public int Y
		{
			get{ return y ;}
			set { y = value; }
		}

		public Field(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

        public abstract bool putCreature(Creature thing);
		public abstract void removeCreature();
		public abstract bool placeObject(GameObject field);
		public abstract void accept(IFieldVisitor visitor);
		public virtual Creature Creature { get; set; }
    }
}
