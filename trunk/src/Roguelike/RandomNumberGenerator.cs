using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class RandomNumberGenerator : IRandomNumberGenerator
	{
		Random random;
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
