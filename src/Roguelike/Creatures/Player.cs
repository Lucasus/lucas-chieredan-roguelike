using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roguelike.Creatures;

namespace Roguelike
{
    public class Player
    {
		private Map map;
		private int playerHealth = 20;

		public enum Direction{
			RightUp = 1,
			Right = 2,
			RightDown = 3,
			Down = 4,
			LeftDown = 5,
			Left = 6,
			LeftUp = 7,
			Up = 8
		}

		public Creature Creature { get; set; }

		public Player(Map map)
		{
			this.map = map;
			this.Creature = new PlayerCreature(20);
		}

		public void move(Direction dir)
		{
			if(Creature.field != null)
			{
				int newX = Creature.X;
				int newY = Creature.Y;
				if(dir == Direction.RightUp || dir == Direction.Right || dir == Direction.RightDown)
					newX += 1;
				else if(dir == Direction.LeftUp || dir == Direction.Left || dir == Direction.LeftDown)
					newX -= 1;
			
				if(dir == Direction.LeftDown || dir == Direction.Down || dir == Direction.RightDown)
					newY += 1;
				else if(dir == Direction.LeftUp || dir == Direction.Up || dir == Direction.RightUp)
					newY -= 1;

				map[Creature.X, Creature.Y].Creature = null;
				map[newX, newY].Creature = Creature;
				Creature.X = newX;
				Creature.Y = newY;
				//VisitResult result = Creature.interactWithField(map[newX, newY]);
				//if (result.moved == true)
				//{
				//}
			}
			else
				throw new CreatureException();
		}
    }
}
