using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class AI
	{
		private Map map;
		private Creature player;
		private List<Creature> creatures;
		public List<Creature> Creatures
		{
			get { return this.creatures; }
		}

		public AI(Map map, Creature player)
		{
			this.map = map;
			this.player = player;
			this.creatures = new List<Creature>();
		}

		public void addCreature(Creature creature)
		{
			creatures.Add(creature);
		}

		public void act()
		{
			creatures.RemoveAll(x => x.isDead || x.Field == null);

			foreach(Creature creature in creatures)
			{
				this.move(creature);
			}
		}

		public void move(Creature creature)
		{
			if(!creature.isDead)
			{
				if(creature.Field != null)
				{
					if(!player.isDead)
					{
						if(map.getDistanceBetweenFields(player.Field, creature.Field) == 1)
						{
							new AttackCommand(creature, player).execute();
						}
						else
						{
							int newX = creature.X;
							int newY = creature.Y;
							if(player.X > creature.X)
								newX += 1;
							else if(player.X < creature.X)
								newX -= 1;

							if(player.Y > creature.Y)
								newY += 1;
							else if(player.Y < creature.Y)
								newY -= 1;
							if (this.map[newY, newX].Creature == null)
								creature.interactWithField(this.map[newY, newX]);
						}
					}
				}
				else
					throw new CreatureException();
			}
		}
	}
}
