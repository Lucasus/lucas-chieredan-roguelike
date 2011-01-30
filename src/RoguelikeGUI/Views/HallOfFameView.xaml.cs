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
	public partial class HallOfFameView : UserControl
	{
		public GlobalGameModel gameModel;
		public event EventHandler backPressed;

		public HallOfFameView(GlobalGameModel gameModel)
		{
			InitializeComponent();
			this.gameModel = gameModel;
			this.gameModel.HallOfFame.recordAdded += this.refreshList;
			refreshList(this, new EventArgs());
		}

		private void refreshList(object sender, EventArgs e)
		{
			hallOfFameBlock.Text = "";
			IEnumerable<KeyValuePair<string, int>> highscores = this.gameModel.HallOfFame.getTopN(10);
			foreach(KeyValuePair<string, int> highscore in highscores)
			{
				hallOfFameBlock.Text += highscore.Key + " " + highscore.Value + "\n";
			}
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			if(backPressed != null)
				backPressed(this, null);
		}
	}
}
