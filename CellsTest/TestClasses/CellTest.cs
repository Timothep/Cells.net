using System.Drawing;
using Cells.GameCore;
using Cells.GameCore.Cells;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CellsTest.TestClasses
{
    [TestClass]
    public class CellTest
    {
        [TestMethod]
        public void TestCellPositionNotNull()
        {
            Cell testCell = new Cell(0, 0, 5, null, Color.Black);
            Assert.IsNotNull(testCell.Position);
        }

        [TestMethod]
        public void TestCellPositionXY()
        {
            const short x = 30;
            const short y = 30;

            Cell testCell = new Cell(x, y, 5, null, Color.Black);
            Assert.IsTrue(testCell.Position.X == x && testCell.Position.Y == y);
        }

        [TestMethod]
        public void TestThinkReturnAction()
        {
            Cell testCell = new Cell(0, 0, 5, new World(), Color.Black);
            Assert.IsNotNull(testCell.Think());
        }

        [TestMethod]
        public void TestSenseNotNull()
        {
            World testWorld = new World();
            Cell testCell = new Cell(0, 0, 5, testWorld, Color.Black);
            Assert.IsNotNull(testCell.Sense());
        }
    }
}
