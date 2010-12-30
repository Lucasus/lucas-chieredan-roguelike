using Roguelike;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Roguelike.Tests
{
    
    
    /// <summary>
    ///This is a test class for FieldTest and is intended
    ///to contain all FieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FieldTest
    {
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
        
        [TestMethod()]
        public void PlaceCreatureTest()
        {
            Creature thing = new Creature(10);
            Field target = new Wall(0, 0);
            Assert.IsFalse(target.putCreature(thing));
			Field target2 = new Floor(0, 0);
            Assert.IsTrue(target2.putCreature(thing));
			Assert.AreEqual(0,target2.X);
			Assert.AreEqual(0, target2.Y);
        }

		[TestMethod()]
		public void PlaceMultipleCreaturesTest()
		{
			Creature thing = new Creature(10);
			Creature thing2 = new Creature(10);
			Field target = new Floor(0, 0);

			Assert.IsTrue(target.putCreature(thing));
			Assert.IsFalse(target.putCreature(thing2));
		}

		[TestMethod()]
		public void PlaceObjectTest()
		{
			IGameObject obj = new Money();
			Field target = new Floor(0, 0);
			Assert.IsTrue(target.placeObject(obj));
			Field targe2 = new Wall(0, 0);
			Assert.IsFalse(targe2.placeObject(obj));
		}

		[TestMethod()]
		public void PlaceMultipleObjectsTest()
		{
			IGameObject thing = new Money();
			IGameObject thing2 = new Money();
			Field target = new Floor(0, 0);

			Assert.IsTrue(target.placeObject(thing));
			Assert.IsTrue(target.placeObject(thing2));
		}
    }
}
