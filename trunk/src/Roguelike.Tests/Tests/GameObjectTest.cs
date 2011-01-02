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
		Creature player;

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
			player = new Creature(10) { MeleeWeapon = new Weapon() { Damage = 1 } };
			map[0,0].putCreature(player);
		}

		/// <summary>
		///A test for GameObject Constructor
		///</summary>
		[TestMethod()]
		public void MoneyPickupTest()
		{
			Money moneyPickup = new Money() { Worth = 10 };
			map[0, 0].placeObject(moneyPickup);
			new PickupCommand(player).execute();
			Assert.AreEqual(10, player.Money);
		}

		[TestMethod()]
		public void HealthPickupTest()
		{
			MedKit medikitPickup = new MedKit() { Health = 5 };
			player.Health = 1;
			map[0, 0].placeObject(medikitPickup);
			new PickupCommand(player).execute();
			Assert.AreEqual(6, player.Health);
		}

		[TestMethod()]
		public void HealthPickupAlmostHealedTest()
		{
			MedKit medikitPickup = new MedKit() { Health = 5 };
			player.Health = 9;
			map[0, 0].placeObject(medikitPickup);
			new PickupCommand(player).execute();
			Assert.AreEqual(10, player.Health);
		}

		[TestMethod()]
		public void WeaponPickupTest()
		{
			Weapon weaponPickup = new Weapon() { Damage = 10 };
			map[0, 0].placeObject(weaponPickup);
			new PickupCommand(player).execute();
			Assert.AreSame(weaponPickup, player.MeleeWeapon);
		}

		[TestMethod()]
		public void MiltipleObjectPickupsTest()
		{
			Money moneyPickup = new Money() { Worth = 10 };
			map[0, 0].placeObject(moneyPickup);
			Weapon weaponPickup = new Weapon() { Damage = 5 };
			map[0, 0].placeObject(weaponPickup);
			new PickupCommand(player).execute();
			Assert.AreEqual(10, player.Money);
			Assert.AreEqual(weaponPickup, player.MeleeWeapon);
		}

		[TestMethod()]
		public void MultipleWeaponsPickupTest()
		{
			Weapon weaponPickup = new Weapon() { Damage = 10 };
			map[0, 0].placeObject(weaponPickup);
			Weapon newerWeapon = new Weapon() { Damage = 15 };
			map[0, 0].placeObject(newerWeapon);
			new PickupCommand(player).execute();
			Assert.AreSame(newerWeapon, player.MeleeWeapon);
		}
	}
}
