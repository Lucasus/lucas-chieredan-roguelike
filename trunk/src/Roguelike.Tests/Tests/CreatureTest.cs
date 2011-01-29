using Roguelike;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Roguelike.Tests.Utilities;

namespace Roguelike.Tests
{
	[TestClass()]
	public class CreatureTest
	{
		private Map map;
		private Creature target;
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
		public void MyTestInitialize()
		{
			map = TestObjects.GetTestMap();
			CreatureVisitor.map = map;
			target = new Creature(10) { MeleeWeapon = new Weapon() { Damage = 5 }, RangedWeapon = new RangedWeapon() { Damage = 5, Range = 5, Chance = 0.5} };
			map[0, 0].putCreature(target);
		}

		[TestMethod()]
		public void ConstructorTest()
		{
			try
			{
				Creature target = new Creature(-10);
				Assert.Fail("spodziewany był wyjątek");
			} catch(CreatureException) { }
		}

		[TestMethod()]
		public void FloorColisionTest()
		{
			target.interactWithField(map[0, 1]);
			Assert.AreEqual(1,target.X);
			Assert.AreEqual(0,target.Y);
		}

		[TestMethod()]
		public void WallColisionTest()
		{
			target.interactWithField(map[1, 1]);
			Assert.AreEqual(0, target.X);
			Assert.AreEqual(0, target.Y);
		}

		[TestMethod()]
		public void CreatureColisionTest()
		{
			Creature opponent = new Creature(10);
			map[0, 1].putCreature(opponent);
			target.interactWithField(map[0, 1]);
			Assert.AreEqual(5, opponent.Health);
		}

		[TestMethod()]
		public void DeadTest()
		{
			Creature enemy = new Creature(1);
			map[0, 1].putCreature(enemy);
			new AttackCommand(target, enemy, map).execute();
			Assert.IsTrue(enemy.isDead);
		}
	}
}
