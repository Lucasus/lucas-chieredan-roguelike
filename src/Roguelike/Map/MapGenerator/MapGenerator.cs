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

		private char[,] mapTemplate;

		public MapGenerator()
		{
			SizeX = 120;
			SizeY = 120;
		}

		public Map GenerateMap()
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
			for (int i = 0; i < 200;)
			{
				Building b = Building.NewRandomBuilding(0, 0, SizeX, SizeY);
				if (!intersects(b, buildings))
				{
					buildings.Add(b);
					++i;
				}
			}

			foreach(Building b in buildings)
				b.DrawOnTemplate(mapTemplate);

			foreach (Building b in buildings)
				b.DrawDoors(mapTemplate);


			return new Map(mapTemplate);
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
