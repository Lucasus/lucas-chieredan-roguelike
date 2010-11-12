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
            Creature thing = new Creature();
            Field target = new Wall();
            Assert.IsFalse(target.put(thing));   
            Field target2 = new Floor();
            Assert.IsTrue(target2.put(thing));
        }

		[TestMethod()]
		public void PlaceMultipleCreaturesTest()
		{
			Creature thing = new Creature();
			Creature thing2 = new Creature();
			Field target = new Floor();

			Assert.IsTrue(target.put(thing));
			Assert.IsFalse(target.put(thing2));
		}

		[TestMethod()]
		public void PlaceObjectTest()
		{
			GameObject obj = new GameObject();
			Field target = new Floor();
			Assert.IsTrue(target.place(obj));
			Field targe2 = new Wall();
			Assert.IsFalse(targe2.place(obj));
		}

		[TestMethod()]
		public void PlaceMultipleObjectsTest()
		{
			GameObject thing = new GameObject();
			GameObject thing2 = new GameObject();
			Field target = new Floor();

			Assert.IsTrue(target.place(thing));
			Assert.IsTrue(target.place(thing2));
		}
    }
}
