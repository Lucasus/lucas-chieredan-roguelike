using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class Creature
    {
		public bool isDead 
		{
			get{return health<=0;}
		}

		private string creatureType;
		public string CreatureType{
			get{ return creatureType; }
			set{ creatureType = value; }
		}


		private Field field;
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

		private int maxHealth;
		public int MaxHealth
		{
			get { return maxHealth; }
		}

		private int health;
		public int Health 
		{ 
			get	{ return health;}
			set	{ health = value; }
		}
		public Weapon MeleeWeapon { get; set; }
		public RangedWeapon RangedWeapon { get; set; }
		public GrenadeWeapon GrenadeWeapon { get; set; }
		public int Money { get; set; }

		public int X
		{
			get { return field.X; }
			set { field.X = value; }
		}

		public int Y
		{
			get { return field.Y; }
			set { field.Y = value; }
		}

		public Creature(int health)
		{
			if(health <= 0)
				throw new CreatureException();
			else 
			{
				this.maxHealth = health;
				this.health = health;
			}
			this.creatureType = "Creature";
		}

		public void interactWithField(Field field)
		{
			CreatureVisitor visitor = new CreatureVisitor(this);
			field.accept(visitor);
		}
    }
}
