using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class MapGenerator
	{
		public int SizeX { get; set; }
		public int SizeY { get; set; }
		public List<Creature> GeneratedCreatures = new List<Creature>();

		private char[,] mapTemplate;

		public MapGenerator()
		{
			SizeX = 100;
			SizeY = 100;
		}

		public Map GenerateMap(Creature player)
		{
			mapTemplate = new char[SizeX, SizeY];

			// na początku wszystko jest terenem po którym można chodzić + obwódka ze ścian
			for (int i = 0; i < SizeX; ++i)
				for (int j = 0; j < SizeY; ++j)
					if (i == 0 || j == 0 || i == SizeX - 1 || j == SizeY - 1)
						mapTemplate[i, j] = '#';
					else
						mapTemplate[i, j] = '.';

			List<Building> buildings = new List<Building>();
			for (int i = 0; i < 600; ++i)
			{
				bool success = false;
				int counter = 0;
				while (success == false && counter < 300)
				{
					Building b = Building.NewRandomBuilding(2, 2, SizeX - 2, SizeY - 2);
					if (!intersects(b, buildings))
					{
						success = true;
						buildings.Add(b);
					}
					++counter;
				}
			}

			foreach(Building b in buildings)
				b.DrawOnTemplate(mapTemplate);

			foreach (Building b in buildings)
				b.DrawDoors(mapTemplate);

			Map map = new Map(mapTemplate);

			foreach (Building b in buildings)
				b.GenerateLoot(map);

			foreach (Building b in buildings)
			{
				GeneratedCreatures.AddRange(b.GenerateCreatures(map, player));
			}

			return map;
		}

		public Creature GenerateRandomCreature(Map map, Creature player)
		{
			Random r = RandomNumberGenerator.GlobalRandom;
			Creature enemy = new CreatureGenerator().GetRandomCreature();

			bool success = false;

			while(success == false)
			{
				success = map[r.Next(map.MapWidth), r.Next(map.MapHeight)].putCreature(enemy);
			}
			AI ai = new AI(map, player, enemy);
			enemy.AI = ai;
			return enemy;

		}

		private bool intersects(Building b, List<Building> buildings)
		{
			foreach (Building building in buildings)
			{
				if (b.intersects(building))
					return true;
			}
			return false;
		}
	}
}
