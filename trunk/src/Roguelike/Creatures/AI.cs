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
		public bool Sniper { get; set; }

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

			Random r = new Random();
			if(r.Next(2) == 0)
				Sniper = true;
			else
				Sniper = false;

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
						creature.SawPlayer = true;

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
							if(map.IsWithinBounds(creature.Y + j, creature.X + i))
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
						}


					// jeżeli przeciwnik jest ranny (prawie umiera), to ucieka. Chyba że nie ma gdzie uciec, wtedy strzela
					if(creature.PanicModeCounter > 0)
					{
						creature.PanicModeCounter--;
						int currentDistance = map.getEuclideanDistanceBetweenFields(player.Field, map[creature.Y, creature.X]);
						int furtherDistance = map.getEuclideanDistanceBetweenFields(player.Field, map[creature.Y + furtherPlayerDirY, creature.X + furtherPlayerDirX]);
						if(furtherDistance > currentDistance)
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
					else if (minDistance > creature.SightRange || creature.SawPlayer == false)
					{
						return RandomMove();
					}
					// przeciwnik widzi (widział gracza) to idzie w jego stronę lub strzela jak lubi
					else
					{
						if (Sniper == false || ShootCommand.CanShoot(map, creature, player) == false
							|| (Sniper == true && creature.RangedWeapon.Ammo == 0 ))
						{
							return new MoveCommand(creature, nearPlayerDirX, nearPlayerDirY, map, false);
						}
						else
						{
							return new ShootCommand(creature, player, map, new RandomNumberGenerator());
						}
					}
				}
			}
			else
				throw new CreatureException();
			return null;
		}

		private ICreatureCommand RandomMove()
		{
			Random r = RandomNumberGenerator.GlobalRandom;
			return new MoveCommand(creature, r.Next(3) - 1, r.Next(3) - 1, map, false);
		}
	}
}
