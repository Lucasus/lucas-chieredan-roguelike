using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class GameService
    {
		public Player Player { get; set; }
		private AI Ai;

		public Map Map { get; set; }

		public void InitializeGame(char[,] initialMap)
		{
			Map = new Map(initialMap);
			Creature playersCreature = new Creature(20){CreatureType = "Hero", Weapon = new Weapon(){Damage=5}};
			Map[2, 2].putCreature(playersCreature);
			Player = new Player(Map){Creature = playersCreature};

			Ai = new AI(Map, Player);

			Random randomNumberGenerator = new Random();
			for(int i=0; i<10; i++)
			{
				Creature enemy = new Creature(10){CreatureType = "Enemy"};
				
				bool success = Map[randomNumberGenerator.Next(10), randomNumberGenerator.Next(10)].putCreature(enemy);
				if(success)
					Ai.addCreature(enemy);
			}
		}

		public void NextTurn(IPlayerCommand playerCommand)
		{
			if(!gameEnded())
			{
				Player.executeCommand(playerCommand);
				//Player.move(playerCommand); 
				Ai.act();
			}
		}

		public bool gameEnded()
		{
			return Player.Creature.isDead;
		}
	}
}
