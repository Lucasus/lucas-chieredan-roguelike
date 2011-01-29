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
				if (this.Target != null)
					manager.MapDrawer.Draw(manager.GameService.Player.Field);
				currentPosition = value;
				if(this.Target != null)
					manager.MapDrawer[this.Target.Field].DrawOnField(ImageLoader.LoadImage("selectBorder.png"));
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
			Creature player = this.manager.GameService.Player;
			Map map = this.manager.GameService.Map;
			this.creatures = this.manager.GameService.Creatures.Where(x => map.getDistanceBetweenFields(player.Field, x.Field) <= player.RangedWeapon.Range && map.isSightBetweenFields(player.Field,x.Field)).ToList();
			this.CurrentPosition = 0;
		}

		public void processKey(Key key)
		{
			switch (key)
			{
				case Key.Q:
					if(this.Target != null)
						manager.MapDrawer.Draw(manager.GameService.Player.Field);
					this.manager.KeyProcessor = new MainKeyProcessor(this.manager);
					break;
				case Key.X://NumPad6:
					nextTarget();
					break;
				case Key.Z://NumPad4:
					prevTarget();
					break;
				case Key.Enter:
					if(this.Target != null)
						this.manager.PlayerCommand(new ShootCommand(manager.GameService.Player, this.Target, manager.GameService.Map, new RandomNumberGenerator()));
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
