using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class GameService
    {
		private Player Player { get; set; }

		public Map Map { get; set; }

		public void InitializeGame(char[,] initialMap)
		{
			Map = new Map(initialMap);
			Creature playersCreature = new PlayerCreature(20);
			Map[2, 2].putCreature(playersCreature);
			Player = new Player(Map){Creature = playersCreature};
		}

		public void NextTurn(Player.Direction playerCommand)
		{
			Player.move(playerCommand);
		}

		public void PlayerCommand(Player.Direction playerCommand)
		{
			NextTurn(playerCommand); // TODO: nie zawsze to będzie następna tura, czasem wiele komend w jednej turze
		}
	}
}
