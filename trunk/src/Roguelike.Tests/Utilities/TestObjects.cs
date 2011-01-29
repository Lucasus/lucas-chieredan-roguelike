using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike.Tests.Utilities
{
	public class TestObjects
	{
		public static Map GetTestMap()
		{

			char[,] mapView = {
						  {'.','.','#'},
						  {'.','#','.'},
						  {'#','#','#'}
					   };

			return new Map(mapView);
		}

		public static char[,] mapConfig = 
		{
			{'.','.','.','.','.', '.'},
			{'.','.','.','.','.', '.'},
			{'.','#','#','#','.', '.'},
			{'.','.','.','#','.', '.'},
			{'.','.','.','#','.', '.'}
		};

		public static Creature GetSimpleCreature(Map map, Creature player)
		{
			Creature creature = new Creature(10);
			AI ai = new AI(map, player, creature);
			ai.Sniper = false;
			creature.AI = ai;
			return creature;
		}
	}
}
