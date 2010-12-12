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
			get{return health<0;}
		}

		private string creatureType;
		public string CreatureType{
			get{ return creatureType; }
			set{ creatureType = value; }
		}


		private Field field;

		private int health;
		public int Health 
		{ 
			get
			{
				return health;
			}
			set
			{
				if(value<0)
				{
					this.die();
				}
				else
					health = value;
			}
		}
		public Weapon Weapon { get; set; }
		public int Money { get; set; }

		public Field Field
		{
			get {return field;}
			set {
				if(this.field != null)
					this.field.Creature = null;
				this.field = value;
			}
		}

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

		public Creature(int health = 10)
		{
			if(health < 0)
				throw new CreatureException();
			else 
				this.health = health;

			this.Weapon = new Weapon(){ Damage = 1, Range = 1 };
			this.creatureType = "Creature";
		}

		public bool canInteractWithField(Field field)
		{
			int distance = Math.Max(Math.Abs(this.X - field.X), Math.Abs(this.Y - field.Y));
			if (distance > 1)
				return false;
			else
				return true;
		}

		public void interactWithField(Field field)
		{
			CreatureVisitor visitor = new CreatureVisitor(this);
			field.accept(visitor);
		}

		public bool canAttack(Creature creature)
		{
			int distance = Math.Max(Math.Abs(this.X - creature.X), Math.Abs(this.Y - creature.Y));
			if(distance > this.Weapon.Range)
				return false;
			else
				return true;
		}

		public void pickupItems()
		{
			foreach (GameObject gameObject in field.Objects)
			{
				gameObject.objectPickedBy(this);
			}
			field.Objects.Clear();
		}

		public void attack(Creature creature)
		{
			creature.Health -= this.Weapon.Damage;
		}

		public void die()
		{
			LootGenerator lootGen = new LootGenerator();
			lootGen.generateLoot(this);
			this.field.removeCreature();
		}
    }
}
