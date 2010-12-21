using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;

using Roguelike;

namespace RoguelikeGUI
{
    public class MapDrawer
    {
		public GuiMapField[,] fields;
        public Grid Grid { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }

		public GuiMapField this[Field field]
		{
			get {
				foreach(GuiMapField guiField in fields)
				{
					if(guiField.Field == field)
						return guiField;
				}
				return null;
			}	
		}

        public MapDrawer(Grid _grid, int rows, int columns, Map map)
        {
            this.Grid = _grid;
            this.RowCount = rows; 
            this.ColumnCount = columns;
			fields = new GuiMapField[rows, columns];

			for (int i = 0; i < rows; ++i)
			{
				for (int j = 0; j < columns; ++j)
				{
					GuiMapField field = new GuiMapField(map[i, j]);
					fields[i,j] = field;

					field.SetValue(Grid.RowProperty, i);
					field.SetValue(Grid.ColumnProperty, j);

					Grid.Children.Add(field);
				}
			}
        }

		public void Draw()
		{
			for (int i = 0; i < fields.GetLength(0); ++i)
			{
				for (int j = 0; j < fields.GetLength(1); ++j)
				{
					fields[i,j].RefreshField();
				}
			}
		}

        /*public void Draw(IList<Image>[,] images)
        {
            if(images.GetLength(0) != RowCount || images.GetLength(1) != ColumnCount)
            {
                throw new IndexOutOfRangeException("Ilości wierszy i kolumn talicy z obrazkami nie są takie same jak ilości wierszy i kolumn MapDrawer'a");
            }

			Grid.Children.Clear();
            for(int i = 0 ; i < images.GetLength(0) ; ++i)
            {
                for (int j = 0; j < images.GetLength(1); ++j)
                {
					foreach (Image img in images[i, j])
					{
						img.SetValue(Grid.RowProperty, i);
						img.SetValue(Grid.ColumnProperty, j);

						Grid.Children.Add(img);
					}
				}
            }
        }*/
    }
}
