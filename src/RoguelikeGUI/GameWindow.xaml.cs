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

namespace RoguelikeGUI
{
	/// <summary>
	/// Interaction logic for GameWindow.xaml
	/// </summary>
	public partial class GameWindow : Window
	{
		MainMenuView mainMenu;
		public GameWindow()
		{
			InitializeComponent();
			mainMenu = new MainMenuView();
			mainMenu.startGamePressed += this.onStartGame;
			mainMenu.hallOfFamePressed += this.onHallOfFame;
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
		private void onStartGame(object sender, EventArgs e)
		{
			this.switchView(new MapView());
		}
		private void onHallOfFame(object sender, EventArgs e)
		{
			this.switchView(new HallOfFameView());
		}
	}
}
