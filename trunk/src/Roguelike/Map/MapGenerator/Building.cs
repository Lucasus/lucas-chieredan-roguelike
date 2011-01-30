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

		public static Building NewRandomBuilding(int minX, int minY, int maxX, int maxY)
		{
			Random r = RandomNumberGenerator.GlobalRandom;
			Building b = new Building();

			// wyznaczamy rozmiary budynku
			b.SizeX = 3 + r.Next(8);
			b.SizeY = 3 + r.Next(8);

			b.X = minX + r.Next(maxX + 1 - b.SizeX);
			b.Y = minY + r.Next(maxY + 1 - b.SizeY);

			// losujemy czy drzwi będą na ścianach lewa/prawa czy też dolna/górna
			Point drzwi = new Point();
			if (r.Next(2) == 0) // lewa/prawa
			{
				int shift = 1 + r.Next(b.SizeY - 2);
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
				int shift = 1 + r.Next(b.SizeX - 2);
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
			m[Doors.X, Doors.Y] = '.';
		}

		public bool intersects(Building building)
		{
			Building left = this.X < building.X ? this : building;
			Building right = this.X >= building.X ? this : building;

			Building top = this.Y < building.Y ? this : building;
			Building down = this.Y >= building.Y ? this : building;

			if (left.X + left.SizeX > right.X && top.Y + top.SizeY > down.Y)
				return true;

			return false;
		}
	}
}
