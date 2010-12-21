using Roguelike;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Roguelike.Tests.Utilities;

namespace Roguelike.Tests
{
    
    
    /// <summary>
    ///This is a test class for CreatureTest and is intended
    ///to contain all CreatureTest Unit Tests
    ///</summary>
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
			target = new Creature(10) { Weapon = new Weapon() { Range = 1, Damage = 5 } };
        }

		[TestMethod()]
		[ExpectedException(typeof(CreatureException))]
		public void ConstructorTest()
		{
			Creature target = new Creature(-10);
		}

		[TestMethod()]
		public void EmptyColisionTest()
		{
			map[1, 0].putCreature(target);
			target.interactWithField(map[0, 1]);
			Assert.AreEqual(1,target.X);
			Assert.AreEqual(0,target.Y);
		}

		[TestMethod()]
		public void WallColisionTest()
		{
			Creature target = new Creature(10);
			map[1, 0].putCreature(target);
			target.interactWithField(map[1, 1]);
			Assert.AreEqual(0, target.X);
			Assert.AreEqual(1, target.Y);
		}

		[TestMethod()]
		public void CreatureColisionTest()
		{
			
			map[0, 1].putCreature(target);
			Creature opponent = new Creature(10) { Weapon = new Weapon() { Range = 1, Damage = 5 } };
			map[0, 0].putCreature(opponent);
			target.interactWithField(map[0, 0]);
			Assert.AreEqual(5, opponent.Health);
		}

		[TestMethod()]
		public void FieldNotInRange()
		{
			map[0, 0].putCreature(target);
			Assert.IsFalse(target.canInteractWithField(map[0,2]));
		}

		[TestMethod()]
		public void AttackRangeTest()
		{
			map[0, 0].putCreature(target);
			Creature enemy = new Creature(10);
			map[1, 2].putCreature(enemy);
			Creature enemy2 = new Creature(10);
			map[0, 1].putCreature(enemy2);
			Assert.IsFalse(target.canAttack(enemy));
			Assert.IsTrue(target.canAttack(enemy2));
		}

		[TestMethod()]
		public void AttackTest()
		{
			target = new Creature(10) { Weapon = new Weapon() { Range = 3, Damage = 5 } };
			map[0, 0].putCreature(target);
			Creature enemy = new Creature(10);
			map[1, 2].putCreature(enemy);
			target.attack(enemy);
			Assert.AreEqual(5, enemy.Health);
		}

		[TestMethod()]
		public void DeadTest()
		{
			map[0, 0].putCreature(target);
			Creature enemy = new Creature(1);
			map[0, 1].putCreature(enemy);
			target.attack(enemy);
			Assert.IsTrue(enemy.isDead);
		}
	}
}
