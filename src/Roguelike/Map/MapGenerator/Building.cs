using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class Point
	{
		public int X { get; set; }
		public int Y { get; set; }
	}

	public class Building
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int SizeX { get; set; }
		public int SizeY { get; set; }

		public Point Doors { get; set; }
		private static Random r = RandomNumberGenerator.GlobalRandom;

		public static Building NewRandomBuilding(int minX, int minY, int maxX, int maxY)
		{
			Building b = new Building();

			// wyznaczamy rozmiary budynku
			if (r.Next(10) > 1)
			{
				b.SizeX = 3 + r.Next(Math.Min(8, maxX - 4));
				b.SizeY = 3 + r.Next(Math.Min(8, maxY - 4));
			}
			else
			{
				if (r.Next(2) == 0)
				{
					b.SizeX = 1;
					b.SizeY = 4 + r.Next(Math.Min(7, maxX - 5));
				}
				else
				{
					b.SizeX = 4 + r.Next(Math.Min(7, maxY - 5));
					b.SizeY = 1;

				}

			}

			b.X = minX + r.Next(maxX + 1 - b.SizeX);
			b.Y = minY + r.Next(maxY + 1 - b.SizeY);

			// losujemy czy drzwi będą na ścianach lewa/prawa czy też dolna/górna
			Point drzwi = new Point();
			if (r.Next(2) == 0) // lewa/prawa
			{
				int shift = 1 + r.Next(Math.Max(1, b.SizeY - 2));
				int x = 0;
				if (b.X < 2) // budynek przy lewym obrzeżu planszy
					x = b.X + b.SizeX - 1;
				else if (b.X + b.SizeX >= maxX -1)
					x = b.X;
				else
					x = r.Next(2) == 0 ? b.X : b.X + b.SizeX - 1;

				drzwi.X = x;
				drzwi.Y = b.Y + shift;
			}
			else
			{
				int shift = 1 + r.Next(Math.Max(1, b.SizeX - 2));
				int y = 0;
				if (b.Y < 2) // budynek przy lewym obrzeżu planszy
					y = b.Y + b.SizeY - 1;
				else if (b.Y + b.SizeY >= maxY -1)
					y = b.Y;
				else
					y = r.Next(2) == 0 ? b.Y : b.Y + b.SizeY - 1;

				drzwi.X = b.X + shift;
				drzwi.Y = y;
			}
			b.Doors = drzwi;

			return b;
		}

		public void DrawOnTemplate(char[,] m)
		{
			for (int i = 0; i < SizeX; ++i)
			{
				m[X + i, Y] = '#';
				m[X + i, Y + SizeY - 1] = '#';
			}

			for (int i = 0; i < SizeY; ++i)
			{
				m[X, Y + i] = '#';
				m[X + SizeX - 1, Y + i] = '#';
			}

			DrawDoors(m);
		}

		public void DrawDoors(char[,] m)
		{
			if(SizeX > 2 && SizeY > 2)
				m[Doors.X, Doors.Y] = '.';
		}

		public void GenerateLoot(Map m)
		{
			if (SizeX > 2 && SizeY > 2)
			{
				int lootCount = r.Next(3);
				for (int i = 0; i < lootCount; ++i)
				{
					int x = X + 1 + r.Next(SizeX - 2);
					int y = Y + 1 + r.Next(SizeY - 2);
					new LootGenerator().generateLoot(m[x, y]);
				}
			}
		}

		public bool GeneratePoints(Map m)
		{
			if (SizeX > 2 && SizeY > 2)
			{
				int x = X + 1 + r.Next(SizeX - 2);
				int y = Y + 1 + r.Next(SizeY - 2);
				m[x,y].placeObject(new Points() { Value = 10 });
				return true;
			}
			return false;
		}


		public List<Creature> GenerateCreatures(Map map, Creature player)
		{
			List<Creature> cList = new List<Creature>();
			if (SizeX > 2 && SizeY > 2)
			{
				int creatureCount = Math.Min((SizeX - 2) * (SizeY - 2), 1 + r.Next(4));
				for (int i = 0; i < creatureCount; ++i)
				{
					Creature enemy = new CreatureGenerator().GetRandomCreature();

					bool success = false;
					while (success == false)
					{
						success = map[X + 1 + r.Next(SizeX - 2), Y + 1 + r.Next(SizeY - 2)].putCreature(enemy);
					}

					AI ai = new AI(map, player, enemy);
					enemy.AI = ai;
					cList.Add(enemy);
				}
			}
			return cList;
		}

		public bool intersects(Building building)
		{
			Building left = this.X < building.X ? this : building;
			Building right = this.X >= building.X ? this : building;

			Building top = this.Y < building.Y ? this : building;
			Building down = this.Y >= building.Y ? this : building;

			if (left.X + left.SizeX >= right.X && top.Y + top.SizeY  >= down.Y)
				return true;

			return false;
		}

	}
}
