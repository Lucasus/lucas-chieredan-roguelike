using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace RoguelikeGUI
{
    public class MapDrawer
    {
        public Grid Grid { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }


        public MapDrawer(Grid _grid, int rows, int columns)
        {
            this.Grid = _grid;
            this.RowCount = rows; 
            this.ColumnCount = columns;

            for (int i = 0; i < RowCount; ++i)
                Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
            for (int i = 0; i < ColumnCount; ++i)
                Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
        }

        public void Draw(Image[,] images)
        {
            if(images.GetLength(0) != RowCount || images.GetLength(1) != ColumnCount)
            {
                throw new IndexOutOfRangeException("Ilości wierszy i kolumn talicy z obrazkami nie są takie same jak ilości wierszy i kolumn MapDrawer'a");
            }

            for(int i = 0 ; i < images.GetLength(0) ; ++i)
            {
                for (int j = 0; j < images.GetLength(1); ++j)
                {
                    images[i,j].SetValue(Grid.RowProperty, i);
                    images[i,j].SetValue(Grid.ColumnProperty, j);
                    Grid.Children.Add(images[i,j]);
                }
            }
        }
    }
}
