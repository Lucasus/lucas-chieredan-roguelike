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
		public event EventHandler gameEndedEvent;
		private GameManager gameManager;

		public MapView(GameService service)
		{
			InitializeComponent();
			gameManager = new GameManager(this, service, new ListBoxLogger(Logger) );
		}

		public void gameEnded()
		{
			if(gameEndedEvent != null)
				gameEndedEvent(this, null);
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			gameManager.processKey(e.Key);
		}
	}
}
