using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class HallOfFameModel
	{
		public event EventHandler recordAdded;

		private List<KeyValuePair<string, int>> statistics;

		public HallOfFameModel()
		{
			statistics = new List<KeyValuePair<string, int>>();
		}

		public IEnumerable<KeyValuePair<string, int>> getTopN(int n)
		{
			return statistics.OrderByDescending(x => x.Value).Take(n);
		}

		public void add(string playerName, int pointsGained)
		{
			statistics.Add(new KeyValuePair<string,int>(playerName, pointsGained));
			if(recordAdded != null)
				recordAdded(this, null);
		}
	}
}
