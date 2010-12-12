using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class DoNothingCommand : IPlayerCommand
	{
		public void execute(Player player)
		{
		}
	}
}
