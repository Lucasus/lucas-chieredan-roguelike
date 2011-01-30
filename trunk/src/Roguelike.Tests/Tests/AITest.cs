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
			map = new Map(TestObjects.mapConfig);
			player = new Creature(10) { MeleeWeapon = new MeleeWeapon() { Damage = 5 } };
		}

		[TestMethod()]
		public void WyjatekGdyPrzeciwnikPozaMapaTest()
		{
			// arrange
			map[0, 0].putCreature(player);
			Creature aiControledCreature = TestObjects.GetSimpleCreature(map, player);
			bool exceptionThrown = false;

			// act
			try
			{
				aiControledCreature.AI.GenerateNextCommand();
			}
			catch (CreatureException)
			{
				exceptionThrown = true;
			}

			// assert
			Assert.IsTrue(exceptionThrown);
		}

		[TestMethod()]
		public void PrzeciwnikNozownikSieZblizaGdyWidziGraczaIJestBliskoTest()
		{
			// arrange
			Creature commingEnemy = TestObjects.GetSimpleCreature(map, player);
			map[0, 0].putCreature(player);
			map[1, 2].putCreature(commingEnemy);

			// act
			ICreatureCommand command = commingEnemy.AI.GenerateNextCommand();
			MoveCommand mCommand = command as MoveCommand;
			
			// assert
			Assert.IsInstanceOfType(command, typeof(MoveCommand));
			Assert.AreEqual<MoveCommand.Direction>(MoveCommand.Direction.LeftUp, mCommand.moveDirection);
		}

		[TestMethod()]
		public void PrzeciwnikSieZblizaGdyWczesniejWidzialGraczaIWybieraNajlepszaTrase()
		{
			// arrange
			Creature commingEnemy = TestObjects.GetSimpleCreature(map, player);
			commingEnemy.SawPlayer = true;
			map[1, 1].putCreature(player);
			map[3, 4].putCreature(commingEnemy);

			// act
			ICreatureCommand command = commingEnemy.AI.GenerateNextCommand();
			MoveCommand mCommand = command as MoveCommand;

			// assert
			Assert.IsInstanceOfType(command, typeof(MoveCommand));
			Assert.AreEqual<MoveCommand.Direction>(MoveCommand.Direction.Up, mCommand.moveDirection);
		}

		[TestMethod()]
		public void PrzeciwnikRuszaSieLosowoGdyJestBliskoAleNieWidziGracza()
		{
			// arrange
			Creature commingEnemy = TestObjects.GetSimpleCreature(map, player);
			DirectionCounter d = new DirectionCounter();
			int n = 1000;
			map[1, 1].putCreature(player);
			map[3, 4].putCreature(commingEnemy);

			// act
			for (int i = 0; i < n; ++i)
			{
				if (i % 2 == 0) commingEnemy.AI.Sniper = false;
				else commingEnemy.AI.Sniper = true;
				d.AddDirection(commingEnemy.AI.GenerateNextCommand());
			}

			// assert
			Assert.IsTrue(d.GetMaxDirectionCount() < (n * 1.2) / 8, "d max = " + d.GetMaxDirectionCount() + ", max = " + ((n * 1.2) / 8));
			Assert.IsTrue(d.GetMinDirectionCount() > (n * 0.7) / 8, "d min = " + d.GetMinDirectionCount() + ", min = " + ((n * 0.7) / 8));
		}

		[TestMethod()]
		public void PrzeciwnikRuszaSieLosowoGdyWidziGraczaAleJestDaleko()
		{
			// arrange
			Creature commingEnemy = TestObjects.GetSimpleCreature(map, player);
			commingEnemy.SightRange = 3;
			DirectionCounter d = new DirectionCounter();
			int n = 1000;
			map[0, 0].putCreature(player);
			map[0, 5].putCreature(commingEnemy);

			// act
			for (int i = 0; i < n; ++i)
			{
				if (i % 2 == 0) commingEnemy.AI.Sniper = false;
				else commingEnemy.AI.Sniper = true;
				d.AddDirection(commingEnemy.AI.GenerateNextCommand());
			}

			// assert
			Assert.IsTrue(d.GetMaxDirectionCount() < (n * 1.2) / 8, "d max = " + d.GetMaxDirectionCount() + ", max = " + ((n * 1.2) / 8));
			Assert.IsTrue(d.GetMinDirectionCount() > (n * 0.7) / 8, "d min = " + d.GetMinDirectionCount() + ", min = " + ((n * 0.7) / 8));
		}

		[TestMethod()]
		public void SnajperStrzelaGdyGraczWidocznyWZasiegu()
		{
			// arrange
			Creature snajper = TestObjects.GetSimpleCreature(map, player);
			snajper.RangedWeapon = new RangedWeapon() { Range = 5, Chance = 0.9, Damage = 3 };
			snajper.AI.Sniper = true;
			map[0, 0].putCreature(player);
			map[0, 5].putCreature(snajper);

			// act
			ICreatureCommand command = snajper.AI.GenerateNextCommand();
			ShootCommand sCommand = command as ShootCommand;

			// assert
			Assert.IsInstanceOfType(command, typeof(ShootCommand));
		}

		[TestMethod()]
		public void SnajperPodchodziGdyGraczPozaZasiegiem()
		{
			// arrange
			Creature snajper = TestObjects.GetSimpleCreature(map, player);
			snajper.RangedWeapon = new RangedWeapon() { Range = 4, Chance = 0.9, Damage = 3 };
			snajper.AI.Sniper = true;
			map[0, 0].putCreature(player);
			map[0, 5].putCreature(snajper);

			// act
			ICreatureCommand command = snajper.AI.GenerateNextCommand();
			MoveCommand mCommand = command as MoveCommand;

			// assert
			Assert.IsInstanceOfType(command, typeof(MoveCommand));
			Assert.AreEqual<MoveCommand.Direction>(MoveCommand.Direction.Left, mCommand.moveDirection);
		}

		[TestMethod()]
		public void NozownikAtakujeGdyObokGracza()
		{
			// arrange
			Creature nozownik = TestObjects.GetSimpleCreature(map, player);
			nozownik.MeleeWeapon = new MeleeWeapon() { Damage = 5 } ;
			map[0, 0].putCreature(player);
			map[1, 0].putCreature(nozownik);

			// act
			ICreatureCommand command = nozownik.AI.GenerateNextCommand();
			AttackCommand aCommand = command as AttackCommand;

			// assert
			Assert.IsInstanceOfType(command, typeof(AttackCommand));
		}

		[TestMethod()]
		public void SnajperAtakujeNozemGdyObokGracza()
		{
			// arrange
			Creature snajper = TestObjects.GetSimpleCreature(map, player);
			snajper.MeleeWeapon = new MeleeWeapon() { Damage = 5 };
			snajper.AI.Sniper = true;
			map[0, 0].putCreature(player);
			map[1, 0].putCreature(snajper);

			// act
			ICreatureCommand command = snajper.AI.GenerateNextCommand();
			AttackCommand aCommand = command as AttackCommand;

			// assert
			Assert.IsInstanceOfType(command, typeof(AttackCommand));
		}

		[TestMethod()]
		public void PrzeciwnikUciekaGdyMaDokadIPanikuje()
		{
			// arrange
			Creature enemyInPanic = TestObjects.GetSimpleCreature(map, player);
			enemyInPanic.MeleeWeapon = new MeleeWeapon() { Damage = 5 };
			enemyInPanic.PanicModeCounter = 10;
			map[0, 0].putCreature(player);
			map[0, 1].putCreature(enemyInPanic);

			// act
			ICreatureCommand command = enemyInPanic.AI.GenerateNextCommand();
			MoveCommand mCommand = command as MoveCommand;

			// assert
			Assert.IsInstanceOfType(command, typeof(MoveCommand));
			Assert.AreEqual<MoveCommand.Direction>(MoveCommand.Direction.RightDown, mCommand.moveDirection);
		}

		[TestMethod()]
		public void PrzeciwnikPanikujeOkreslonaIloscCzasu()
		{
			// arrange
			Creature enemyInPanic = TestObjects.GetSimpleCreature(map, player);
			enemyInPanic.MeleeWeapon = new MeleeWeapon() { Damage = 5 };
			enemyInPanic.PanicModeCounter = 1;
			map[0, 0].putCreature(player);
			map[0, 1].putCreature(enemyInPanic);

			// act
			ICreatureCommand command1 = enemyInPanic.AI.GenerateNextCommand();
			MoveCommand mCommand1 = command1 as MoveCommand;

			ICreatureCommand command2 = enemyInPanic.AI.GenerateNextCommand();
			AttackCommand mCommand2 = command2 as AttackCommand;

			// assert
			Assert.IsInstanceOfType(command1, typeof(MoveCommand));
			Assert.AreEqual<MoveCommand.Direction>(MoveCommand.Direction.RightDown, mCommand1.moveDirection);
			Assert.IsInstanceOfType(command2, typeof(AttackCommand));
		}

		[TestMethod()]
		public void SpanikowanyNozownikAtakujeGdyNieMaGdzieUciec()
		{
			// arrange
			Creature nozownikInPanic = TestObjects.GetSimpleCreature(map, player);
			nozownikInPanic.MeleeWeapon = new MeleeWeapon() { Damage = 5 };
			nozownikInPanic.PanicModeCounter = 10;
			map[1, 1].putCreature(player);
			map[0, 0].putCreature(nozownikInPanic);

			// act
			ICreatureCommand command = nozownikInPanic.AI.GenerateNextCommand();
			AttackCommand mCommand = command as AttackCommand;

			// assert
			Assert.IsInstanceOfType(command, typeof(AttackCommand));
		}

		[TestMethod()]
		public void SpanikowanySnajperStrzelaGdyNieMaGdzieUciec()
		{
			// arrange
			Creature snajperInPanic = TestObjects.GetSimpleCreature(map, player);
			snajperInPanic.RangedWeapon = new RangedWeapon() { Range = 4, Chance = 0.9, Damage = 3 };
			snajperInPanic.AI.Sniper = true;
			snajperInPanic.PanicModeCounter = 10;
			map[1, 3].putCreature(player);
			map[0, 0].putCreature(snajperInPanic);

			// act
			ICreatureCommand command = snajperInPanic.AI.GenerateNextCommand();
			ShootCommand mCommand = command as ShootCommand;

			// assert
			Assert.IsInstanceOfType(command, typeof(ShootCommand));
		}

	}
}
