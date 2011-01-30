using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class GlobalGameModel
	{
		public HallOfFameModel HallOfFame { get; set; }

		public GlobalGameModel()
		{
			HallOfFame = new HallOfFameModel();
		}
	}
}
