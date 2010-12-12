using Roguelike;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roguelike.Tests.Utilities;
namespace Roguelike.Tests
{
    
    
    /// <summary>
    ///This is a test class for PlayerTest and is intended
    ///to contain all PlayerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PlayerTest
    {
		private Map map;
        public TestContext TestContext { get; set; }

		[TestInitialize()]
		public void MapTestInitialize()
		{
            map = TestObjects.GetTestMap();
        }

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

		[TestMethod()]
		public void MoveCreatureTest()
		{
			Creature playersCreature = new Creature() { Health = 10, Weapon = new Weapon() { Range = 1, Damage = 5 } };
			map[0,0].putCreature(playersCreature);
			Player player = new Player(map) { Creature = playersCreature };
			player.executeCommand(new MoveCommand(MoveCommand.Direction.Right));
			Assert.AreEqual(1, player.Creature.X);
			Assert.AreEqual(0, player.Creature.Y);
		}

		[TestMethod()]
		[ExpectedException(typeof(CreatureException))]
		public void CreatureNotOnTheMapTest()
		{
			Creature playersCreature = new Creature() { Health = 10, Weapon = new Weapon() { Range = 1, Damage = 5 } };
			Player player = new Player(map) { Creature = playersCreature };
			player.executeCommand(new MoveCommand(MoveCommand.Direction.Right));
		}
    }
}
