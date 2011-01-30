using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Roguelike;
using System.Windows.Media.Imaging;

namespace RoguelikeGUI.Utilities
{
    public class ImageLoader
    {
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
			else if (gameObject is Ammo)
				return LoadImage("money.png");
			else if (gameObject is GrenadeWeapon)
				return LoadImage("grenade.png");
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
