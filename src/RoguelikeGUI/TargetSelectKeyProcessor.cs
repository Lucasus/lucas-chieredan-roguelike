using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using Roguelike;
using RoguelikeGUI.Utilities;

namespace RoguelikeGUI
{
	public class TargetSelectKeyProcessor : IKeyProcessor
	{
		private GameManager manager;
		private List<Creature> creatures;
		private int currentPosition;

		private int CurrentPosition
		{
			set {
				if(this.Target != null)
					manager.MapDrawer[this.Target.Field].RefreshField();
				currentPosition = value;
				if(this.Target != null)
					manager.MapDrawer[this.Target.Field].DrawOnField(FieldToImageConverter.LoadImage("selectBorder.png"));
			}
		}

		Creature Target
		{
			get { 
				if(creatures.Count > 0)
					return creatures[currentPosition];
				else
					return null;
				}
		}

		public TargetSelectKeyProcessor(GameManager manager)
		{
			this.manager = manager;
			this.creatures = this.manager.GameService.Ai.Creatures.Where(x => this.manager.GameService.Player.Creature.canShoot(x)).ToList();
			this.CurrentPosition = 0;
		}

		public void processKey(Key key)
		{
			switch (key)
			{
				case Key.Q:
					if(this.Target != null)
						manager.MapDrawer[this.Target.Field].RefreshField();
					this.manager.KeyProcessor = new MainKeyProcessor(this.manager);
					break;
				case Key.X://NumPad6:
					nextTarget();
					break;
				case Key.Z://NumPad4:
					prevTarget();
					break;
				case Key.Enter:
					if(this.Target != null && this.manager.GameService.Player.Creature.canShoot(this.Target))
						this.manager.PlayerCommand(new ShootCommand(this.Target));
					break;
			}
		}

		public void prevTarget()
		{
			if(currentPosition - 1 < 0)
				CurrentPosition = this.creatures.Count - 1;
			else
				CurrentPosition = currentPosition - 1;
		}

		public void nextTarget()
		{
			if(this.creatures.Count > 0)
				CurrentPosition = (currentPosition + 1) % this.creatures.Count;
		}
	}
}
