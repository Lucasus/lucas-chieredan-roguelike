using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using Roguelike;

namespace RoguelikeGUI
{
	public class MainKeyProcessor : IKeyProcessor
	{
		GameManager manager;

		public MainKeyProcessor(GameManager manager)
		{
			this.manager = manager;
		}

		public void processKey(Key key)
		{
			switch (key)
			{
				case Key.NumPad1:// Key.B: // 
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.LeftDown, manager.GameService.Map));
					break;
				case Key.NumPad2: // Key.N://
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.Down, manager.GameService.Map));
					break;
				case Key.NumPad3: //Key.M://
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.RightDown, manager.GameService.Map));
					break;
				case Key.NumPad4: // Key.G://
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.Left, manager.GameService.Map));
					break;
				case Key.NumPad6: // Key.J://
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.Right, manager.GameService.Map));
					break;
				case Key.NumPad7: // Key.T://
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.LeftUp, manager.GameService.Map));
					break;
				case Key.NumPad8: // Key.Y://
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.Up, manager.GameService.Map));
					break;
				case Key.NumPad9: // Key.U://
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.RightUp, manager.GameService.Map));
					break;
				case Key.NumPad5: // Key.H://
					this.manager.PlayerCommand(new DoNothingCommand(manager.GameService.Player));
					break;
				case Key.P:
					this.manager.PlayerCommand(new PickupCommand(manager.GameService.Player));
					break;
				case Key.A:
					this.manager.KeyProcessor = new TargetSelectKeyProcessor(this.manager);
					break;
			}
		}
	}
}
