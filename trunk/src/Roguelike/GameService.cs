using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class GameService
	{
		public Creature Player { get; set; }
		private int turnCounter = 0;

		/// <summary>
		///  lista przeciwników obecnych na mapie
		/// </summary>
		public List<Creature> Creatures { get; set; }
	
		public Map Map { get; set; }

		public void InitializeGame(char[,] initialMap)
		{
			Random r = RandomNumberGenerator.GlobalRandom;
			MapGenerator generator = new MapGenerator();
			Map = generator.GenerateMap();// new Map(initialMap);
			CreatureVisitor.map = Map;
			Creatures = new List<Creature>();
			Player = new Creature(40){CreatureType = "Hero", MeleeWeapon = new Weapon(){Damage=3}, RangedWeapon = new RangedWeapon(){Damage=2, Range=3, Chance=0.5}, GrenadeWeapon = new GrenadeWeapon{Damage=5, Range=5, Spread=2, Count=2}};
			Player.MianownikName = "Gracz";
			Player.BiernikName = "gracza";

			bool playerPlaced = false;
			while (playerPlaced == false)
			{
				playerPlaced = Map[r.Next(Map.MapWidth), r.Next(Map.MapHeight)].putCreature(Player);
			}


			//Random r = new Random();
			//for(int i=0; i<30;)
			//{
			//
			//}
		}

		public void NextTurn(ICreatureCommand playerCommand)
		{
			Random r = RandomNumberGenerator.GlobalRandom;
			if (!gameEnded())
			{
				if(playerCommand.isExecutable())
				{
					AbstractLogger.Current.Clear();
					playerCommand.execute();

					Creatures.RemoveAll(x => x.isDead || x.Field == null);

					foreach (Creature creature in Creatures)
					{
						ICreatureCommand creatureCommand = creature.AI.GenerateNextCommand();
						if (creatureCommand != null)
							creatureCommand.execute();
					}
					++turnCounter;
					AbstractLogger.Current.Log("Tura " + turnCounter);

					if (turnCounter % 5 == 0)
					{
						Creature enemy = new Creature(7 + r.Next(13))
						{
							CreatureType = "Enemy",
							MianownikName = "Gangster",
							BiernikName = "gangstera",
							MeleeWeapon = new Weapon() { Damage = 1 + r.Next(2) },
							RangedWeapon = new RangedWeapon() { Chance = (double)(10 + r.Next(80))/100 , Damage = 1, Range = 2 + r.Next(7) }
						};

						bool success = Map[r.Next(Map.MapWidth), r.Next(Map.MapHeight)].putCreature(enemy);
						if (success)
						{
							AI ai = new AI(Map, Player, enemy);
							enemy.AI = ai;
							Creatures.Add(enemy);
						}
					}


				}
			}
		}

		public bool gameEnded()
		{
			return Player.isDead;
		}
	}
}
