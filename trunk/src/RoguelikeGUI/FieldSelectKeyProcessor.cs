using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using Roguelike;
using RoguelikeGUI.Utilities;

namespace RoguelikeGUI
{
	public class FieldSelectKeyProcessor : IKeyProcessor
	{
		GameManager manager;
		Field targetField;
		Field TargetField
		{
			get {return targetField;}
			set 
			{
				if(this.targetField != null)
				{
					manager.MapDrawer.Draw(manager.GameService.Player.Field);
				}
				this.targetField = value;
				manager.MapDrawer[this.targetField].DrawOnField(ImageLoader.LoadImage("selectBorder.png"));
			}
		}

		public FieldSelectKeyProcessor(GameManager manager)
		{
			this.manager = manager;
			TargetField = this.manager.GameService.Player.Field;
		}

		public void processKey(Key key)
		{
			switch (key)
			{
				case Key.NumPad1:// Key.B: // 
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y + 1, this.TargetField.X - 1];
					break;
				case Key.NumPad2: // Key.N://
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y + 1, this.TargetField.X];
					break;
				case Key.NumPad3: //Key.M://
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y + 1, this.TargetField.X + 1];
					break;
				case Key.NumPad4: // Key.G://
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y, this.TargetField.X - 1];
					break;
				case Key.NumPad6: // Key.J://
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y, this.TargetField.X + 1];
					break;
				case Key.NumPad7: // Key.T://
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y - 1, this.TargetField.X - 1];
					break;
				case Key.NumPad8: // Key.Y://
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y - 1, this.TargetField.X];
					break;
				case Key.NumPad9: // Key.U://
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y - 1, this.TargetField.X + 1];
					break;
				case Key.Q:
					if (this.targetField != null)
						manager.MapDrawer.Draw(manager.GameService.Player.Field);
					this.manager.KeyProcessor = new MainKeyProcessor(this.manager);
					break;
				case Key.Enter:
					if (this.targetField != null)
						this.manager.PlayerCommand(new ThrowCommand(manager.GameService.Player, this.targetField, manager.GameService.Map, new RandomNumberGenerator()));
					break;
			}
		}
	}
}
