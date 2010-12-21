using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class GameService
    {
		public Player Player { get; set; }
		public AI Ai { get; set; }

		public Map Map { get; set; }

		public void InitializeGame(char[,] initialMap)
		{
			Map = new Map(initialMap);
			Creature playersCreature = new Creature(20){CreatureType = "Hero", Weapon = new Weapon(){Damage=3, Range=3}};
			Map[2, 2].putCreature(playersCreature);
			Player = new Player(Map){Creature = playersCreature};

			Ai = new AI(Map, Player);

			Random randomNumberGenerator = new Random();
			for(int i=0; i<10;)
			{
				Creature enemy = new Creature(10){CreatureType = "Enemy", Weapon = new Weapon(){Damage=1,Range=2}};

				bool success = Map[randomNumberGenerator.Next(Map.MapWidth), randomNumberGenerator.Next(Map.MapHeight)].putCreature(enemy);
				if(success)
				{
					Ai.addCreature(enemy);
					i++;
				}
			}
		}

		public void NextTurn(IPlayerCommand playerCommand)
		{
			if(!gameEnded())
			{
				Player.executeCommand(playerCommand);
				Ai.act();
			}
		}

		public bool gameEnded()
		{
			return Player.Creature.isDead;
		}
	}
}
