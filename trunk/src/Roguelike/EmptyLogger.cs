using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class EmptyLogger : AbstractLogger
	{
		public override void Log(string message)
		{
		}

		public override void Clear()
		{
		}
	}
}
