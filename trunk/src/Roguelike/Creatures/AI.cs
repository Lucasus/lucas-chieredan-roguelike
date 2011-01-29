using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	/// <summary>
	/// klasa odpowiedzialna za ruchy i sztuczną inteligencję komputerowych przeciwników
	/// </summary>
	public class AI
	{
		#region Private fields

		private Map map;
		private Creature player;
		private bool sawPlayer = false;

		/// <summary>
		/// referencja do stwora, którym będzie sztuczna inteligencja kierować
		/// </summary>
		private Creature creature;

		#endregion
		#region Constructor

		public AI(Map map, Creature player, Creature creature)
		{
			this.map = map;
			this.player = player;
			this.creature = creature;
		}

		#endregion

		/// <summary>
		/// Generuje komendę - co ma zrobić komputerowy przeciwnik
		/// </summary>
		public ICreatureCommand GenerateNextCommand()
		{
			if (creature.Field != null)
			{
				if (!player.isDead)
				{
					if (map.isSightBetweenFields(player.Field, creature.Field) == true)
						sawPlayer = true;

					int minDistance = int.MaxValue;
					int maxDistance = 0;
					int nearPlayerDirX = 0;
					int nearPlayerDirY = 0;
					int furtherPlayerDirX = 0;
					int furtherPlayerDirY = 0;

					// liczymy, na które pole przeciwnik może wejść aby być najbliżej/najdalej gracza
					for (int i = -1; i <= 1; ++i)
						for (int j = -1; j <= 1; ++j)
						{
							Field f = this.map[creature.Y + j, creature.X + i];
							if (f != null && creature.canInteractWithField(f) == true
								&& this.map[creature.Y + j, creature.X + i].Creature == null)
							{
								int distance = map.getEuclideanDistanceBetweenFields(player.Field, f);
								if (distance < minDistance)
								{
									minDistance = distance;
									nearPlayerDirX = i;
									nearPlayerDirY = j;
								}
								if (distance >= maxDistance)
								{
									maxDistance = distance;
									furtherPlayerDirX = i;
									furtherPlayerDirY = j;
								}
							}
						}


					// jeżeli przeciwnik jest ranny (prawie umiera), to ucieka. Chyba że nie ma gdzie uciec, wtedy strzela
					if(creature.PanicModeCounter > 0)
					{
						creature.PanicModeCounter--;
						if (map.getDistanceBetweenFields(player.Field, map[creature.Y + furtherPlayerDirY, creature.X + furtherPlayerDirX]) == 1)
						{
							return new AttackCommand(creature, player, map);
						}
						else
						{
							return new MoveCommand(creature, furtherPlayerDirX, furtherPlayerDirY, map, false);
						}
					}
					// jeżeli przeciwnik jest w miarę blisko - atakuje
					if (map.getDistanceBetweenFields(player.Field, creature.Field) == 1)
					{
						return new AttackCommand(creature, player, map);
					}
					// jeśli przeciwnik nie widział gracza - rusza się losowo
					else if (minDistance > 10 && sawPlayer == false)
					{
						return RandomMove();
					}
					// przeciwnik widzi (widział gracza) to idzie w jego stronę
					else
					{
						return new MoveCommand(creature, nearPlayerDirX, nearPlayerDirY, map, false);
					}
				}
			}
			else
				throw new CreatureException();
			return null;
		}

		private ICreatureCommand RandomMove()
		{
			Random r = new Random();
			return new MoveCommand(creature, r.Next(3) - 1, r.Next(3) - 1, map, false);
		}
	}
}
