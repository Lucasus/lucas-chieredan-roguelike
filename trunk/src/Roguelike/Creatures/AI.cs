using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class AI
	{
		private Map map;
		private Player player;
		private List<Creature> creatures;

		public AI(Map map, Player player)
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
					if(!player.Creature.isDead)
					{
						if(creature.canAttack(player.Creature))
						{
							creature.attack(player.Creature);
						}
						else
						{
							int newX = creature.X;
							int newY = creature.Y;
							if(player.Creature.X > creature.X)
								newX += 1;
							else if(player.Creature.X < creature.X)
								newX -= 1;

							if(player.Creature.Y > creature.Y)
								newY += 1;
							else if(player.Creature.Y < creature.Y)
								newY -= 1;
					
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
