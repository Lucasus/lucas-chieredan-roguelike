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

		public Creature(int health)
		{
			if(health <= 0)
				throw new CreatureException();
			else 
			{
				this.maxHealth = health;
				this.health = health;
			}
			this.MeleeWeapon = new Weapon();
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
			if(this.MeleeWeapon != null)
			{
				int distance = Math.Max(Math.Abs(this.X - creature.X), Math.Abs(this.Y - creature.Y));
				if(distance > 1)
					return false;
				else
					return true;
			}
			else
				return false;
		}

		public bool canShoot(Creature creature)
		{
			if(this.RangedWeapon != null)
			{
				int distance = Math.Max(Math.Abs(this.X - creature.X), Math.Abs(this.Y - creature.Y));
				if (distance > this.RangedWeapon.Range)
					return false;
				else
					return true;
			}
			else
				return false;
		}

		public void pickupItems()
		{
			foreach (IGameObject gameObject in field.Objects)
			{
				gameObject.objectPickedBy(this);
			}
			field.Objects.Clear();
		}
		
		public void shoot(Creature creature)
		{
			if(this.canShoot(creature))
				new ShootOut(new RandomNumberGenerator()).commenceInteraction(this, creature);
		}

		public void attack(Creature creature)
		{
			if(this.canAttack(creature))
				new Fight().commenceInteraction(this, creature);
		}
    }
}
