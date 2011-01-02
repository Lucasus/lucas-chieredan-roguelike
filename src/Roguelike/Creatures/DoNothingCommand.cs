using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class DoNothingCommand : ICreatureCommand
	{
		private Creature creature;
		public DoNothingCommand(Creature creature)
		{
			this.creature = creature;
		}

		public bool isExecutable()
		{
			return true;
		}

		public void execute()
		{
		}
	}
}
