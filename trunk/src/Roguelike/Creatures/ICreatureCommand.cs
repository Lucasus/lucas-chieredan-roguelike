﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public interface ICreatureCommand
	{
		bool isExecutable();
		void execute();
	}
}
