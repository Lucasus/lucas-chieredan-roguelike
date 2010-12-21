using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;

using Roguelike;
using RoguelikeGUI.Utilities;

namespace RoguelikeGUI
{
	public class GuiMapField : Canvas
	{
		private Field field;
		private Image background;
		
		public Field Field
		{
			get { return this.field; }
		}

		public GuiMapField(Field field)
		{
			this.field = field;

			if (field is Wall)
				background = FieldToImageConverter.LoadImage("wall.png");
			else if (field is Floor)
				background = FieldToImageConverter.LoadImage("floor.png");
		}

		public void RefreshField()
		{
			this.Children.Clear();
			this.Children.Add(background);
			if(this.field.Objects.Count > 0)
			{
				Image objectImage = FieldToImageConverter.CreaObjectImage(field.Objects[0]);
				this.Children.Add(objectImage);
			}
			if(this.field.Creature != null)
			{
				Image creatureImage = FieldToImageConverter.CreateCreatureImage(this.field.Creature);
				this.Children.Add(creatureImage);
			}
		}

		public void DrawOnField(UIElement element)
		{
			this.Children.Add(element);
		}
	}
}
