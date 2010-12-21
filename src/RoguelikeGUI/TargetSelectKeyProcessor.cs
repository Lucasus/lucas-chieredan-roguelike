using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using Roguelike;

namespace RoguelikeGUI
{
	public class TargetSelectKeyProcessor : IKeyProcessor
	{
		GameManager manager;
		List<Creature> creatures;
		int currentPosition;
		Creature Target
		{
			get { return creatures[currentPosition];}
		}

		public TargetSelectKeyProcessor(GameManager manager)
		{
			this.manager = manager;
			this.creatures = this.manager.GameService.Ai.Creatures;
			this.currentPosition = 0;
		}

		public void processKey(Key key)
		{
			switch (key)
			{
				case Key.Q:
					this.manager.KeyProcessor = new MainKeyProcessor(this.manager);
					break;
				case Key.NumPad6:
					prevTarget();
					break;
				case Key.NumPad4:
					nextTarget();
					break;
				case Key.Enter:
					if(this.Target != null)
						this.manager.PlayerCommand(new AttackCommand(this.Target));
					break;
			}
		}

		public void prevTarget()
		{
			currentPosition = (currentPosition - 1);
			if(currentPosition < 0)
				currentPosition = this.creatures.Count - 1;
		}

		public void nextTarget()
		{
			currentPosition = (currentPosition + 1) % this.creatures.Count;
		}
	}
}
