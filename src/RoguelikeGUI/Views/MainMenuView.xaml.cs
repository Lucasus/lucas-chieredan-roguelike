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

namespace RoguelikeGUI
{
	/// <summary>
	/// Interaction logic for MainMenu.xaml
	/// </summary>
	public partial class MainMenuView : UserControl
	{
		public GlobalGameModel gameModel;

		public delegate void StartGameEventHandler(object sender, StartGameEventArgs e);

		public event StartGameEventHandler startGamePressed;
		public event EventHandler hallOfFamePressed;

		public MainMenuView(GlobalGameModel gameModel)
		{
			InitializeComponent();
			this.gameModel = gameModel;
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			GameService gameService = new GameService();
			gameService.InitializeGame(Convert.ToInt32(this.mapWidth.Text), Convert.ToInt32(this.mapHeight.Text), this.playerName.Text);

			if(startGamePressed != null)
				startGamePressed(this, new StartGameEventArgs(gameService));
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			if(hallOfFamePressed != null)
				hallOfFamePressed(this, null);
		}
	}
}
