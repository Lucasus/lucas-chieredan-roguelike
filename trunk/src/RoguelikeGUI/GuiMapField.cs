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
		private Field field;
		private Image background;
		
		public Field Field
		{
            set {
                this.field = value;

                if (value != null)
                {
                    if (value is Wall)
                        background = ImageLoader.LoadImage("wall.png");
                    else if (value is Floor)
                        background = ImageLoader.LoadImage("floor.png");
                }
            }
			get { return this.field; }
		}

		public GuiMapField(Field field)
		{
            Field = field;
		}

		public void RefreshField(bool visible)
		{
			this.Children.Clear();
            if (this.field != null)
            {
                this.Children.Add(background);
                if (visible)
                {
                    if (this.field.Objects.Count > 0)
                    {
                        if (this.field.Objects.Count == 1)
                        {
                            Image objectImage = ImageLoader.CreaObjectImage(field.Objects[0]);
                            this.Children.Add(objectImage);
                        }
                        else
                        {
                            Image objectImage = ImageLoader.LoadImage("chest.png");
                            this.Children.Add(objectImage);
                        }
                    }
                    if (this.field.Creature != null)
                    {
                        Image creatureImage = ImageLoader.CreateCreatureImage(this.field.Creature);
                        this.Children.Add(creatureImage);
                        Line healthBar = new Line();
                        healthBar.Stroke = Brushes.LightGreen;
                        healthBar.X1 = 2;
                        healthBar.Y1 = 2;
                        healthBar.X2 = ((double)this.field.Creature.Health / this.field.Creature.MaxHealth) * 30 + 2;
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
