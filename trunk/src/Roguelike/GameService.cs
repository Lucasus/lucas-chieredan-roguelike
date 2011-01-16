using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class GameService
    {
		public Creature Player { get; set; }
		public AI Ai { get; set; }

		public Map Map { get; set; }

		public void InitializeGame(char[,] initialMap)
		{
			Map = new Map(initialMap);
			CreatureVisitor.map = Map;
			Player = new Creature(20){CreatureType = "Hero", MeleeWeapon = new Weapon(){Damage=3}, RangedWeapon = new RangedWeapon(){Damage=2, Range=3, Chance=0.5}, GrenadeWeapon = new GrenadeWeapon{Damage=5, Range=5, Spread=2, Count=2}};
			Map[2, 2].putCreature(Player);

			Ai = new AI(Map, Player);

			Random randomNumberGenerator = new Random();
			for(int i=0; i<30;)
			{

				Creature enemy = new Creature(5 + randomNumberGenerator.Next(10)){CreatureType = "Enemy", MeleeWeapon = new Weapon(){Damage=1}};

				bool success = Map[randomNumberGenerator.Next(Map.MapWidth), randomNumberGenerator.Next(Map.MapHeight)].putCreature(enemy);
				if(success)
				{
					Ai.addCreature(enemy);
					i++;
				}
			}
		}

		public void NextTurn(ICreatureCommand playerCommand)
		{
			if(!gameEnded())
			{
				if(playerCommand.isExecutable())
				{
					playerCommand.execute();
					Ai.act();
				}
			}
		}

		public bool gameEnded()
		{
			return Player.isDead;
		}
	}
}
