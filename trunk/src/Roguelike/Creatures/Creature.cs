using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class Creature
	{
		private Field field;
		private int maxHealth;

		public AI AI { get; set; }
		public int PickedPointsCount { get; set; }
		public int PanicModeCounter { get; set; }
		public int MaxHealth { get { return maxHealth; } }
		public bool isDead { get { return Health <= 0; } }
		public bool SawPlayer { get; set; }
		public string CreatureType { get; set; }
		public int Health { get; set; }
		public int SightRange { get; set; }
		public MeleeWeapon MeleeWeapon { get; set; }
		public RangedWeapon RangedWeapon { get; set; }
		public GrenadeWeapon GrenadeWeapon { get; set; }
		public int Money { get; set; }
		public int X { get { return field.X; } }
		public int Y { get { return field.Y; } }

		public string MianownikName { get; set; }
		public string BiernikName { get; set; }

		public Field Field
		{
			get { return field; }
			set
			{
				if (this.field != null)
					this.field.Creature = null;
				this.field = value;
			}
		}

		public Creature(int health)
		{
			if(health <= 0)
				throw new CreatureException();
			else 
			{
				this.maxHealth = health;
				Health = health;
			}
			this.CreatureType = "Creature";
			this.PanicModeCounter = 0;
			this.SightRange = 10;
			this.SawPlayer = false;
			this.PickedPointsCount = 0;
		}

		public bool canInteractWithField(Field field)
		{
			if (!(field is Wall) && field.Creature == null)
				return true;
			return false;
		}

		public void interactWithField(Field field)
		{
			CreatureVisitor visitor = new CreatureVisitor(this);
			field.accept(visitor);
		}
	}
}
