using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace RoguelikeGUI
{
	public interface IKeyProcessor
	{
		void processKey(Key key);
		String getProcessorComment();
	}
}
