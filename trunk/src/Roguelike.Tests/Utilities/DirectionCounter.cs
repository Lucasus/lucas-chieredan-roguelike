using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike.Tests.Utilities
{
	public class DirectionCounter
	{
		private Dictionary<MoveCommand.Direction, int> dirCount = new Dictionary<MoveCommand.Direction,int>();
		private int totalCount = 0;

		public void AddDirection(ICreatureCommand command)
		{
			MoveCommand mCommand = (MoveCommand)command;
			if (!dirCount.ContainsKey(mCommand.moveDirection))
				dirCount.Add(mCommand.moveDirection, 0);
			dirCount[mCommand.moveDirection]++;
			totalCount++;
		}

		public int GetMinDirectionCount()
		{
			int min = Int32.MaxValue;
			foreach (int count in dirCount.Values)
			{
				if (count < min)
					min = count;
			}
			return min;
		}

		public int GetMaxDirectionCount()
		{
			int max = 0;
			foreach (int count in dirCount.Values)
			{
				if (count > max)
					max = count;
			}
			return max;
		}

	}
}
