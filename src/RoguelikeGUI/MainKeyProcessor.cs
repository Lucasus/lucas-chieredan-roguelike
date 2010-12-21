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
				case Key.B: // Key.NumPad1:
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.LeftDown));
					break;
				case Key.N://NumPad2:
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.Down));
					break;
				case Key.M://NumPad3:
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.RightDown));
					break;
				case Key.G://NumPad4:
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.Left));
					break;
				case Key.J://NumPad6:
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.Right));
					break;
				case Key.T://NumPad7:
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.LeftUp));
					break;
				case Key.Y://NumPad8:
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.Up));
					break;
				case Key.U://NumPad9:
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.RightUp));
					break;
				case Key.H://NumPad5:
					this.manager.PlayerCommand(new DoNothingCommand());
					break;
				case Key.P:
					this.manager.PlayerCommand(new PickupCommand());
					break;
				case Key.A:
					this.manager.KeyProcessor = new TargetSelectKeyProcessor(this.manager);
					break;
			}
		}
	}
}
