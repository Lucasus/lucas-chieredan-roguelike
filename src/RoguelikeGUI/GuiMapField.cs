using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Roguelike;
using RoguelikeGUI.Utilities;


namespace RoguelikeGUI
{
	public class GuiMapField : Canvas
	{
		//private Field field;
		private Map map;
		private int x;
		private int y;
		private Image background;
		
		public Field Field
		{
			set 
			{
				if (value != null)
				{
					this.x = value.X;
					this.y = value.Y;
				}
				else
				{
					this.x = -1;
					this.y = -1;
				}
				//this.field = value;

				UpdateBackground();
			}
			get 
			{
				if(x != -1 && y != -1)
					return this.map[y, x];
				return null;
			} // this.field; }
		}

		private void UpdateBackground()
		{
			if (Field != null)
			{
				if (Field is Wall)
					background = ImageLoader.LoadImage("wall.png");
				else if (Field is Floor)
					background = ImageLoader.LoadImage("floor.png");
			}
		}

		public GuiMapField(Map m, Field field)
		{
			this.map = m;
			this.Field = field;
		}

		public void RefreshField(bool visible)
		{
			UpdateBackground();
			this.Children.Clear();
			if (this.Field != null)
			{
				this.Children.Add(background);
				if (visible)
				{
					if (this.Field.Objects.Count > 0)
					{
						if (this.Field.Objects.Count == 1)
						{
							Image objectImage = ImageLoader.CreaObjectImage(Field.Objects[0]);
							this.Children.Add(objectImage);
						}
						else
						{
							Image objectImage = ImageLoader.LoadImage("chest.png");
							this.Children.Add(objectImage);
						}
					}
					if (this.Field.Creature != null)
					{
						Image creatureImage = ImageLoader.CreateCreatureImage(this.Field.Creature);
						this.Children.Add(creatureImage);
						Line healthBar = new Line();
						healthBar.Stroke = Brushes.LightGreen;
						healthBar.X1 = 2;
						healthBar.Y1 = 2;
						healthBar.X2 = ((double)this.Field.Creature.Health / this.Field.Creature.MaxHealth) * 30 + 2;
						healthBar.Y2 = 2;
						healthBar.StrokeThickness = 2;

						this.Children.Add(healthBar);
					}
				}
				else
				{
					Rectangle rectangle = new Rectangle();
					rectangle.Width = 30;
					rectangle.Height = 30;
					rectangle.Fill = new SolidColorBrush(Colors.Black);
					rectangle.Opacity = 0.5;
					this.Children.Add(rectangle);
				}
			}
		}

		public void DrawOnField(UIElement element)
		{
			this.Children.Add(element);
		}
	}
}
