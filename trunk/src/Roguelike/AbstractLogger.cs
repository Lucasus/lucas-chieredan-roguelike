using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public abstract class AbstractLogger
	{
		private static AbstractLogger _current = new EmptyLogger();
		public static AbstractLogger Current { get { return _current; } set { _current = value; } }
		public abstract void Log(string message);
		public abstract void Clear();
	}
}
