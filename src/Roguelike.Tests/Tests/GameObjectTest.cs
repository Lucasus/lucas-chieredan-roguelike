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
            player = new Player() { Creature = new Creature() { Health = 10, Weapon = new Weapon() { Damage = 0, Range = 1 } } };
			map[0,0].put(player.Creature);
		}

		/// <summary>
		///A test for GameObject Constructor
		///</summary>
		[TestMethod()]
		public void MoneyPickupTest()
		{
			Money money = new Money() { Worth = 10 };
			map[0, 1].place(money);
			player.move(Player.Direction.Right);
			Assert.AreEqual(10, player.Creature.Money);
		}

		[TestMethod()]
		public void HealthPickupTest()
		{
			MedKit kit = new MedKit() { Health = 5 };
			map[0, 1].place(kit);
			player.move(Player.Direction.Right);
			Assert.AreEqual(15, player.Creature.Health);
		}

		[TestMethod()]
		public void WeaponPickupTest()
		{
			Weapon wep = new Weapon() { Damage = 10, Range = 3};
			map[0, 1].place(wep);
			player.move(Player.Direction.Right);
			Assert.AreSame(wep, player.Creature.Weapon);
		}
	}
}
