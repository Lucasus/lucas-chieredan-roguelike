using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	interface ICreatureInteraction
	{
		void commenceInteraction(Creature initiator, Creature subject);
	}
}
