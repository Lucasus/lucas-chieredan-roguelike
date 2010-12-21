using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class MoveCommand : IPlayerCommand
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

		public MoveCommand(Direction moveDirection)
		{
			this.moveDirection = moveDirection;
		}

		public void execute(Player player)
		{
			if (player.Creature.Field != null)
			{
				int newX = player.Creature.X;
				int newY = player.Creature.Y;
				if (moveDirection == Direction.RightUp || moveDirection == Direction.Right || moveDirection == Direction.RightDown)
					newX += 1;
				else if (moveDirection == Direction.LeftUp || moveDirection == Direction.Left || moveDirection == Direction.LeftDown)
					newX -= 1;

				if (moveDirection == Direction.LeftDown || moveDirection == Direction.Down || moveDirection == Direction.RightDown)
					newY += 1;
				else if (moveDirection == Direction.LeftUp || moveDirection == Direction.Up || moveDirection == Direction.RightUp)
					newY -= 1;

				player.Creature.interactWithField(player.map[newY, newX]);
			}
			else
				throw new CreatureException();
		}
	}
}
