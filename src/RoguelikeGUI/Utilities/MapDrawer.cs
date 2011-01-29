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
		private int mapPartX;
		private int mapPartY;
		private Map mapModel;
		public GuiMapField[,] fields;
		public int RowCount { get; set; }
		public int ColumnCount { get; set; }

		public GuiMapField this[Field field]
		{
			get {
				foreach(GuiMapField guiField in fields)
				{
					if(guiField != null && guiField.Field == field)
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

			for (int i = 0; i < RowCount; ++i)
			{
				for (int j = 0; j < ColumnCount; ++j)
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
			if (centerField != null)
			{
				int posX = (centerField.X - ColumnCount/4)/ (ColumnCount/2);
				int posY = (centerField.Y - RowCount/4)/ (RowCount/2);

				if (this.mapPartX != posX || this.mapPartY != posY)
				{
					this.mapPartX = posX;
					this.mapPartY = posY;
					updateMap(posX * (ColumnCount/2), posY * (RowCount/2));
				}
			}

			for (int i = 0; i < fields.GetLength(0); ++i)
			{
				for (int j = 0; j < fields.GetLength(1); ++j)
				{
					if (fields[i, j] != null)
					{
						if (centerField != null && fields[i,j].Field != null)
							fields[i, j].RefreshField(mapModel.isSightBetweenFields(centerField, fields[i, j].Field));
						else
							fields[i, j].RefreshField(true);
					}
				}
			}
		}

		public void updateMap(int firstY, int firstX)
		{
			for (int i = 0; i < RowCount; ++i)
			{
				for (int j = 0; j < ColumnCount; ++j)
				{
					if (firstX + i < mapModel.MapWidth && firstY + j < mapModel.MapHeight)
						fields[i, j].Field = mapModel[firstX + i, firstY + j];
					else
						fields[i, j].Field = null;
				}
			}
		}
	}
}
