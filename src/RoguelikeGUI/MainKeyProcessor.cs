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
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.LeftDown));
					break;
				case Key.NumPad2: // Key.N://
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.Down));
					break;
				case Key.NumPad3: //Key.M://
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.RightDown));
					break;
				case Key.NumPad4: // Key.G://
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.Left));
					break;
				case Key.NumPad6: // Key.J://
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.Right));
					break;
				case Key.NumPad7: // Key.T://
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.LeftUp));
					break;
				case Key.NumPad8: // Key.Y://
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.Up));
					break;
				case Key.NumPad9: // Key.U://
					this.manager.PlayerCommand(new MoveCommand(MoveCommand.Direction.RightUp));
					break;
				case Key.NumPad5: // Key.H://
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
