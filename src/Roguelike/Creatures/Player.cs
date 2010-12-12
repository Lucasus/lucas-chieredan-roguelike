using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class Player
    {
		public Map map;

		public Creature Creature { get; set; }

		public Player(Map map)
		{
			this.map = map;
		}

		public void executeCommand(IPlayerCommand command)
		{
			command.execute(this);
		}
    }
}
