using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public interface IFieldVisitor
	{
		void visit(Wall wall);
		void visit(Floor floor);
	}
}
