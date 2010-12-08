using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike.GameObjects
{
	public class CreatureMoneyMediator : IObjectMediator
	{
		private Creature creature;
		private Money money;
		public CreatureMoneyMediator(Creature creature, Money money)
		{
			this.creature = creature;
			this.money = money;
		}

		public void mediate()
		{
			this.creature.Money += money.Worth;
		}
	}
}
