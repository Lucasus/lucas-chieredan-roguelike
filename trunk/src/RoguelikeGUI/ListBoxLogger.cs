using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roguelike;
using System.Windows.Controls;

namespace RoguelikeGUI
{
	public class ListBoxLogger : AbstractLogger
	{
		private ListBox listbox;
		public ListBoxLogger(ListBox listbox)
		{
			this.listbox = listbox;
		}

		public override void Log(string message)
		{
			listbox.Items.Insert(0, message);
		}

		public override void Clear()
		{
			listbox.Items.Clear();
		}
	}
}
