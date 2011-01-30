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
				GuiMapField guiField = manager.MapDrawer[this.targetField];
				if(guiField != null)
					guiField.DrawOnField(ImageLoader.LoadImage("selectBorder.png"));
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
				case Key.B: //
				case Key.NumPad1:
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y + 1, this.TargetField.X - 1];
					break;
				case Key.N://
				case Key.NumPad2:
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y + 1, this.TargetField.X];
					break;
				case Key.M://
				case Key.NumPad3:
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y + 1, this.TargetField.X + 1];
					break;
				case Key.G://
				case Key.NumPad4:
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y, this.TargetField.X - 1];
					break;
				case Key.J://
				case Key.NumPad6:
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y, this.TargetField.X + 1];
					break;
				case Key.T://
				case Key.NumPad7:
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y - 1, this.TargetField.X - 1];
					break;
				case Key.Y://
				case Key.NumPad8:
					this.TargetField = this.manager.GameService.Map[this.TargetField.Y - 1, this.TargetField.X];
					break;
				case Key.U://
				case Key.NumPad9:
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
		public String getProcessorComment()
		{
			return "Select Tile to throw granade to";
		}
	}
}
