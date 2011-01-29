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
		private bool interactIfCreature = false;

		public MoveCommand(Creature creature, Direction moveDirection, Map map)
		{
			this.creature = creature;
			this.moveDirection = moveDirection;
			this.map = map;
			this.interactIfCreature = true;
		}

		public MoveCommand(Creature creature, int xDir, int yDir, Map map, bool interactIfCreature)
		{
			Direction moveDirection = Direction.Down;
			if (xDir == -1 && yDir == -1) moveDirection = Direction.LeftUp;
			if (xDir == -1 && yDir == 0) moveDirection = Direction.Left;
			if (xDir == -1 && yDir == 1) moveDirection = Direction.LeftDown;
			if (xDir == 0 && yDir == -1) moveDirection = Direction.Up;
			if (xDir == 0 && yDir == 1) moveDirection = Direction.Down;
			if (xDir == 1 && yDir == -1) moveDirection = Direction.RightUp;
			if (xDir == 1 && yDir == 0) moveDirection = Direction.Right;
			if (xDir == 1 && yDir == 1) moveDirection = Direction.RightDown;

			this.creature = creature;
			this.moveDirection = moveDirection;
			this.map = map;
			this.interactIfCreature = interactIfCreature;
		}

		public bool isExecutable()
		{
			if(creature.Field != null)
				return true;
			else
				return false;
		}

		public void execute()
		{
			if (this.isExecutable())
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

				if (this.map[newY, newX].Creature == null || this.interactIfCreature)
					creature.interactWithField(map[newY, newX]);
			}
			else
				throw new CreatureException();
		}
	}
}
