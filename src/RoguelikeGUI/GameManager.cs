using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

using Roguelike;
using RoguelikeGUI.Utilities;

namespace RoguelikeGUI
{
	public class GameManager
	{
		private MapWindow window;
		private GameService gameService = new GameService();
		private IKeyProcessor keyProcessor;
		private MapDrawer mapDrawer;

		public MapDrawer MapDrawer
		{
			get { return mapDrawer; }
		}

		public GameService GameService
		{
			get { return gameService; }
		}

		public IKeyProcessor KeyProcessor
		{
			set { keyProcessor = value; }
		}

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
			this.keyProcessor = new MainKeyProcessor(this);
			this.gameService.InitializeGame(initialMap);
			this.mapDrawer = new MapDrawer(window.GridMap, 15, 20, this.gameService.Map);

			UpdateScreenMap();
			UpdateGui();
		}

		private void UpdateGui()
		{
			window.PlayerHp.Content = gameService.Player.Creature.Health;
			window.playerMoney.Content = gameService.Player.Creature.Money;
			window.playerDamage.Content = gameService.Player.Creature.Weapon.Damage;
			window.playerRange.Content = gameService.Player.Creature.Weapon.Range;
			window.CurrentInputProcessor.Content = this.keyProcessor.ToString();
		}

		private void UpdateScreenMap()
		{
			Map map = gameService.Map;
			mapDrawer.Draw();
		}

		public void processKey(Key key)
		{
			this.keyProcessor.processKey(key);
			this.UpdateGui();
		}

		public void PlayerCommand(IPlayerCommand playerCommand)
		{
			gameService.NextTurn(playerCommand);
			UpdateScreenMap();
			this.KeyProcessor = new MainKeyProcessor(this);
		}
	}
}
