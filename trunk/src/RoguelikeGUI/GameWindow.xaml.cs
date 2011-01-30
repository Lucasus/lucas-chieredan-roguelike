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
using System.Windows.Shapes;

using Roguelike;

namespace RoguelikeGUI
{
	/// <summary>
	/// Interaction logic for GameWindow.xaml
	/// </summary>
	public partial class GameWindow : Window
	{
		GlobalGameModel gameModel;

		MainMenuView mainMenu;
		HallOfFameView hallOfFame;
		public GameWindow()
		{
			InitializeComponent();

			gameModel = new GlobalGameModel();

			mainMenu = new MainMenuView(gameModel);
			mainMenu.startGamePressed += this.switchToGameView;
			mainMenu.hallOfFamePressed += this.switchToHallOfFame;
			hallOfFame = new HallOfFameView(gameModel);
			hallOfFame.backPressed += this.switchToMenu;
			this.switchView(mainMenu);
		}

		private void switchView(UserControl newControll)
		{
			this.Content = newControll;
			newControll.Loaded += this.onControllLoaded;	
		}
		private void onControllLoaded(object sender, EventArgs e)
		{
			Keyboard.Focus((UserControl)sender);
		}
		private void switchToGameView(object sender, StartGameEventArgs e)
		{
			MapView map = new MapView(gameModel, e.gameService);
			map.gameEndedEvent += this.switchToHallOfFame;
			this.switchView(map);
		}
		private void switchToHallOfFame(object sender, EventArgs e)
		{
			this.switchView(hallOfFame);
		}
		private void switchToMenu(object sender, EventArgs e)
		{
			this.switchView(mainMenu);
		}
	}
}
