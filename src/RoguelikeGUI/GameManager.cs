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

		public GameManager(Grid gridMap)
		{
			this.gridMap = gridMap;
			gameService.InitializeGame(initialMap);
			UpdateScreenMap();
		}

		private void UpdateScreenMap()
		{
			Map map = gameService.Map;
			Image[,] images = new FieldToImageConverter().ConvertFieldArray(map.GetSubmap(0, initialMap.GetLength(0) - 1, 0, initialMap.GetLength(1) - 1));
			MapDrawer mapDrawer = new MapDrawer(gridMap, images.GetLength(0), images.GetLength(1));
			mapDrawer.Draw(images);
		}

		public void PlayerCommand(Key key)
		{
			//throw new NotImplementedException();
		}
	}
}
