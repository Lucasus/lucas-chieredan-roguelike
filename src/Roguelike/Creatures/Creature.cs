using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class Creature
    {
		public Field field;

		private int health;
		public int Health { get; set; }
		public Weapon Weapon { get; set; }
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

		public Creature():this(0){}

		public Creature(int health)
		{
			if(health < 0)
				throw new CreatureException();
			else this.health = health;
		}

		public bool canInteractWithField(Field field)
		{
			int distance = Math.Max(Math.Abs(this.X - field.X), Math.Abs(this.Y - field.Y));
			if (distance > 1)
				return false;
			else
				return true;
		}

		public VisitResult interactWithField(Field field)
		{
			CreatureVisitor visitor = new CreatureVisitor(this);
			field.accept(visitor);
			return visitor.visitResult;
		}

		public bool canAttack(Creature creature)
		{
			int distance = Math.Max(Math.Abs(this.X - creature.X), Math.Abs(this.Y - creature.Y));
			if(distance > this.Weapon.Range)
				return false;
			else
				return true;
		}

		public void attack(Creature creature)
		{
			creature.Health -= this.Weapon.Damage;
		}
    }
}
