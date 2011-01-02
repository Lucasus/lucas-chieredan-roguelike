using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class Map
    {
        private Field[,] fields;

		public int MapWidth
		{
			get {return fields.GetLength(0);}
		}

		public int MapHeight
		{
			get {return fields.GetLength(1);}
		}

        public Map(char[,] mapString)
        {
            fields = new Field[mapString.GetLength(0), mapString.GetLength(1)];
            for (int i = 0; i < mapString.GetLength(0); ++i)
            {
                for (int j = 0; j < mapString.GetLength(1); ++j)
                {
                    switch (mapString[i, j])
                    {
                        case '.':
                            fields[i, j] = new Floor(j,i);
                            break;
                        case '#':
							fields[i, j] = new Wall(j, i);
                            break;
                    }
                }
            }
        }

        public Field this[int index1, int index2]
        {
            get
            {
				if(index1 < 0 || index1 >= fields.GetLength(0) || index2 < 0 || index2 >= fields.GetLength(1))
					throw new MapOutOfBoundException();
                return fields[index1,index2];
            }
        }

		public double getDistanceBetweenFields(Field sourceField, Field destField)
		{
			return Math.Max(Math.Abs(sourceField.X - destField.X), Math.Abs(sourceField.Y - destField.Y));
		}

		public bool isSightBetweenFields(Field sourceField, Field destField)
		{
			double distance = getDistanceBetweenFields(sourceField, destField);
			if(distance > 0)
			{
				double stepX = (destField.X - sourceField.X) / distance;
				double stepY = (destField.Y - sourceField.Y) / distance;
				double currentX = sourceField.X + 0.5;
				double currentY = sourceField.Y + 0.5;

				for(int i=0; i<=distance; i++)
				{
					if(Math.Abs(currentX - (int)currentX) > 0.95)
					{
						if (this[(int)currentY, (int)currentX].blocksSight() || this[(int)currentY, (int)currentX + 1].blocksSight())
							return false;
					}
					else if( Math.Abs(currentX - (int)currentX) < 0.05)
					{
						if (this[(int)currentY, (int)currentX].blocksSight() || this[(int)currentY, (int)currentX - 1].blocksSight())
							return false;
					}
					else if(Math.Abs(currentY - (int)currentY) > 0.95)
					{
						if (this[(int)currentY, (int)currentX].blocksSight() || this[(int)currentY + 1, (int)currentX].blocksSight())
							return false;
					}
					else if(Math.Abs(currentY - (int)currentY) < 0.05)
					{
						if (this[(int)currentY, (int)currentX].blocksSight() || this[(int)currentY - 1, (int)currentX].blocksSight())
							return false;
					}
					else
					{
						if (this[(int)currentY, (int)currentX].blocksSight())
							return false;
					}
					currentX += stepX;
					currentY += stepY;
				}
			}
			
			return true;
		}
    }
}
