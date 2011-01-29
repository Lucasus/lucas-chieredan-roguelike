using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public abstract class AbstractLogger
	{
		public static AbstractLogger Current { get; set; }
		public abstract void Log(string message);
		public abstract void Clear();
	}
}
