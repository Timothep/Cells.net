using Cells.GameCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cells.GameCore.Cells;

namespace CellsTest.TestClasses
{
    [TestClass]
    public class WorldTest
    {
        [TestMethod]
        public void TestGetCellsNotNull()
        {
            World testWorld = new World();
            Assert.IsNotNull(testWorld.GetCells());    
        }
    }
}
