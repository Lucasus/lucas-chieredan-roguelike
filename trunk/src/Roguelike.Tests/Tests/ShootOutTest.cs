using Roguelike;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Roguelike.Tests
{
    
    
    /// <summary>
    ///This is a test class for ShootOutTest and is intended
    ///to contain all ShootOutTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ShootOutTest
	{

		private Creature attacker;
		private Creature deffender;

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
			attacker = new Creature(10) { RangedWeapon = new RangedWeapon() { Damage = 5, Range = 1, Chance = 0.5 } };
			deffender = new Creature(10);
		}

		[TestMethod()]
		public void ShootHitTest()
		{
			ShootOut target = new ShootOut(new TestRandom(0.1));			
			target.commenceInteraction(attacker, deffender);
			Assert.AreEqual(5, deffender.Health);
		}

		[TestMethod()]
		public void ShootMissTest()
		{
			ShootOut target = new ShootOut(new TestRandom(0.9));
			target.commenceInteraction(attacker, deffender);
			Assert.AreEqual(10, deffender.Health);
		}
	}
}
