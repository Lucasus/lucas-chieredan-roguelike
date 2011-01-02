using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class MoveCommand : ICreatureCommand
	{
		public enum Direction
		{
			RightUp = 1,
			Right = 2,
			RightDown = 3,
			Down = 4,
			LeftDown = 5,
			Left = 6,
			LeftUp = 7,
			Up = 8
		}

		public Direction moveDirection;
		private Map map;
		private Creature creature;

		public MoveCommand(Creature creature, Direction moveDirection, Map map)
		{
			this.creature = creature;
			this.moveDirection = moveDirection;
			this.map = map;
		}

		public void execute()
		{
			if (creature.Field != null)
			{
				int newX = creature.X;
				int newY = creature.Y;
				if (moveDirection == Direction.RightUp || moveDirection == Direction.Right || moveDirection == Direction.RightDown)
					newX += 1;
				else if (moveDirection == Direction.LeftUp || moveDirection == Direction.Left || moveDirection == Direction.LeftDown)
					newX -= 1;

				if (moveDirection == Direction.LeftDown || moveDirection == Direction.Down || moveDirection == Direction.RightDown)
					newY += 1;
				else if (moveDirection == Direction.LeftUp || moveDirection == Direction.Up || moveDirection == Direction.RightUp)
					newY -= 1;

				creature.interactWithField(map[newY, newX]);
			}
			else
				throw new CreatureException();
		}
	}
}
