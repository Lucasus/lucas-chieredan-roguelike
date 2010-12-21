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

        public Field[,] GetSubmap(int rowStart, int rowEnd, int columnStart, int columnEnd)
        {
            Field[,] subFields = new Field[rowEnd - rowStart + 1, columnEnd - columnStart + 1];
            for (int i = rowStart; i <= rowEnd; ++i)
            {
                for (int j = columnStart; j <= columnEnd; ++j)
                {
                    subFields[i - rowStart, j - columnStart] = fields[i, j];
                }
            }
            return subFields;
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
    }
}
