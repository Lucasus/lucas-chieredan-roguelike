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
		/*public IList<Image>[,] ConvertFieldArray(Field[,] fieldArray)
        {
			IList<Image>[,] imageArray = new IList<Image>[fieldArray.GetLength(0), fieldArray.GetLength(1)];
            for (int i = 0; i < fieldArray.GetLength(0); ++i)
            {
                for (int j = 0; j < fieldArray.GetLength(1); ++j)
                {
					imageArray[i, j] = CreateImageFromField(fieldArray[i, j]);
                }
            }
            return imageArray;
        }

		private List<Image> CreateImageFromField(Field field)
		{
			List<Image> returnValue = new List<Image>();
            if (field is Wall)
            {
				returnValue.Add(LoadImage("wall.png"));
            }
            else if (field is Floor)
            {
				returnValue.Add(LoadImage("floor.png"));
            }

			if (field.Creature != null)
			{
				returnValue.Add(CreateCreatureImage(field.Creature));
			}
			else if(field.Objects.Count > 0)
			{
				returnValue.Add(CreaObjectImage(field.Objects[field.Objects.Count-1]));
			}
			return returnValue;
		}*/

		public static Image CreateCreatureImage(Creature creature)
		{
			if (creature.CreatureType == "Hero")
			{
				return LoadImage("player.png");
			}
			else if (creature.CreatureType == "Enemy")
			{
				return LoadImage("enemy.png");
			}
			else
				return null;
		}

		public static Image CreaObjectImage(IGameObject gameObject)
		{
			if(gameObject is MedKit)
				return LoadImage("health.png");
			else if (gameObject is Money)
				return LoadImage("money.png");
			else if (gameObject is RangedWeapon)
				return LoadImage("gun.png");
			else
				return LoadImage("knife.png");
		}

		public static Image LoadImage(string imageName)
		{
			Uri uriSource = new Uri(@"/RoguelikeGUI;component/Images/" + imageName, UriKind.Relative);
			return new Image() { Source = new BitmapImage(uriSource), Width = 30, Height = 30 };
		}
    }
}
