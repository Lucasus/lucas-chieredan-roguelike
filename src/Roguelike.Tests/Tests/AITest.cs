using Roguelike;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Roguelike.Tests.Utilities;

namespace Roguelike.Tests
{
	
	
	/// <summary>
	///This is a test class for AITest and is intended
	///to contain all AITest Unit Tests
	///</summary>
	[TestClass()]
	public class AITest
	{
		private Map map;
		private Creature player;
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
			player = new Creature(10) { MeleeWeapon = new Weapon() { Damage = 5 } };
			map[0, 0].putCreature(player);
		}

		[TestMethod()]
		public void MoveCreatureTest()
		{
			Creature aiControledCreature = new Creature(10) { MeleeWeapon = new Weapon() { Damage = 5 } };
			map[1,2].putCreature(aiControledCreature);
			AI artificialInteligence = new AI(map, player, aiControledCreature);
			artificialInteligence.GenerateNextCommand();
			Assert.AreEqual(1,aiControledCreature.X);
			Assert.AreEqual(0,aiControledCreature.Y);
		}

		//[TestMethod()]
		//public void CreatureMovingTowardPlayer()
		//{
		//    Creature aiControledCreature = new Creature(10) { MeleeWeapon = new Weapon() { Damage = 5 } };
		//    map[1, 0].putCreature(aiControledCreature);
		//    AI artificialInteligence = new AI(map, player);
		//    ICreatureCommand command = artificialInteligence.generateNextCommand(aiControledCreature);
		//    if (command != null)
		//        command.execute();
		//    Assert.AreEqual(5, player.Health);
		//}

		[TestMethod()]
		public void AttackPlayerTest()
		{
			Creature aiControledCreature = new Creature(10) { MeleeWeapon = new Weapon() { Damage = 5 } };
			map[1, 0].putCreature(aiControledCreature);
			AI artificialInteligence = new AI(map, player, aiControledCreature);
			ICreatureCommand command = artificialInteligence.GenerateNextCommand();
			if(command != null)
				command.execute();
			Assert.AreEqual(5, player.Health);
		}

		[TestMethod()]
		[ExpectedException(typeof(CreatureException))]
		public void CreatureNotOnTheMapTest()
		{
			Creature aiControledCreature = new Creature(10) { MeleeWeapon = new Weapon() { Damage = 5 } };
			AI artificialInteligence = new AI(map, player, aiControledCreature);
			artificialInteligence.GenerateNextCommand();
		}
	}
}
