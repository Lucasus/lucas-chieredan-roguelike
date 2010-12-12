using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roguelike;
using System.Windows.Controls;
using RoguelikeGUI.Utilities;
using System.Windows.Input;

namespace RoguelikeGUI
{
	public class GameManager
	{
		private MapWindow window;
		private GameService gameService = new GameService();
		private char[,] initialMap = {
                          {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                          {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','.','#'},
                          {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','.','#'},
                          {'#','.','.','.','.','#','#','#','#','#','#','#','#','#','.','#','#','.','#','#'},
                          {'#','.','.','.','.','#','.','#','.','.','#','.','.','#','.','.','.','.','.','#'},
                          {'#','.','.','.','.','.','.','#','#','.','#','.','.','#','.','.','.','.','.','#'},
                          {'#','.','.','.','.','#','.','.','.','.','.','.','.','#','.','.','.','.','.','#'},
                          {'#','.','.','.','.','#','#','#','#','#','#','#','#','#','.','.','.','.','.','#'},
                          {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                          {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
						  {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#','#','#','#','#'},
						  {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
						  {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','.','#'},
                          {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','.','#'},
                          {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
                       };

		public GameManager(MapWindow view)
		{
			this.window = view;
			gameService.InitializeGame(initialMap);
			UpdateScreenMap();
		}

		private void UpdateScreenMap()
		{
			Map map = gameService.Map;
			IList<Image>[,] images = new FieldToImageConverter().ConvertFieldArray(map.GetSubmap(0, initialMap.GetLength(0) - 1, 0, initialMap.GetLength(1) - 1));
			MapDrawer mapDrawer = new MapDrawer(window.GridMap, images.GetLength(0), images.GetLength(1));
			mapDrawer.Draw(images);
			window.PlayerHp.Content = gameService.Player.Creature.Health;
			window.playerMoney.Content = gameService.Player.Creature.Money;
		}

		public void PlayerCommand(Key key)
		{
			IPlayerCommand playerCommand = null;
			switch(key)
			{
				case Key.NumPad1:
					playerCommand = new MoveCommand(MoveCommand.Direction.LeftDown);
					break;
				case Key.NumPad2:
					playerCommand = new MoveCommand(MoveCommand.Direction.Down);
					break;
				case Key.NumPad3:
					playerCommand = new MoveCommand(MoveCommand.Direction.RightDown);
					break;
				case Key.NumPad4:
					playerCommand = new MoveCommand(MoveCommand.Direction.Left);
					break;
				case Key.NumPad6:
					playerCommand = new MoveCommand(MoveCommand.Direction.Right);
					break;
				case Key.NumPad7:
					playerCommand = new MoveCommand(MoveCommand.Direction.LeftUp);
					break;
				case Key.NumPad8:
					playerCommand = new MoveCommand(MoveCommand.Direction.Up);
					break;
				case Key.NumPad9:
					playerCommand = new MoveCommand(MoveCommand.Direction.RightUp);
					break;
				default:
					playerCommand = new DoNothingCommand();
					break;
			}

			gameService.NextTurn(playerCommand);
			UpdateScreenMap();
		}
	}
}
