using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class GameService
	{
		public string playerName;

		public Creature Player { get; set; }
		public MapGenerator Generator { get; set; }
		
		private int turnCounter = 0;

		/// <summary>
		///  lista przeciwników obecnych na mapie
		/// </summary>
		public List<Creature> Creatures { get; set; }
	
		public Map Map { get; set; }

		public void InitializeGame(int mapSizeX, int mapSizeY, string playerName)//char[,] initialMap)
		{
			this.playerName = playerName;
			Random r = RandomNumberGenerator.GlobalRandom;
			Generator = new MapGenerator(mapSizeX, mapSizeY);
			Creatures = new List<Creature>();
			Player = new Creature(40)
			{
				CreatureType = "Hero", 
				MeleeWeapon = new Weapon(){Damage=3, BrokeChance=0.001}, 
				RangedWeapon = new RangedWeapon(){Damage=2, Range=3, Chance=0.5, Ammo=15}, 
				GrenadeWeapon = new GrenadeWeapon{Damage=5, Range=5, Spread=2, Ammo=2}
			};
			Player.MianownikName = "Gracz";
			Player.BiernikName = "gracza";
			Map = Generator.GenerateMap(Player);// new Map(initialMap);
			CreatureVisitor.map = Map;
			Creatures.AddRange(Generator.GeneratedCreatures);

			bool playerPlaced = false;
			while (playerPlaced == false)
			{
				playerPlaced = Map[r.Next(Map.MapWidth), r.Next(Map.MapHeight)].putCreature(Player);
			}
		}

		public void NextTurn(ICreatureCommand playerCommand)
		{
			Random r = RandomNumberGenerator.GlobalRandom;
			if (!gameEnded())
			{
				if (playerCommand.isExecutable())
				{
					//AbstractLogger.Current.Clear();
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

					if(gameEnded())
					{
						if (Player.isDead == true)
							AbstractLogger.Current.Log("Przegrałeś. Twój wynik to " + Player.Money + " punktów.");
						else if (Player.PickedPointsCount == Generator.PointObjectsCount)
						{
							Player.Money += 10;
							Player.Money += Player.Health/2;
							AbstractLogger.Current.Log("Dostajesz bonus " + Player.Health/2 + " punktów za zachowane zdrowie.");
							AbstractLogger.Current.Log("Dostajesz bonus " + (Generator.SizeX*Generator.SizeY)/10 + " punktów za wielkość mapy.");
							AbstractLogger.Current.Log("Wygrałeś!. Twój wynik to " + Player.Money + " punktów.");
						}
					}

					if (turnCounter % 8 == 0)
					{
						Creatures.Add(Generator.GenerateRandomCreature(Map, Player));
					}
				}
			}
		}

		public int getPoints()
		{
			return Player.Money;
		}

		public bool gameEnded()
		{
			return Player.isDead || Player.PickedPointsCount == Generator.PointObjectsCount;
		}
	}
}
 