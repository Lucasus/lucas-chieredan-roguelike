using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Roguelike;
using System.Windows.Media.Imaging;

namespace RoguelikeGUI.Utilities
{
    public class FieldToImageConverter
    {
        public Image[,] ConvertFieldArray(Field[,] fieldArray)
        {
            Image[,] imageArray = new Image[fieldArray.GetLength(0), fieldArray.GetLength(1)];
            for (int i = 0; i < fieldArray.GetLength(0); ++i)
            {
                for (int j = 0; j < fieldArray.GetLength(1); ++j)
                {
                    Uri uriSource = null;
                    if (fieldArray[i, j] is Wall)
                    {
                        uriSource = new Uri(@"/RoguelikeGUI;component/Images/wall.png", UriKind.Relative);
                    }
                    else if (fieldArray[i, j] is Floor)
                    {
                        uriSource = new Uri(@"/RoguelikeGUI;component/Images/floor.png", UriKind.Relative);
                    }
                    imageArray[i, j] = new Image() { Source = new BitmapImage(uriSource), Width = 30, Height = 30 };
                }
            }
            return imageArray;
        }
    }
}
