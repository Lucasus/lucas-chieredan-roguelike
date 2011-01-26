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

namespace RoguelikeGUI
{
	/// <summary>
	/// Interaction logic for MainMenu.xaml
	/// </summary>
	public partial class MainMenuView : UserControl
	{
		public event EventHandler startGamePressed;
		public event EventHandler hallOfFamePressed;

		public MainMenuView()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			if(startGamePressed != null)
				startGamePressed(this, null);
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			if(hallOfFamePressed != null)
				hallOfFamePressed(this, null);
		}
	}
}
