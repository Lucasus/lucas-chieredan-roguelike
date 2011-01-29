using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class RandomNumberGenerator : IRandomNumberGenerator
	{
		public static Random GlobalRandom { get; set; }

		Random random;
		static RandomNumberGenerator()
		{
			GlobalRandom = new Random();
		}

		public RandomNumberGenerator()
		{
			random = new Random();
		}

		public double generateNumber()
		{
			return random.NextDouble();	
		}
	}
}
