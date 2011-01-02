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
		private Map mapModel;
		public GuiMapField[,] fields;
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

        public MapDrawer(Grid grid, int rows, int columns, Map map)
        {
			this.mapModel = map;
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

					grid.Children.Add(field);
				}
			}
        }

		public void Draw(Field centerField)
		{
			for (int i = 0; i < fields.GetLength(0); ++i)
			{
				for (int j = 0; j < fields.GetLength(1); ++j)
				{
					if (centerField != null)
						fields[i, j].RefreshField(mapModel.isSightBetweenFields(centerField, fields[i, j].Field));
					else
						fields[i, j].RefreshField(true);
				}
			}
		}
    }
}
