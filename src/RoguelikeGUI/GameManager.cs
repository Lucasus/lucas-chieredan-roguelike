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
		private Grid gridMap;
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

		public GameManager(Grid gridMap)
		{
			this.gridMap = gridMap;
			gameService.InitializeGame(initialMap);
			UpdateScreenMap();
		}

		private void UpdateScreenMap()
		{
			Map map = gameService.Map;
			IList<Image>[,] images = new FieldToImageConverter().ConvertFieldArray(map.GetSubmap(0, initialMap.GetLength(0) - 1, 0, initialMap.GetLength(1) - 1));
			MapDrawer mapDrawer = new MapDrawer(gridMap, images.GetLength(0), images.GetLength(1));
			mapDrawer.Draw(images);
		}

		public void PlayerCommand(Key key)
		{
			Player.Direction playerCommand = new Player.Direction();
			switch(key)
			{
				case Key.NumPad1:
					playerCommand = Player.Direction.LeftDown;
					break;
				case Key.NumPad2:
					playerCommand = Player.Direction.Down;
					break;
				case Key.NumPad3:
					playerCommand = Player.Direction.RightDown;
					break;
				case Key.NumPad4:
					playerCommand = Player.Direction.Left;
					break;
				case Key.NumPad6:
					playerCommand = Player.Direction.Right;
					break;
				case Key.NumPad7:
					playerCommand = Player.Direction.LeftUp;
					break;
				case Key.NumPad8:
					playerCommand = Player.Direction.Up;
					break;
				case Key.NumPad9:
					playerCommand = Player.Direction.RightUp;
					break;
			}

			gameService.PlayerCommand(playerCommand);
			UpdateScreenMap();
		}
	}
}
