using Roguelike;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using Roguelike.Tests.Utilities;

namespace Roguelike.Tests
{
    
    
    /// <summary>
    ///This is a test class for FightTest and is intended
    ///to contain all FightTest Unit Tests
    ///</summary>
	[TestClass()]
	public class FightTest
	{
		private Creature attacker;
		private Creature deffender;
		private Map map;

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
			attacker = new Creature(10){MeleeWeapon = new MeleeWeapon(){Damage=1}};
			deffender = new Creature(10);
			map[0,0].putCreature(attacker);
			map[0,1].putCreature(deffender);
		}

		[TestMethod()]
		public void MeeleAttackRangeTest()
		{
			Creature enemy = new Creature(10);
			map[1, 2].putCreature(enemy);

			ICreatureCommand command = new AttackCommand(attacker, deffender, map);
			Assert.IsTrue(command.isExecutable());
			command = new AttackCommand(attacker, enemy, map);
			Assert.IsFalse(command.isExecutable());
		}

		[TestMethod()]
		public void CloseCombatHitTest()
		{
			new AttackCommand(attacker, deffender, map).execute();
			Assert.AreEqual(9, deffender.Health);
		}
	}
}
