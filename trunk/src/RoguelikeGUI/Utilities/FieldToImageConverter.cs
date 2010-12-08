using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Roguelike;
using System.Windows.Media.Imaging;
using Roguelike.Creatures;

namespace RoguelikeGUI.Utilities
{
    public class FieldToImageConverter
    {
		public IList<Image>[,] ConvertFieldArray(Field[,] fieldArray)
        {
			IList<Image>[,] imageArray = new IList<Image>[fieldArray.GetLength(0), fieldArray.GetLength(1)];
            for (int i = 0; i < fieldArray.GetLength(0); ++i)
            {
                for (int j = 0; j < fieldArray.GetLength(1); ++j)
                {
					imageArray[i, j] = new List<Image>();
                    Uri uriSource = null;
                    if (fieldArray[i, j] is Wall)
                    {
                        uriSource = new Uri(@"/RoguelikeGUI;component/Images/wall.png", UriKind.Relative);
                    }
                    else if (fieldArray[i, j] is Floor)
                    {
                        uriSource = new Uri(@"/RoguelikeGUI;component/Images/floor.png", UriKind.Relative);
                    }
					imageArray[i, j].Add(new Image() { Source = new BitmapImage(uriSource), Width = 30, Height = 30 });

					if (fieldArray[i, j].Creature != null)
					{
						if (fieldArray[i, j].Creature.GetType() == typeof(PlayerCreature))
						{
							uriSource = new Uri(@"/RoguelikeGUI;component/Images/player.png", UriKind.Relative);
							imageArray[i, j].Add(new Image() { Source = new BitmapImage(uriSource), Width = 30, Height = 30 });
						}
					}
                }
            }
            return imageArray;
        }
    }
}
