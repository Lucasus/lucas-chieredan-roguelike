using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class Creature
    {
		public int Health { get; set; }
		public Weapon Weapon { get; set; }
		public int Money { get; set; } 
		public int X { get; set; }
		public int Y { get; set; }

		public bool canInteractWithField(Field field)
		{
			throw new NotImplementedException();
		}

		public void interactWithField(Field field)
		{
			throw new NotImplementedException();
		}

		public bool canAttack(Creature creature)
		{
			throw new NotImplementedException();
		}

		public void attack(Creature creature)
		{
			throw new NotImplementedException();
		}
    }
}
