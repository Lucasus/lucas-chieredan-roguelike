using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    public class GameService
    {
		public Map Map { get; set; }
		public PlayerLocation PlayerLocation { get; set; }
		public Player Player { get; set; }

		public void InitializeGame(char[,] initialMap)
		{
			PlayerLocation = new PlayerLocation() { X = 2, Y = 2 };
			Map = new Map(initialMap);
			Player = new Player(Map);
			Map[PlayerLocation.X, PlayerLocation.Y].putCreature(Player.Creature);
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
