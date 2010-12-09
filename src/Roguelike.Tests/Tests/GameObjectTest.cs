using Roguelike;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Roguelike.Tests.Utilities;

namespace Roguelike.Tests
{
    
    
    /// <summary>
    ///This is a test class for GameObjectTest and is intended
    ///to contain all GameObjectTest Unit Tests
    ///</summary>
	[TestClass()]
	public class GameObjectTest
	{
		private Map map;
		private Player player;

		public TestContext TestContext { get; set; }

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		[TestInitialize()]
		public void MapTestInitialize()
		{
            map = TestObjects.GetTestMap();
			player = new Player(map) { Creature = new Creature() { Health = 10, Weapon = new Weapon() { Damage = 0, Range = 1 } } };
			map[0,0].putCreature(player.Creature);
		}

		/// <summary>
		///A test for GameObject Constructor
		///</summary>
		[TestMethod()]
		public void MoneyPickupTest()
		{
			Money moneyPickup = new Money() { Worth = 10 };
			map[0, 1].placeObject(moneyPickup);
			player.move(Player.Direction.Right);
			Assert.AreEqual(10, player.Creature.Money);
		}

		[TestMethod()]
		public void HealthPickupTest()
		{
			MedKit medikitPickup = new MedKit() { Health = 5 };
			map[0, 1].placeObject(medikitPickup);
			player.move(Player.Direction.Right);
			Assert.AreEqual(15, player.Creature.Health);
		}

		[TestMethod()]
		public void WeaponPickupTest()
		{
			Weapon weaponPickup = new Weapon() { Damage = 10, Range = 3};
			map[0, 1].placeObject(weaponPickup);
			player.move(Player.Direction.Right);
			Assert.AreSame(weaponPickup, player.Creature.Weapon);
		}

		[TestMethod()]
		public void MiltipleObjectPickupsTest()
		{
			Money moneyPickup = new Money() { Worth = 10 };
			map[0, 1].placeObject(moneyPickup);
			MedKit medikitPickup = new MedKit() { Health = 5 };
			map[0, 1].placeObject(medikitPickup);
			player.move(Player.Direction.Right);
			Assert.AreEqual(10, player.Creature.Money);
			Assert.AreEqual(15, player.Creature.Health);
		}

		[TestMethod()]
		public void MultipleWeaponsPickupTest()
		{
			Weapon weaponPickup = new Weapon() { Damage = 10, Range = 3 };
			map[0, 1].placeObject(weaponPickup);
			Weapon newerWeapon = new Weapon() { Damage = 15, Range = 2 };
			map[0, 1].placeObject(newerWeapon);
			player.move(Player.Direction.Right);
			Assert.AreSame(newerWeapon, player.Creature.Weapon);
		}
	}
}
