using Roguelike;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Roguelike.Tests.Utilities;

namespace Roguelike.Tests
{
	[TestClass()]
	public class MapGeneratorTest
	{
		public TestContext TestContext { get; set; }
		private Map map;
		private Creature player;


		[TestInitialize()]
		public void MapTestInitialize()
		{
			player = new Creature(10) { MeleeWeapon = new MeleeWeapon() { Damage = 5 } };
		}

		[TestMethod()]
		public void MapGeneratedTest()
		{
			// arrange
			int mapSizeX = 30;
			int mapSizeY = 40;
			MapGenerator Generator = new MapGenerator(mapSizeX, mapSizeY);

			// act
			map = Generator.GenerateMap(player);

			// assert
			Assert.AreEqual(mapSizeX, map.MapWidth);
			Assert.AreEqual(mapSizeY, map.MapHeight);
		}

		
	}
}
