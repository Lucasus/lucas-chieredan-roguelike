using Roguelike;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roguelike.Tests.Utilities;
namespace Roguelike.Tests
{
    
    
    /// <summary>
    ///This is a test class for Class1Test and is intended
    ///to contain all Class1Test Unit Tests
    ///</summary>
    [TestClass()]
    public class MapTest
    {
        private Map map;
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
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MapTestInitialize()
        {
            map = TestObjects.GetTestMap();
        }

        [TestMethod()]
        public void TerrainTest()
        {
            Assert.IsInstanceOfType(map[1, 1], typeof(Wall));
            Assert.IsInstanceOfType(map[1, 2], typeof(Floor));
        }

        [TestMethod()]
		[ExpectedException(typeof(MapOutOfBoundException))]
        public void OutOfBoundTest()
        {
			Field field = map[-1, 2];
			field = map[0, 3];
			field = map[1, -3];
			field = map[3, 2];
        }
    }
}
