using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
	public class ThrowCommand : ICreatureCommand
	{
		Creature thrower;
		Field targetField;
		Map map;
		IRandomNumberGenerator randomNumberGenerator;

		public ThrowCommand(Creature thrower, Field targetField, Map map, IRandomNumberGenerator randomNumberGenerator)
		{
			this.thrower = thrower;
			this.targetField = targetField;
			this.map = map;
			this.randomNumberGenerator = randomNumberGenerator;
		}

		public bool isExecutable()
		{
			if(thrower.GrenadeWeapon != null && thrower.GrenadeWeapon.Count > 0 && map.getDistanceBetweenFields(thrower.Field, targetField) <= thrower.GrenadeWeapon.Range)
				return true;
			else
				return false;
		}
		public void execute()
		{
			if(this.isExecutable())
			{
				this.thrower.GrenadeWeapon.Count -= 1;
				int enemyCount = 0;
				int enemyKilledCount = 0;
				for(int i=-thrower.GrenadeWeapon.Spread; i<thrower.GrenadeWeapon.Spread + 1; i++)
				{
					for (int j = -thrower.GrenadeWeapon.Spread; j < thrower.GrenadeWeapon.Spread + 1; j++)
					{
						if (map.IsWithinBounds(targetField.Y + j, targetField.X + i))
						{
							Field damagedField = map[targetField.Y + j, targetField.X + i];
							if (damagedField.Creature != null)
							{
								damagedField.Creature.Health -= this.thrower.GrenadeWeapon.Damage;
								if (damagedField.Creature.isDead)
								{
									++enemyKilledCount;
									LootGenerator lootGen = new LootGenerator();
									lootGen.generateLoot(thrower, damagedField.Creature);
									damagedField.Creature.Field.removeCreature();
								}
								else
								{
									++enemyCount;
									if (damagedField.Creature.Health * 2 < damagedField.Creature.MaxHealth)
									{
										damagedField.Creature.PanicModeCounter = 20;
									}
								}
							}
						}
					}
				}
				AbstractLogger.Current.Log(thrower.MianownikName + " rzucił granatem zabijając " + enemyKilledCount + " i raniąc " + enemyCount + " osób.");
			}
		}
	}
}
