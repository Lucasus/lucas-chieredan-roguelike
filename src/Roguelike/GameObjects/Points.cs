using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class Points : IGameObject
	{
		public int Value { get; set; }
		public void objectPickedBy(Creature creature)
		{
			AbstractLogger.Current.Log(creature.MianownikName + " zebrał " + Value + " punktów.");
		}
	}
}
