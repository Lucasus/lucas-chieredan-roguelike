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
				case Key.B: 
				case Key.NumPad1:
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.LeftDown, manager.GameService.Map));
					break;
				case Key.N:
				case Key.NumPad2:
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.Down, manager.GameService.Map));
					break;
				case Key.M:
				case Key.NumPad3:
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.RightDown, manager.GameService.Map));
					break;
				case Key.G:
				case Key.NumPad4:
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.Left, manager.GameService.Map));
					break;
				case Key.J:
				case Key.NumPad6:
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.Right, manager.GameService.Map));
					break;
				case Key.T:
				case Key.NumPad7:
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.LeftUp, manager.GameService.Map));
					break;
				case Key.Y:
				case Key.NumPad8:
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.Up, manager.GameService.Map));
					break;
				case Key.U:
				case Key.NumPad9:
					this.manager.PlayerCommand(new MoveCommand(manager.GameService.Player, MoveCommand.Direction.RightUp, manager.GameService.Map));
					break;
				case Key.H:
				case Key.NumPad5:
					this.manager.PlayerCommand(new DoNothingCommand(manager.GameService.Player));
					break;
				case Key.P:
					this.manager.PlayerCommand(new PickupCommand(manager.GameService.Player));
					break;
				case Key.A:
					this.manager.KeyProcessor = new TargetSelectKeyProcessor(this.manager);
					break;
				case Key.S:
					this.manager.KeyProcessor = new FieldSelectKeyProcessor(this.manager);
					break;
			}
		}
	}
}
