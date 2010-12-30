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

		private char[,] initialMap;/* = {
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
                       };*/

		public GameManager(MapWindow view)
		{
			this.window = view;
			this.keyProcessor = new MainKeyProcessor(this);
			MapLoader loader = new MapLoader();
			this.initialMap = loader.loadMap("TestMap.map");
			this.gameService.InitializeGame(initialMap);
			this.mapDrawer = new MapDrawer(window.GridMap, 15, 20, this.gameService.Map);

			UpdateScreenMap();
			UpdateGui();
		}

		private void UpdateGui()
		{
			window.PlayerHp.Content = gameService.Player.Creature.Health;
			window.playerMoney.Content = gameService.Player.Creature.Money;
			window.rangedDamage.Content = gameService.Player.Creature.RangedWeapon.Damage;
			window.rangedRange.Content = gameService.Player.Creature.RangedWeapon.Range;
			window.rangedChance.Content = (Math.Round(gameService.Player.Creature.RangedWeapon.Chance * 100)).ToString() + "%";
			window.MeleeDamage.Content = gameService.Player.Creature.MeleeWeapon.Damage;
			window.CurrentInputProcessor.Content = this.keyProcessor.ToString();
		}

		private void UpdateScreenMap()
		{
			Map map = gameService.Map;
			mapDrawer.Draw();
		}

		public void processKey(Key key)
		{
			if(!this.gameService.gameEnded())
			{
				this.keyProcessor.processKey(key);
				this.UpdateGui();
			}
		}

		public void PlayerCommand(IPlayerCommand playerCommand)
		{
			gameService.NextTurn(playerCommand);
			UpdateScreenMap();
			this.KeyProcessor = new MainKeyProcessor(this);
		}
	}
}
