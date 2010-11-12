using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Roguelike;
using RoguelikeGUI.Utilities;

namespace RoguelikeGUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        public MapWindow()
        {
            InitializeComponent();

            char[,] mapView = {
                          {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                          {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','.','.','#'},
                          {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','.','.','#'},
                          {'#','.','.','.','.','.','#','#','#','#','#','#','#','#','#','.','#','#','.','#','#','#'},
                          {'#','.','.','.','.','.','#','.','#','.','.','#','.','.','#','.','.','.','.','.','.','#'},
                          {'#','.','.','.','.','.','.','.','#','#','.','#','.','.','#','.','.','.','.','.','.','#'},
                          {'#','.','.','.','.','.','#','.','.','.','.','.','.','.','#','.','.','.','.','.','.','#'},
                          {'#','.','.','.','.','.','#','#','#','#','#','#','#','#','#','.','.','.','.','.','.','#'},
                          {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                          {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                          {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
                       };

            Map map = new Map(mapView);
            Image[,] images = new FieldToImageConverter().ConvertFieldArray(map.GetSubmap(0, mapView.GetLength(0)-1, 0, mapView.GetLength(1)-1));
            MapDrawer mapDrawer = new MapDrawer(GridMap, images.GetLength(0), images.GetLength(1));
            mapDrawer.Draw(images);

        }
    }
}
