using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Roguelike;

namespace RoguelikeGUI
{
	public class StartGameEventArgs : EventArgs
	{
		public GameService gameService;

		public StartGameEventArgs(GameService gameService)
		{
			this.gameService = gameService;
		}
	}
}
