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
    public partial class MapView : UserControl
    {
		private GameManager gameManager;

        public MapView()
        {
            InitializeComponent();
			gameManager = new GameManager(this);
        }

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			gameManager.processKey(e.Key);
		}
    }
}
