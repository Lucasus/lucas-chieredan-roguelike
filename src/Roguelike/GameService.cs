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
		private MapGenerator generator;

		public void InitializeGame(char[,] initialMap)
		{
			Random r = RandomNumberGenerator.GlobalRandom;
			generator = new MapGenerator();
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
			Map = generator.GenerateMap(Player);// new Map(initialMap);
			CreatureVisitor.map = Map;
			Creatures.AddRange(generator.GeneratedCreatures);

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
						else if (Player.PicketPointsCount == generator.PointObjectsCount)
						{
							Player.Money += 10;
							Player.Money += Player.Health/2;
							AbstractLogger.Current.Log("Dostajesz bonus "+Player.Health/2+" punktów za zachowane zdrowie.");
							AbstractLogger.Current.Log("Dostajesz bonus 10 punktów za ukończenie gry.");
							AbstractLogger.Current.Log("Wygrałeś!. Twój wynik to " + Player.Money + " punktów.");
						}
					}

					if (turnCounter % 8 == 0)
					{
						Creatures.Add(generator.GenerateRandomCreature(Map, Player));
					}
				}
			}
		}

		public bool gameEnded()
		{
			return Player.isDead || Player.PicketPointsCount == generator.PointObjectsCount;
		}
	}
}
