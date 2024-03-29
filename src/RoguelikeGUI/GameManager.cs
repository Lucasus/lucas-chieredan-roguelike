﻿using System;
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
		private MapView window;
		private GameService gameService;
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

		public GameManager(MapView mapView, GameService service, ListBoxLogger listBoxLogger)
		{
			this.gameService = service;
			this.window = mapView;
			this.keyProcessor = new MainKeyProcessor(this);
			//MapLoader loader = new MapLoader();
			//char[,] initialMap = loader.loadMap("TestMap.map");
			AbstractLogger.Current = listBoxLogger;
			//this.gameService.InitializeGame(initialMap);
			this.mapDrawer = new MapDrawer(window.GridMap, 15, 20, this.gameService.Map);

			UpdateScreenMap();
			UpdateGui();
		}

		private void UpdateGui()
		{
			window.playerMoney.Content = gameService.Player.Money;
			window.PlayerHp.Content = gameService.Player.Health;
			window.playerMoney.Content = gameService.Player.Money + " (" + gameService.Player.PickedPointsCount + "/" + gameService.Generator.PointObjectsCount + ")";
			window.rangedDamage.Content = gameService.Player.RangedWeapon.Damage;
			window.rangedRange.Content = gameService.Player.RangedWeapon.Range;
			window.rangedAmmo.Content = gameService.Player.RangedWeapon.Ammo;
			window.MeleeBrokeChance.Content = (Math.Round(gameService.Player.MeleeWeapon.BrokeChance * 100)).ToString() + "%";
			window.rangedChance.Content = (Math.Round(gameService.Player.RangedWeapon.Chance * 100)).ToString() + "%";
			window.MeleeDamage.Content = gameService.Player.MeleeWeapon.Damage;
			window.CurrentInputProcessor.Content = this.keyProcessor.getProcessorComment();
			if(gameService.Player.GrenadeWeapon != null)
			{
				window.grenadeDamage.Content = gameService.Player.GrenadeWeapon.Damage;
				window.grenadeSpread.Content = gameService.Player.GrenadeWeapon.Spread;
				window.grenadeCount.Content = gameService.Player.GrenadeWeapon.Ammo;
				window.grenadeRange.Content = gameService.Player.GrenadeWeapon.Range;
			}
		}

		private void UpdateScreenMap()
		{
			Map map = gameService.Map;
			mapDrawer.Draw(this.gameService.Player.Field);
		}

		public void processKey(Key key)
		{
			if(!this.gameService.gameEnded())
			{
				this.keyProcessor.processKey(key);
				this.UpdateGui();
			}
			else
			{
				window.gameEnded();
			}
		}

		public void PlayerCommand(ICreatureCommand playerCommand)
		{
			gameService.NextTurn(playerCommand);
			UpdateScreenMap();
			this.KeyProcessor = new MainKeyProcessor(this);
		}
	}
}
