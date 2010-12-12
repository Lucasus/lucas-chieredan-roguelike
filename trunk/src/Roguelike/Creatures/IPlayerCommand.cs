using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public interface IPlayerCommand
	{
		void execute(Player player);
	}
}
