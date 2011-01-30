using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Roguelike
{
	public class HallOfFameModel
	{
		public event EventHandler recordAdded;

		private List<KeyValuePair<string, int>> statistics;

		public HallOfFameModel()
		{
			statistics = new List<KeyValuePair<string, int>>();

			// create reader & open file
			TextReader tr = new StreamReader("scores.txt");

			string input = null;
			while ((input = tr.ReadLine()) != null)
			{
				string[] val = input.Split(',');
				statistics.Add(new KeyValuePair<string, int>(val[0], Int32.Parse(val[1])));
			}
			// close the stream
			tr.Close();

			if (recordAdded != null)
				recordAdded(this, null);
		}

		public IEnumerable<KeyValuePair<string, int>> getTopN(int n)
		{
			return statistics.OrderByDescending(x => x.Value).Take(n);
		}

		public void add(string playerName, int pointsGained)
		{
			statistics.Add(new KeyValuePair<string,int>(playerName, pointsGained));
			writeStatisticsToFile();
			if(recordAdded != null)
				recordAdded(this, null);
		}

		private void writeStatisticsToFile()
		{
			// create a writer and open the file
			TextWriter tw = new StreamWriter("scores.txt");
			foreach (KeyValuePair<string, int> pair in statistics)
			{
				tw.WriteLine(pair.Key + ", " + pair.Value);
			}
			// close the stream
			tw.Close();
		}
	}
}
