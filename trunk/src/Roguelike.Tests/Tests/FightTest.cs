using Roguelike;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
			
			attacker = new Creature(10){Weapon = new Weapon(){Damage=1, Range=1, Chance=0.5}}; // TODO: Initialize to an appropriate value
			deffender = new Creature(10);
		}

		[TestMethod()]
		public void HitTest()
		{
			Fight fight = new Fight(new TestRandom(0.1));
			fight.commenceInteraction(attacker, deffender);
			Assert.AreEqual(9, deffender.Health);
		}

		[TestMethod()]
		public void MissTest()
		{
			Fight fight = new Fight(new TestRandom(0.9));
			fight.commenceInteraction(attacker, deffender);
			Assert.AreEqual(10, deffender.Health);
		}
	}
}
