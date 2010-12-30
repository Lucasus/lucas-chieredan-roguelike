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
			target = new Creature(10) { MeleeWeapon = new Weapon() { Damage = 5 }, RangedWeapon = new RangedWeapon() { Damage = 5, Range = 5, Chance = 0.5} };
			map[0, 0].putCreature(target);
        }

		[TestMethod()]
		[ExpectedException(typeof(CreatureException))]
		public void ConstructorTest()
		{
			Creature target = new Creature(-10);
		}

		[TestMethod()]
		public void FieldNotInRange()
		{
			Assert.IsFalse(target.canInteractWithField(map[0, 2]));
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
		public void MeeleAttackRangeTest()
		{
			Creature enemy = new Creature(10);
			map[1, 2].putCreature(enemy);
			Creature enemy2 = new Creature(10);
			map[0, 1].putCreature(enemy2);
			Assert.IsFalse(target.canAttack(enemy));
			Assert.IsTrue(target.canAttack(enemy2));
		}

		[TestMethod()]
		public void RangedAttackRangeTest()
		{
			Creature enemy = new Creature(10);
			map[1, 2].putCreature(enemy);
			Creature enemy2 = new Creature(10);
			map[0, 1].putCreature(enemy2);
			Assert.IsTrue(target.canShoot(enemy));
			Assert.IsTrue(target.canShoot(enemy2));
		}

		[TestMethod()]
		public void DeadTest()
		{
			Creature enemy = new Creature(1);
			map[0, 1].putCreature(enemy);
			target.attack(enemy);
			Assert.IsTrue(enemy.isDead);
		}
	}
}
