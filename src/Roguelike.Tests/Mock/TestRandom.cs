using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike.Tests
{
	public class TestRandom : IRandomNumberGenerator
	{
		private double value;
		public TestRandom(double value)
		{
			this.value = value;
		}
		public double generateNumber()
		{
			return this.value;
		}
	}
}
