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
				ICreatureCommand creatureCommand = this.generateNextCommand(creature);
				if(creatureCommand != null)
					creatureCommand.execute();
			}
		}

		public ICreatureCommand generateNextCommand(Creature creature)
		{
			ICreatureCommand command = null;
			if(creature.Field != null)
			{
				if(!player.isDead)
				{
					if(map.getDistanceBetweenFields(player.Field, creature.Field) == 1)
					{
						command = new AttackCommand(creature, player, map);
					}
					else
					{
                        int minDistance = int.MaxValue;
                        int bestX = 0;
                        int bestY = 0;
                        for (int i = -1; i <= 1; ++i)
                        {
                            for (int j = -1; j <= 1; ++j)
                            {
                                Field f = this.map[creature.Y + j,creature.X + i];
                                if(f != null && creature.canInteractWithField(f) == true)
                                {
                                    int distance = map.getEuclideanDistanceBetweenFields(player.Field, f);
                                    if (distance < minDistance)
                                    {
                                        minDistance = distance;
                                        bestX = creature.X + i;
                                        bestY = creature.Y + j;
                                    }
                                }
                            }
                        }

                        Random r = new Random();

                        if (minDistance > 10 || (map.isSightBetweenFields(player.Field, creature.Field) == false && minDistance > 3))
                        {
                            bestX = creature.X + r.Next(3) - 1;
                            bestY = creature.Y + r.Next(3) - 1;
                        }

                        if (this.map[bestY, bestX].Creature == null)
                            creature.interactWithField(this.map[bestY, bestX]);

					}
				}
			}
			else
				throw new CreatureException();

			return command;
		}
	}
}
