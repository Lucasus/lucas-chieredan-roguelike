using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class Player
    {
		public enum Direction{
			RightUp,
			Right,
			RightDown,
			Down,
			LeftDown,
			Left,
			LeftUp,
			Up
		}

		public Creature Creature { get; set; }

		public void move(Direction dir)
		{
			throw new NotImplementedException();
		}
    }
}
